using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Visitor : MonoBehaviour
{
    private Vector3 destination;
    private float entryThreshold = 3.5f;
    private PointOfInterest currentPOI = null;
    public NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    PointOfInterest GetRandomPOI()
    {
        var POIs = Object.FindObjectsOfType<PointOfInterest>();
        Debug.Assert(POIs != null && POIs.Length > 0, "POIs array is null or empty.");

        int randomIndex = Random.Range(0, POIs.Length);
        return POIs[randomIndex];
    }

    IEnumerator MoveToNextPOI()
    {

        while (true)
        {

            // Met à jour la position de la file d'attente
            currentPOI.UpdateQueuePositions(this);

            const float playTime = 3.0f;
            float distToEntrance = Vector3.Distance(transform.position, currentPOI.Entrance.position);


            if (distToEntrance < entryThreshold && !currentPOI.IsFull()) // visitor can enter
            {   

                // Entering the POI
                GetComponent<Renderer>().enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;
                agent.enabled = false;
                currentPOI.InPoint(this);

                yield return new WaitForSeconds(playTime);

                // Exiting the POI
                transform.position = currentPOI.Exit.position;
                agent.enabled = true;
                GetComponent<Renderer>().enabled = true;
                GetComponent<CapsuleCollider>().enabled = true;
                currentPOI.OutofPoint();
                currentPOI = GetRandomPOI();
                currentPOI.JoinQueue(this);


            }

            yield return null;
        }
    }
    
   


    void Start()
    {
        var POIs = Object.FindObjectsOfType<PointOfInterest>();

        if (POIs.Length > 0)
        {
            currentPOI = GetRandomPOI();
            currentPOI.JoinQueue(this);


        }

        StartCoroutine(MoveToNextPOI());
    }
}
