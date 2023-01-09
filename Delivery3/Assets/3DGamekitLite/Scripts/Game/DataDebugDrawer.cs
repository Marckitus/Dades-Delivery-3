using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class DataDebugDrawer : MonoBehaviour
{
    // Array de vectores que representan el pathing
    public System.Action<Data[]> deathsCallback;
    private UserDeaths[] deathsData;

    // Color de la línea que se dibujará
    public Color lineColor = Color.red;

    [ButtonMethod]
    public void GetData()
    {
        UserDeaths death = new UserDeaths();

        deathsCallback = (result) =>
        {
            deathsData = (UserDeaths[])result;
        };

        StartCoroutine(DataAnalytics.ReadData(death, deathsCallback));
    }

    private void OnDrawGizmos()
    {
        if (deathsData.Length > 0)
        {
            for (int i = 0; i < deathsData.Length - 1; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(Utils.StringToVector3(deathsData[i].deathPosition), new Vector3(1,1,1));
                Debug.Log(Utils.StringToVector3(deathsData[i].deathPosition));
            }
        }
        
    }
}
