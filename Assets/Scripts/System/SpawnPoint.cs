using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject exitPrefab;
    void Start()
    {
        SpawnExit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnExit()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(exitPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
    }
}
