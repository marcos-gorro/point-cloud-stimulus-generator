using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotation 
{
    public Vector3 RotateXZ(Vector3 dotPosition, float angle)
    {
        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);

        Vector3 positionAfterRotation = new Vector3(0,0,0);


        positionAfterRotation.x = (dotPosition.x * cos) - (dotPosition.z * sin);
        positionAfterRotation.y = dotPosition.y;
        positionAfterRotation.z = (dotPosition.x * sin) + (dotPosition.z * cos);

        return positionAfterRotation;
    }
}
