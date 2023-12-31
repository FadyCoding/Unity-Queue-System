using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class ControlPoint : MonoBehaviour
{
    public Color color = Color.red;
    
    private float radius = 0.1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Handles.Label(transform.position + 2 * radius * Vector3.up, gameObject.name);
        Gizmos.DrawSphere(transform.position, radius);
    }
}
