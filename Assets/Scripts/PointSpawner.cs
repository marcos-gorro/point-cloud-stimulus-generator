using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PointSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject Point;
    public GameObject CenterPoint;
    public Transform Canvas;


    public enum ShapeType{ Cube, Sphere, Cylinder };

    [Header("Select a Shape")]
    public ShapeType shape;


    [Header("Customize Shape")]
    public float RPM = 15;
    public int numOfDots = 150;
    public float size = 3;
    public float radius = 3;
    public float height = 3;
    public bool hasCenterPoint;



    private Projection projector;
    private PointCloudData pointCloud;
    private Rotation rotate;
    private ZTranslation translateZ;

    private float dz = 5.0f;
    private float angle = 0;


    private Vector3[] points;

    void Start()
    {
        projector = new Projection();
        pointCloud = new PointCloudData();
        rotate = new Rotation();
        translateZ = new ZTranslation();

        switch (shape)
        {
            case ShapeType.Cube: points = pointCloud.PointCloudCube(numOfDots, size); break;
            case ShapeType.Sphere: points = pointCloud.PointCloudSphere(numOfDots, radius); break;
            case ShapeType.Cylinder: points = pointCloud.PointCloudCylinder(numOfDots, radius, height); break;
            default: points = pointCloud.PointCloudCube(numOfDots, size); break;

        }
           
        
        
        if (hasCenterPoint)
        {
            GameObject center = Instantiate(CenterPoint);
            center.transform.SetParent(Canvas.transform, false);
        }
    }

    void Update()
    {
        // Increment time-based variables ONCE per frame 
        float dt = Time.deltaTime;
        //dz += 0.5f * dt; //uncomment to translate z

        // Radians
        //Mathf.PI * dt = 30 RPM
        angle += (Mathf.PI * dt) * (RPM/30f);

        // Render the frame
        Frame(points);

    }

    void Frame(Vector3[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            // Destroy after one frame cycle
            Destroy(SpawnPoint(points[i]), 0.0266f);
        }
    }

    GameObject SpawnPoint(Vector3 position)
    {
        // performing rotation - > translation - > projeciton
        Vector3 rotated = rotate.RotateXZ(position, angle);
        Vector3 translated = translateZ.TranslateZ(rotated, dz);
        Vector3 projectedPosition = projector.Project(translated);

        GameObject point = Instantiate(Point);
        point.transform.SetParent(Canvas.transform, false);

        RectTransform rectTransform = point.GetComponent<RectTransform>();
        rectTransform.localPosition = projectedPosition;

        return point;
    }
}
