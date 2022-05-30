using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject followerPrefab;
    [SerializeField]
    float spawnInterval = 5.0f;
    [SerializeField]
    GameObject[] spawnGroupLocations;
    [SerializeField]
    int initialSpawnCount = 10;
    [SerializeField]
    int spawnCount = 0;
    [SerializeField]
    int maxSpawnCount = 20;
    [SerializeField]
    float spawnLocationVariance = 5f;

    [SerializeField]
    public List<Follower> followers = new List<Follower> { };

    [SerializeField]
    List<string> followerNamesMale = new List<string>{ "John", "James", "Carl", "Frank", "Lou", "Jessop", "Harold" };
    [SerializeField]
    List<string> followerNamesFemale = new List<string>{ "Jane", "Carla", "Maisie", "Frida", "Blair", "Lisa", "Fran" };

    [SerializeField]
    bool isMale = true;

    [SerializeField]
    List<string> followerSurnames = new List<string>{ "Voight", "Smith", "Barnaby", "Fredrickson", "Bobby", "Dubois", "Stone" };

    string GenerateFollowerName()
    {
        return gameObject.name = (isMale ? followerNamesMale[Random.Range(0, followerNamesMale.Count)] : followerNamesFemale[Random.Range(0, followerNamesFemale.Count)]) + " of House " + followerSurnames[Random.Range(0, followerSurnames.Count)];
    }

    private void OnEnable()
    {
        Actions.OnGameStarted += OnStart;
        Actions.OnGameOver += OnGameOver;
        /*
        Actions.OnSpawn += OnSpawn;
        Actions.OnPreach += OnPreach;
        Actions.OnUnpreach += OnUnpreach;
        Actions.OnTouch += OnTouch;
        */
    }

    private void OnDisable()
    {
        Actions.OnGameStarted -= OnStart;
        Actions.OnGameOver -= OnGameOver;
        /*
        Actions.OnSpawn -= OnSpawn;
        Actions.OnPreach -= OnPreach;
        Actions.OnUnpreach -= OnUnpreach;
        Actions.OnTouch -= OnTouch;
        */
    }

    // Start is called before the first frame update
    void OnStart()
    {
        if(followers.Count > 0)
        {
            foreach(Follower f in followers)
            {
                Destroy(f.gameObject);
            }
            followers.Clear();
        }
        for(int i = 0; i < initialSpawnCount; i++)
        {
            Spawn();
        }
        StartCoroutine(IntervalSpawn());
    }

    IEnumerator IntervalSpawn()
    {
        if (spawnCount < maxSpawnCount)
        {
            yield return new WaitForSeconds(spawnInterval);

            Spawn();

            StartCoroutine(IntervalSpawn());
        }
    }

    void Spawn()
    {
        GameObject followerGhost = Instantiate(followerPrefab, spawnGroupLocations[Random.Range(0, spawnGroupLocations.Length - 1)].transform);
        followerGhost.transform.localPosition += new Vector3(Random.Range(-spawnLocationVariance, spawnLocationVariance), Random.Range(-spawnLocationVariance, spawnLocationVariance), 0f);
        followerGhost.name = GenerateFollowerName();
        followerGhost.GetComponent<Follower>().isMale = isMale;
        followers.Add(followerGhost.GetComponent<Follower>());
        spawnCount += 1;
    }

    void OnGameOver()
    {
        StopCoroutine(IntervalSpawn());
    }
}
