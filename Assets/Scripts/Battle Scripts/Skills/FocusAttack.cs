using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusAttack : BaseAttack {

    public FocusAttack()
    {
        attackName = "Focus Attack";
        attackDescription = "Focuses your attack to target enemies weak spots!";
        attackDamage = 15f;
        attackCost = 15f;
    }
}
