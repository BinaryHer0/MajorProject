using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager {


    public static void SavePlayerOne(PlayerStats playerstats)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/playerone.sav", FileMode.Create);

        PlayerData data = new PlayerData(playerstats);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static float[] LoadPlayerStatsOne()
    {
        if (File.Exists(Application.persistentDataPath + "/playerone.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/playerone.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            return data.stats;
        }
        else
        {
            Debug.LogError("Stats does not exist.");
            return new float[16];
        }
    }

    public static int[] LoadPlayerLevelsOne()
    {
        if (File.Exists(Application.persistentDataPath + "/playerone.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/playerone.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            return data.levels;
        }
        else
        {
            Debug.LogError("Levels does not exist.");
            return new int[3];
        }
    }

    public static void SavePlayerTwo(PlayerStats playerstats)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/playertwo.sav", FileMode.Create);

        PlayerData data = new PlayerData(playerstats);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static float[] LoadPlayerStatsTwo()
    {
        if (File.Exists(Application.persistentDataPath + "/playertwo.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/playertwo.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            return data.stats;
        }
        else
        {
            Debug.LogError("Stats does not exist.");
            return new float[16];
        }
    }

    public static int[] LoadPlayerLevelsTwo()
    {
        if (File.Exists(Application.persistentDataPath + "/playertwo.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/playertwo.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            return data.levels;
        }
        else
        {
            Debug.LogError("Levels does not exist.");
            return new int[3];
        }
    }

    public static void SavePlayerThree(PlayerStats playerstats)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/playerthree.sav", FileMode.Create);

        PlayerData data = new PlayerData(playerstats);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static float[] LoadPlayerStatsThree()
    {
        if (File.Exists(Application.persistentDataPath + "/playerthree.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/playerthree.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            return data.stats;
        }
        else
        {
            Debug.LogError("Stats does not exist.");
            return new float[16];
        }
    }

    public static int[] LoadPlayerLevelsThree()
    {
        if (File.Exists(Application.persistentDataPath + "/playerthree.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/playerthree.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            return data.levels;
        }
        else
        {
            Debug.LogError("Levels does not exist.");
            return new int[3];
        }
    }
}

[Serializable]
public class PlayerData
{
    public float[] stats;
    public int[] levels;

    public Item[] items;

    public PlayerData(PlayerStats playerstats)







    {
        stats = new float[16];
        stats[0] = playerstats.playerMaxHealth;
        stats[1] = playerstats.playerHealth;
        stats[2] = playerstats.stamina;
        stats[3] = playerstats.maxStamina;
        stats[4] = playerstats.agility;
        stats[5] = playerstats.strength;
        stats[6] = playerstats.maxDefence;
        stats[7] = playerstats.currentDefence;
        stats[8] = playerstats.playerPosX;
        stats[9] = playerstats.playerPosY;
        stats[10] = playerstats.minCameraBoundX;
        stats[11] = playerstats.minCameraBoundY;
        stats[12] = playerstats.minCameraBoundZ;
        stats[13] = playerstats.maxCameraBoundX;
        stats[14] = playerstats.maxCameraBoundY;
        stats[15] = playerstats.maxCameraBoundZ;

        levels = new int[3];
        levels[0] = playerstats.playerLevel;
        levels[1] = playerstats.currentXP;
        levels[2] = playerstats.gold;

        items = new Item[12];
        items[0] = playerstats.savedItems[0];
        items[1] = playerstats.savedItems[1];
        items[2] = playerstats.savedItems[2];
        items[3] = playerstats.savedItems[3];
        items[4] = playerstats.savedItems[4];
        items[5] = playerstats.savedItems[5];
        items[6] = playerstats.savedItems[6];
        items[7] = playerstats.savedItems[7];
        items[8] = playerstats.savedItems[8];
        items[9] = playerstats.savedItems[9];
        items[10] = playerstats.savedItems[10];
        items[11] = playerstats.savedItems[11];
    }

}
