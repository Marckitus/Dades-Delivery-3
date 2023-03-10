using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture),
            float.Parse(sArray[1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture),
            float.Parse(sArray[2], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture));

        return result;
    }
}
