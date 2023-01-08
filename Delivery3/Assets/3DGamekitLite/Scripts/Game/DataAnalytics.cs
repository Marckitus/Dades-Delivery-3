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
    public abstract WWWForm FillData(string[] dataList);
}
public class UserDeaths : Data
{
    public UserDeaths()
    {
        type = DataType.DEATH;
        fileName = "UserDeaths.php";
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
    public UserHit()
    {
        type = DataType.HIT;
        fileName = "UserHits.php";
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
    public UserKills()
    {
        type = DataType.KILLS;
        fileName = "UserKills.php";
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
    public UserPosition()
    {
        type = DataType.POSITION;
        fileName = "UserPosition.php";
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
    public static IEnumerator AddData(Data data, params string[] dataList)
    {
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
}
