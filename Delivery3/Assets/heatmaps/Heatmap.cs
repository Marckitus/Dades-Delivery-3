// Alan Zucconi
// www.alanzucconi.com
using UnityEngine;
using System.Collections;
using MyBox;

public class Heatmap : MonoBehaviour
{
    public Material material;
    public Vector3[] positions;
    public DataType dataType;

    private DataDebugDrawer dataDebug;

    private void Start()
    {
        dataDebug = FindObjectOfType<DataDebugDrawer>();
    }

    [ButtonMethod]
    public void ResetPositions()
    {
        dataDebug = FindObjectOfType<DataDebugDrawer>();
        if (dataDebug)
        {
            positions = new Vector3[0];
            positions = dataDebug.GetDataPosition(dataType);
        }
    }

    [ButtonMethod]
    public void UpdateMaterial()
    {
        if (positions.Length > 0)
        {
            material.SetInt("_Points_Length", positions.Length);
            Vector4[] points = new Vector4[positions.Length];
            Vector4[] properties = new Vector4[positions.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                points[i] = new Vector4(positions[i].x, positions[i].y, positions[i].z, 0);
                properties[i] = new Vector4(5, 1, 0, 0);
            }
            material.SetVectorArray("_Points", points);
            material.SetVectorArray("_Properties", properties);
        }    }
}