using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class DataDebugDrawer : MonoBehaviour
{
    // Array de vectores que representan el pathing
    public System.Action<Data[]> deathsCallback;
    private UserDeaths[] deathsData;

    public System.Action<Data[]> hitsCallback;
    private UserHit[] hitsData;
    
    public System.Action<Data[]> killsCallback;
    private UserKills[] killsData;
    
    public System.Action<Data[]> positionCallback;
    private UserPosition[] positionData;

    // Color de la línea que se dibujará
    public Color deathsColor = Color.red;
    public bool deathsActive = true;
    [Space]
    public Color hitsColor = Color.yellow;
    public bool hitsActive = true;
    [Space]
    public Color killsColor = Color.green;
    public bool killsActive = true;
    [Space]
    public Color positionColor = Color.blue;
    public bool positionActive = true;

    [ButtonMethod]
    public void GetData()
    {
        deathsCallback = (result) =>
        {
            deathsData = (UserDeaths[])result;
        };
        StartCoroutine(DataAnalytics.ReadData(new UserDeaths(), deathsCallback));

        hitsCallback = (result) =>
        {
            hitsData = (UserHit[])result;
        };
        StartCoroutine(DataAnalytics.ReadData(new UserHit(), hitsCallback));

        killsCallback = (result) =>
        {
            killsData = (UserKills[])result;
        };
        StartCoroutine(DataAnalytics.ReadData(new UserKills(), killsCallback));

        positionCallback = (result) =>
        {
            positionData = (UserPosition[])result;
        };
        StartCoroutine(DataAnalytics.ReadData(new UserPosition(), positionCallback));
    }

    private void OnDrawGizmos()
    {
        if (deathsActive && deathsData  != null && deathsData.Length > 0)
        {
            for (int i = 0; i < deathsData.Length; i++)
            {
                Gizmos.color = deathsColor;
                Gizmos.DrawCube(Utils.StringToVector3(deathsData[i].deathPosition), new Vector3(1,1,1));
            }
        }

        if (killsActive && killsData != null && killsData.Length > 0)
        {
            for (int i = 0; i < killsData.Length; i++)
            {
                Gizmos.color = killsColor;
                Gizmos.DrawCube(Utils.StringToVector3(killsData[i].enemyPosition), new Vector3(1, 1, 1));
            }
        }

        if (hitsActive && hitsData != null && hitsData.Length > 0)
        {
            for (int i = 0; i < hitsData.Length; i++)
            {
                Gizmos.color = hitsColor;
                Gizmos.DrawCube(Utils.StringToVector3(hitsData[i].hitPosition), new Vector3(1, 1, 1));
            }
        }

        if (positionActive && positionData != null && positionData.Length > 0)
        {
            for (int i = 0; i < positionData.Length; i++)
            {
                Gizmos.color = positionColor;
                Gizmos.DrawCube(Utils.StringToVector3(positionData[i].playerPosition), new Vector3(.3f, .3f, .3f));
            }
        }
    }

    public Vector3[] GetDataPosition(DataType dataType)
    {
        Vector3[] newVector = new Vector3[0];
        switch (dataType)
        {
            case DataType.DEATH:
                newVector = new Vector3[deathsData.Length];
                for (int i = 0; i < deathsData.Length; i++)
                {
                    newVector[i] = Utils.StringToVector3(deathsData[i].deathPosition);
                }
                break;
            case DataType.HIT:
                newVector = new Vector3[hitsData.Length];
                for (int i = 0; i < hitsData.Length; i++)
                {
                    newVector[i] = Utils.StringToVector3(hitsData[i].hitPosition);
                }
                break;
            case DataType.KILLS:
                newVector = new Vector3[killsData.Length];
                for (int i = 0; i < killsData.Length; i++)
                {
                    newVector[i] = Utils.StringToVector3(killsData[i].enemyPosition);
                }
                break;
            case DataType.POSITION:
                newVector = new Vector3[positionData.Length];
                for (int i = 0; i < positionData.Length; i++)
                {
                    newVector[i] = Utils.StringToVector3(positionData[i].playerPosition);
                }
                break;
            default:
                break;
        }
        return newVector;
    }
}
