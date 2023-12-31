using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using System.Data;
using UnityEngine.AI;
using System.Data.Common;
public class PointOfInterest : MonoBehaviour
{
    public float VisitTime = 5.0f;
    //public Vector3 position;
    public Transform Entrance;
    public Transform Exit;
    public int numVisitorsMax = 10;
    public float visitorSpacing = 2.0f;

    public Queue<Visitor> fileAttente = new Queue<Visitor>(); // File d'attente

    private Queue<Visitor> visitors = new Queue<Visitor>(); // Visiteurs dans le POI

    // Ajoute le visiteur dans la file d'attente
    public void JoinQueue(Visitor visitor)
    {
        fileAttente.Enqueue(visitor);
    }

    public void UpdateQueuePositions(Visitor visitor)
    {
        if (fileAttente.Peek() == visitor)
        {
            visitor.agent.SetDestination(Entrance.position);
        }
        else
        {
            var visitorInFront = GetVisitorInFront(visitor);
            visitor.agent.SetDestination(visitorInFront.transform.position - visitorInFront.transform.forward * visitorSpacing);
        }
    }

    // Verifie si le POI est plein
    public bool IsFull()
    {
        return visitors.Count >= numVisitorsMax;
    }

    // Si le visiteur est dans la file d'attente et qu'il est le premier
    public bool IsVisitorAtFrontOfQueue(Visitor visitor)
    {
        return fileAttente.Count > 0 && fileAttente.Peek() == visitor;
    }

    public bool isVisitorInside(Visitor visitor)
    {
        return visitors.Contains(visitor);
    }

    public void InPoint(Visitor visitor)
    {
        // Ajoute le visiteur dans la liste des visiteurs
        visitors.Enqueue(visitor);
        Visitor visitorPrevious = GetVisitorBehind(visitor);
        // Retire le visiteur de la file d'attente
        fileAttente.Dequeue();


    }

    public void OutofPoint()
    {
        visitors.Dequeue();
    }

    public Visitor GetVisitorInFront(Visitor visitor)
    {
        if (fileAttente.Count > 0 && fileAttente.Peek() != visitor)
        {
            List<Visitor> fileAttenteList = fileAttente.ToList();
            int visitorIndex = fileAttenteList.FindIndex(x => x == visitor);
            
            if (visitorIndex > 0)
            {
                return fileAttenteList[visitorIndex - 1];
            }
        }

        return visitor;
    }

    public Visitor GetVisitorBehind(Visitor visitor)
    {
        if (fileAttente.Count > 0 && fileAttente.Peek() != visitor)
        {
            List<Visitor> fileAttenteList = fileAttente.ToList();
            int visitorIndex = fileAttenteList.FindIndex(x => x == visitor);

            if (visitorIndex > 0)
            {
                return fileAttenteList[visitorIndex + 1];
            }
            
        }

        return visitor;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
