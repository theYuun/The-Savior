using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject followerPrefab;
    [SerializeField]
    float spawnInterval = 5.0f;
    bool spawnHold = false;
    [SerializeField]
    GameObject[] spawnGroupLocations;
    [SerializeField]
    int initialSpawnCount = 10;
    [SerializeField]
    int maxSpawnCount = 20;
    [SerializeField]
    float spawnLocationVariance = 5f;

    Follower[] activeFollowers;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < initialSpawnCount; i++)
        {
            GameObject followerGhost = Instantiate(followerPrefab, spawnGroupLocations[Random.Range(0, spawnGroupLocations.Length - 1)].transform);
            followerGhost.transform.localPosition += new Vector3(Random.Range(-spawnLocationVariance, spawnLocationVariance), Random.Range(-spawnLocationVariance, spawnLocationVariance), 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawnHold && maxSpawnCount > 0)
            StartCoroutine(IntervalSpawn());
    }

    IEnumerator IntervalSpawn()
    {
        spawnHold = true;
        
        GameObject followerGhost = Instantiate(followerPrefab, spawnGroupLocations[Random.Range(0, spawnGroupLocations.Length - 1)].transform);
        followerGhost.transform.localPosition += new Vector3(Random.Range(-spawnLocationVariance, spawnLocationVariance), Random.Range(-spawnLocationVariance, spawnLocationVariance), 0f);
        maxSpawnCount -= 1;

        yield return new WaitForSeconds(spawnInterval);
        spawnHold = false;
    }
}
