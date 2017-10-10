using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyState : MonoBehaviour
{
    private BattleManager battleManager;
    public BaseEnemy enemy;

    public bool enableDebugLines = false;

    public bool enemyTurn = false;

    // Indicator for which enemy is active
    public GameObject selector;

    public enum TurnState
    {
        PROCESSING,
        CHOOSEACTION,
        WAITING,
        ACTION,
        DEAD
    }

    public TurnState currentState;
     

    // Time for Action Variables
    private Vector3 startPosition; // Start Position for moving
    private bool actionStarted = false;
    public GameObject HeroToAttack;
    private float animSpeed = 10f;

    private bool alive = true;


    void Start()
    {
        currentState = TurnState.PROCESSING;
        selector.SetActive(false);
        battleManager = GameObject.Find("Battle Area").GetComponent<BattleManager>();
        startPosition = transform.position;
        // EnemyStats();
    }


    void Update()
    {
        if (enableDebugLines) print(enemy.theName + " Enemy State: " + currentState);
        switch (currentState)
        {
            case (TurnState.PROCESSING):
                if (enemyTurn == true)
                {
                    battleManager.EnemiesToManage.Add(this.gameObject);
                    currentState = TurnState.CHOOSEACTION;
                    break;
                }

                else
                {
                    currentState = TurnState.WAITING;
                    break;
                }

            case (TurnState.CHOOSEACTION):
                ChooseAction();
                currentState = TurnState.WAITING;
                break;

            case (TurnState.WAITING):

                break;

            case (TurnState.ACTION):
                StartCoroutine(TimeForAction());
                break;

            case (TurnState.DEAD):
                if (!alive)
                {
                    return;
                }
                else
                {
                    this.gameObject.tag = ("DeadEnemy");
                    battleManager.EnemiesInBattle.Remove(this.gameObject);
                    battleManager.DeadEnemies.Add(this.gameObject);
                    foreach(GameObject hero in battleManager.HerosInGame)
                    {
                        MainHeroState MHS = hero.GetComponent<MainHeroState>();
                        MHS.hero.heroXP = MHS.hero.heroXP + enemy.enemyExperience;
                        if (enableDebugLines) print("Player has gained "+ (MHS.hero.heroXP = MHS.hero.heroXP + enemy.enemyExperience) + " XP");
                    }

                    if (battleManager.EnemiesInBattle.Count > 0)
                    {
                        for (int i = 0; i < battleManager.PerformList.Count; i++)
                        {
                            if (i != 0)
                            {
                                if (battleManager.PerformList[i].AttackersGameObject == this.gameObject)
                                {
                                    battleManager.PerformList.Remove(battleManager.PerformList[i]);
                                } 
                                if (battleManager.PerformList[i].AttackersTarget == this.gameObject)
                                {
                                    if (battleManager.EnemiesInBattle.Count == 0)
                                    {
                                        battleManager.PerformList[i].AttackersTarget = null;
                                    }

                                    else
                                    {
                                        battleManager.PerformList[i].AttackersTarget = battleManager.EnemiesInBattle[Random.Range(0, battleManager.EnemiesInBattle.Count)];
                                    }
                                }


                            }
                        }
                    }
                    this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    alive = false;
                    battleManager.EnemyButtons();

                    battleManager.battleStates = BattleManager.PerformAction.CHECKALIVE;
                    this.gameObject.SetActive(false);
                }
                break;
        }
    }

    void ChooseAction()
    {
        TurnBasedHandler myattack = new TurnBasedHandler();
        myattack.Attacker = enemy.theName;
        myattack.Type = "Enemy";
        myattack.AttackersGameObject = this.gameObject;
        myattack.AttackersTarget = battleManager.HerosInGame[Random.Range(0, battleManager.HerosInGame.Count)];

        int num = Random.Range(0, enemy.attacks.Count);
        myattack.choosenAttack = enemy.attacks[num];
        enemy.currentStamina = enemy.currentStamina-myattack.choosenAttack.attackCost;
        if (enableDebugLines) print(this.gameObject.name + " has choosen " + myattack.choosenAttack.attackName + " and does " + myattack.choosenAttack.attackDamage + " damage!");



        battleManager.CollectActions(myattack); // sends the information to perform list via Collect Actions
    }

    private IEnumerator TimeForAction()
    {
        if(actionStarted)
        {
            yield break;
        }

        // Moving towards Hero to Attack
        actionStarted = true;
        Vector3 heroPosition = new Vector3(HeroToAttack.transform.position.x + 1.5f, HeroToAttack.transform.position.y, HeroToAttack.transform.position.z);
        while (MoveTowardsEnemy(heroPosition))
        {
            yield return null;
        }

        // Wait for x seconds infront of Hero
        yield return new WaitForSeconds(0.5f);

        // Damage Said Hero
        DoDamage();

        // Move back to start Position
        Vector3 firstPosition = startPosition;
        while (MoveTowardsStart(firstPosition))
        {
            yield return null;
        }

        // Remove from the Battle Manager Perform List and EnemiesToManage
        battleManager.PerformList.RemoveAt(0);
        battleManager.EnemiesToManage.Remove(this.gameObject);

        // Resetting the Battle Manager State Machine
        battleManager.battleStates = BattleManager.PerformAction.WAIT;

        actionStarted = false;
        // Reset the state machine into wait cycle
        enemyTurn = false;

        if (battleManager.EnemiesToManage.Count == 0)
        {
            battleManager.heroTurns = true;
            battleManager.enemyTurns = false;
            currentState = TurnState.PROCESSING;
        }

        else
        {
            currentState = TurnState.PROCESSING;
        }
        

    }

    private bool MoveTowardsEnemy (Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    private bool MoveTowardsStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    void DoDamage()
    {

        float calculateDamage = enemy.currentAttack + battleManager.PerformList[0].choosenAttack.attackDamage;
        HeroToAttack.GetComponent<MainHeroState>().TakeDamage(calculateDamage);
    }

    public void TakeDamage(float getDamageAmount)
    {
        enemy.currentHP -= getDamageAmount;
        if (enemy.currentHP <= 0)
        {
            enemy.currentHP = 0;
            currentState = TurnState.DEAD;
        }
    }


     

} 