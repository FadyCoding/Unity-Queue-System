using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Bezier : MonoBehaviour
{
    private List<ControlPoint> controlsPoints = new List<ControlPoint>();



    private void CreateControlPoint()
    {
        do
        {
            GameObject controlPoint = new GameObject($"P{controlsPoints.Count}");
            controlPoint.transform.parent = transform;
            controlPoint.AddComponent<ControlPoint>();
            controlPoint.GetComponent<ControlPoint>().color = Color.red;
            controlsPoints.Add(controlPoint.GetComponent<ControlPoint>());

        } while (controlsPoints.Count < 4);
    }


    private void DrawBezier()
    {
        int visitorsCount = GameManager.instance.numOfVisitors;
        
        for(int i = 0; i < visitorsCount; i++)
        {
            float t = i / (float)visitorsCount;
            Vector3 visitorPos = ComputeBerstein(controlsPoints, t);
            GameManager.instance.visitors[i].transform.position = visitorPos;
        }

    }

    private Vector3 ComputeBerstein(List<ControlPoint> controlPoints, float t)
    {
        return controlPoints[0].transform.position * (-1 * (t * t * t) + 3 * t * t - 3 * t + 1) // P0
            + controlPoints[1].transform.position * (3 * (t * t * t) - 6 * t * t + 3 * t) // P1
            + controlPoints[2].transform.position * (-3 * (t * t * t) + 3 * t * t) // P2
            + controlPoints[3].transform.position * t * t * t; // P3
    }

    void Start()
    {
        CreateControlPoint();
    }

    void Update() 
    {
        DrawBezier();
    }
}