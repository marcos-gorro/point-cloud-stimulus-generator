using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class PointCloudData
{
    private Vector3[] pointCloud;


    public void extractPointCloud()
    {

    }


    public Vector3[] PointCloudCube(int numOfDots, float size)
    {
        Vector3[] pointCloud = new Vector3[numOfDots];

        float HalfSize = size / 2;

        //Secure vertexes
        Vector3[] vertices = CubeVertex(HalfSize);

        for (int i = 0; i < vertices.Length; i++)
        {
            pointCloud[i] = vertices[i];
        }

            for (int i = vertices.Length; i < numOfDots; i++)
        {

            //Pick a random face index from 0 to 5
            int FaceIndex = UnityEngine.Random.Range(0, 6);

            // Randomize the two free surface axes
            float U = UnityEngine.Random.Range(-HalfSize, HalfSize);
            float V = UnityEngine.Random.Range(-HalfSize, HalfSize);

            if (FaceIndex == 0)
            {
                pointCloud[i] = new Vector3(HalfSize, U, V);   // Front Face
            }
            else if (FaceIndex == 1)
            {
                pointCloud[i] = new Vector3(-HalfSize, U, V);       // Back Face
            }
            else if (FaceIndex == 2)
            {
                pointCloud[i] = new Vector3(U, HalfSize, V);       // Top Face
            }
            else if (FaceIndex == 3)
            {
                pointCloud[i] = new Vector3(U, -HalfSize, V);       //Bottom Face
            }
            else if (FaceIndex == 4)
            {
                pointCloud[i] = new Vector3(U, V, HalfSize);      // Right Face
            }
            else if (FaceIndex == 5)
            {
                pointCloud[i] = new Vector3(U, V, -HalfSize);      // Left Face
            }
         }


        return pointCloud;
    }

    


    public Vector3[] PointCloudSphere(int numOfDots, float radius)
    {
        Vector3[] pointCloud = new Vector3[numOfDots];
        

        float goldenRatio = (1f + Mathf.Sqrt(5f)) / 2f;

        for (int i = 0; i < numOfDots; i++)
        {
            float index = i + 0.5f;

            // Calculate spherical coordinates using Fibonacci spiral
            float phi = Mathf.Acos(1f - 2f * index / numOfDots);
            float theta = 2f * Mathf.PI * goldenRatio * index;

            // Convert to Cartesian (X, Y, Z) space
            float x = radius * Mathf.Cos(theta) * Mathf.Sin(phi);
            float y = radius * Mathf.Sin(theta) * Mathf.Sin(phi);
            float z = radius * Mathf.Cos(phi);

            pointCloud[i] = new Vector3(x, y, z);
            
        }
        return pointCloud;
    }

    public Vector3[] PointCloudCylinder(int numOfDots, float radius, float height)
    {
        Vector3[] pointCloud = new Vector3[numOfDots];

        int dotPerCircle  = (int) ((float) numOfDots / height);
        int index = 0;

        for(int i =0; i < height; i++)
        {
            //creates a circle at each height, separated by 1
            //  y = i -height/2 so that it stays in the center 
            Vector3[] circlePointCloud = Circle(dotPerCircle, radius, i - (height/2)); 

            for(int j = 0; j < dotPerCircle; j++)
            { 
                pointCloud[j + index] = circlePointCloud[j];
            }
            //To track the index after iterating each circle
            index += dotPerCircle;
        }

        return pointCloud;

    }


    public Vector3[] CubeVertex(float scale)
    {

        Vector3 point1 = new Vector3(1 * scale, 1 * scale, 1 * scale);
        Vector3 point2 = new Vector3(-1 * scale, 1 * scale, 1 * scale);
        Vector3 point3 = new Vector3(1 * scale, -1 * scale, 1 * scale);
        Vector3 point4 = new Vector3(-1 * scale, -1 * scale, 1 * scale);

        Vector3 point5 = new Vector3(1 * scale, 1 * scale, -1 * scale);
        Vector3 point6 = new Vector3(-1 * scale, 1 * scale, -1 * scale);
        Vector3 point7 = new Vector3(1 * scale, -1 * scale, -1 * scale);
        Vector3 point8 = new Vector3(-1 * scale, -1 * scale, -1 * scale);


        Vector3[] pointCloud = new Vector3[] { point1, point2, point3, point4, point5, point6, point7, point8 };

        return pointCloud;
    }


    public Vector3[] Circle(int numOfDots, float radius, float y)
    {
        Vector3[] pointCloud = new Vector3[numOfDots];

        for (int i = 0; i < numOfDots; i++)
        {
            // Calculate angle for each point (0 to 2pi)
            float angle = (i / (float)numOfDots) * Mathf.PI * 2f;

            // Parametric circle equations
            float x = radius * Mathf.Cos(angle);
            float z = radius * Mathf.Sin(angle);

            pointCloud[i] = new Vector3(x, y, z);
        }

        return pointCloud;

    }
}
