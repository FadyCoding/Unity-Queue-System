using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    public GameObject ReferenceVisitor;


    public static GameManager instance;
    public int numOfVisitors = 30;
    public List<GameObject> visitors;

    [SerializeField] GameObject spawnAreaGO;

    void Start()
    {
        instance = this;

        visitors = new List<GameObject>();
        
        Bounds spawnArea = spawnAreaGO.GetComponent<MeshRenderer>().bounds;
        
        for(int i = 0; i < numOfVisitors; i++)
        {
            // Random position
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnArea.min.x, spawnArea.max.x),
                0,
                Random.Range(spawnArea.min.z, spawnArea.max.z)
            );
            // Reference visitor
            GameObject visitor = Instantiate(ReferenceVisitor, randomPosition, Quaternion.identity);
            visitor.SetActive(true);
            visitor.name = $"visitor{i+1}";

            visitors.Add(visitor);
        }
        
    }
}
