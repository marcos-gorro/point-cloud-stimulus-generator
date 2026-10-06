using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projection
{

    public Vector3 Project(Vector3 position3D)
    {
        float x = position3D.x;
        float y = position3D.y;
        float z = position3D.z;

        if (z <= 0) z = 0.001f;

        float x_prime = (x / z);
        float y_prime = (y / z);

        return new Vector3(x_prime, y_prime, z);
    }
}
