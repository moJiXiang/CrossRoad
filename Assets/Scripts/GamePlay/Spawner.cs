using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> spawnObjects;
    public int direction;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Spawn), 0.2f, Random.Range(5f, 7f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        var index = Random.Range(0, spawnObjects.Count);
        GameObject car = Instantiate(spawnObjects[index], transform.position, Quaternion.identity, transform);
        
        car.GetComponent<MoveForward>().dir = direction;
    }
}
