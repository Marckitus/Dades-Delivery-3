using System.Collections.Generic;
using UnityEngine;
using MyBox;
using System.ComponentModel;
using System.Linq;

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
    public bool[] pathActives;
    private UserPosition[] positionData;
    private Dictionary<System.Guid, Vector3[]> playerPositions;
    private Color[] pathColors;

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
            playerPositions = GetPositionsByUID(positionData);
        };
        StartCoroutine(DataAnalytics.ReadData(new UserPosition(), positionCallback));
    }

    private void OnDrawGizmos()
    {
        if (deathsActive && deathsData  != null && deathsData.Length > 0)
        {
            Gizmos.color = deathsColor;
            for (int i = 0; i < deathsData.Length; i++)
            {
                Gizmos.DrawCube(Utils.StringToVector3(deathsData[i].deathPosition), new Vector3(1,1,1));
            }
        }

        if (killsActive && killsData != null && killsData.Length > 0)
        {
            Gizmos.color = killsColor;
            for (int i = 0; i < killsData.Length; i++)
            { 
                Gizmos.DrawCube(Utils.StringToVector3(killsData[i].enemyPosition), new Vector3(1, 1, 1));
            }
        }

        if (hitsActive && hitsData != null && hitsData.Length > 0)
        {
            Gizmos.color = hitsColor;
            for (int i = 0; i < hitsData.Length; i++)
            {
                Gizmos.DrawCube(Utils.StringToVector3(hitsData[i].hitPosition), new Vector3(1, 1, 1));
            }
        }

        if (positionActive && positionData != null && positionData.Length > 0)
        {
            for (int i = 0; i < playerPositions.Count; i++)
            {
                if (pathActives[i])
                {
                    Vector3[] positions = playerPositions.ElementAt(i).Value;

                    Gizmos.color = pathColors[i];
                    for (int j = 0; j < positions.Length; j++)
                    {
                        Gizmos.DrawCube(positions[j], new Vector3(.3f, .3f, .3f));
                    }
                }
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

    public Dictionary<System.Guid, Vector3[]> GetPositionsByUID(UserPosition[] positions)
    {
        // Crear un diccionario vacío
        var positionsByUID = new Dictionary<System.Guid, Vector3[]>();

        // Iterar sobre cada UserPosition en el array
        foreach (var position in positions)
        {
            // Obtener la userUID de la posición actual
            System.Guid uid = System.Guid.Parse(position.userUID);

            // Convertir la posición actual a un Vector3
            var vec3 = Utils.StringToVector3(position.playerPosition);

            // Si el diccionario no tiene una entrada para la userUID actual, crear una nueva
            if (!positionsByUID.ContainsKey(uid))
            {
                positionsByUID[uid] = new Vector3[] { vec3 };
            }
            else
            {
                // Añadir el Vector3 al array en la entrada del diccionario correspondiente a la userUID
                var currentArr = positionsByUID[uid];
                var newArr = new Vector3[currentArr.Length + 1];
                currentArr.CopyTo(newArr, 0);
                newArr[currentArr.Length] = vec3;
                positionsByUID[uid] = newArr;
            }
        }

        pathColors = new Color[positionsByUID.Count];
        for (int i = 0; i < pathColors.Length; i++)
        {
            pathColors[i] = Random.ColorHSV();
        }

        pathActives = new bool[positionsByUID.Count];
        for (int i = 0; i < pathActives.Length; i++)
        {
            pathActives[i] = true;
        }

        // Devolver el diccionario
        return positionsByUID;
    }
}
