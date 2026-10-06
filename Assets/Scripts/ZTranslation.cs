using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZTranslation 
{
    public Vector3 TranslateZ(Vector3 dotPosition, float dz)
    {
        dotPosition.z += 10.0f + dz;
        return dotPosition;
    }
}
