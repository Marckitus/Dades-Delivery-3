using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using static Gamekit3D.Damageable;

public enum DataType
{
    DEATH,
    HIT,
    KILLS,
    POSITION
}

public enum DamageType
{
    NONE,
    FALL,
    ACID,
    SPITTER,
    CHOMPER
}

public enum EnemyType
{
    SPITTER,
    CHOMPER,
    BLOCKS
}

public abstract class Data
{
    public DataType type;
    public string fileName;
    public string readerFileName;

    public abstract WWWForm FillData(string[] dataList);
}
public class UserDeaths : Data
{
    public DamageType deathType;
    public UserDeaths()
    {
        type = DataType.DEATH;
        fileName = "UserDeaths.php";
        fileName = "UserDeathsReader.php";
    }
    public override WWWForm FillData(string[] dataList)
    {
        WWWForm dataForm = new WWWForm();

        dataForm.AddField("deathPosition", dataList[0]);
        dataForm.AddField("deathTime", dataList[1]);
        dataForm.AddField("deathType", dataList[2]);

        return dataForm;
    }
}
public class UserHit : Data
{
    public DamageType hitType;
    public UserHit()
    {
        type = DataType.HIT;
        fileName = "UserHits.php";
        fileName = "UserHitsReader.php";
    }
    public override WWWForm FillData(string[] dataList)
    {
        WWWForm dataForm = new WWWForm();

        dataForm.AddField("hitPosition", dataList[0]);
        dataForm.AddField("hitTime", dataList[1]);
        dataForm.AddField("hitType", dataList[2]);

        return dataForm;
    }
}
public class UserKills : Data
{
    public EnemyType enemyType;
    public UserKills()
    {
        type = DataType.KILLS;
        fileName = "UserKills.php";
        fileName = "UserKillsReader.php";
    }
    public override WWWForm FillData(string[] dataList)
    {
        WWWForm dataForm = new WWWForm();

        dataForm.AddField("enemyPosition", dataList[0]);
        dataForm.AddField("enemyKillTime", dataList[1]);
        dataForm.AddField("enemyKillType", dataList[2]);

        return dataForm;
    }
}
public class UserPosition : Data
{
    public Vector3 playerVelocity;
    public System.Guid sessionUID;
    public UserPosition()
    {
        type = DataType.POSITION;
        fileName = "UserPosition.php";
        fileName = "UserPositionReader.php";
    }
    public override WWWForm FillData(string[] dataList)
    {
        WWWForm dataForm = new WWWForm();

        dataForm.AddField("playerPosition", dataList[0]);
        dataForm.AddField("playerTime", dataList[1]);
        dataForm.AddField("playerVelocity", dataList[2]);
        dataForm.AddField("userUID", dataList[3]);

        return dataForm;
    }
}
public class DataAnalytics : MonoBehaviour
{
    public static IEnumerator AddData(DataType type, params string[] dataList)
    {
        Data data = CreateData(type);
        string linkName = "https://citmalumnes.upc.es/~polvp1/" + data.fileName;
        UnityWebRequest database = UnityWebRequest.Post(linkName, data.FillData(dataList));
        yield return database.SendWebRequest();

        if (database.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error downloading: " + database.error);
        }
        else
        {
            if (data.type == DataType.POSITION)
            {
                Debug.Log("Data of type " + data.type.ToString() + " sended");
            }
            else
            {
                Debug.Log("Data of type " + data.type.ToString() + " sended: " + database.downloadHandler.text);
            }
        }
    }

    public static IEnumerator ReadData(Data data, Data[] dataList)
    {
        string linkName = "https://citmalumnes.upc.es/~polvp1/" + data.readerFileName;
        UnityWebRequest database = UnityWebRequest.Post(linkName, new WWWForm());
        yield return database.SendWebRequest();

        if (database.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error downloading: " + database.error);
        }
        else
        {
            Debug.Log("Data of type " + data.type.ToString() + " received");

            switch (data.type)
            {
                case DataType.DEATH:
                    dataList = JsonUtility.FromJson<UserDeaths[]>(database.downloadHandler.text);
                    break;
                case DataType.HIT:
                    break;
                case DataType.KILLS:
                    break;
                case DataType.POSITION:
                    break;
                default:
                    break;
            }
        }
    }

    public static int GetDamageType(string dmgName)
    {
        switch (dmgName)
        {
            case "DeathVolume":
                return (int)DamageType.FALL;
            case "Acid":
                return (int)DamageType.ACID;
            case "Spit(Clone)":
                return (int)DamageType.SPITTER;
            case "Chomper":
                return (int)DamageType.CHOMPER;
            default:
                return (int)DamageType.NONE;
        }
    }

    public static int GetEnemyType(string name)
    {
        switch (name)
        {
            case "Spitter":
                return (int)EnemyType.SPITTER;
            case "Chomper":
                return (int)EnemyType.CHOMPER;
            case "Cube":
                return (int)EnemyType.BLOCKS;
            default:
                return -1;
        }
    }

    public static Data CreateData(DataType type)
    {
        switch (type)
        {
            case DataType.DEATH:
                return new UserDeaths();
            case DataType.HIT:
                return new UserHit();
            case DataType.KILLS:
                return new UserKills();
            case DataType.POSITION:
                return new UserPosition();
            default:
                throw new ArgumentException("Invalid type enum value");
        }
    }
}
