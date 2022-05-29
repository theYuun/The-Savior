using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Follower : MonoBehaviour
{

    [SerializeField]
    string[] followerNames = {"John","James","Adam","Fred","Frank","Lou","Jessop"};

    [SerializeField]
    string[] followerSurnames = {"Johnson","Hale","Vetiver","Haroldson","Smith","Cobb","Voil"};

    void GenerateFollowerName(){
        gameObject.name = followerNames[Random.Range(0, followerNames.Length - 1)] + " " + followerSurnames[Random.Range(0, followerSurnames.Length - 1)];
    }

    [SerializeField]
    float speed = 1.0f;
    [SerializeField]
    float speedRange = 0.2f;
    
    [SerializeField]
    bool idling = false;
    [SerializeField]
    float idleDurationMin = 2.0f;
    [SerializeField]
    float idleDurationMax = 5.0f;
    [SerializeField]
    float idleDistanceMin = 3.0f;
    [SerializeField]
    float idleDistanceMax = 5.0f;

    bool following = false;

    GameObject player()
    {
        return GameObject.FindWithTag("Player");
    }

    Vector3 playerLocation()
    {
        return player().transform.position;
    }

    [SerializeField]
    float maxDistanceFromPlayer = 10f;

    [SerializeField]
    float distanceFromPlayer()
    {
        return Vector3.Distance(transform.position, playerLocation());
    }

    private void OnEnable()
    {
        Actions.DoSpawnFollower += FollowerSpawned;
        Actions.DoPreach += PreachedTo;
        Actions.DoFollow += StartFollowing;
        Actions.DoUnfollow += EndFollowing;
    }

    private void OnDisable()
    {
        Actions.DoSpawnFollower -= FollowerSpawned;
        Actions.DoPreach -= PreachedTo;
        Actions.DoFollow -= StartFollowing;
        Actions.DoUnfollow -= EndFollowing;
    }

    // Start is called before the first frame update
    void Start()
    {
        FollowerSpawned();
        speed += Random.Range(-speedRange, speedRange);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(distanceFromPlayer());
        if (following)
            Follow();
        else
        {
            if(!idling)
                StartCoroutine(Idling());
        }

        /*
        if(distanceFromPlayer() > maxDistanceFromPlayer)
        {
            EndFollowing();
        }
        */
    }

    void FollowerSpawned()
    {
        Debug.Log("Hello, my name is " + gameObject.name + ".");
        Actions.DoSpawnFollower();
    }

    void PreachedTo()
    {
        Debug.Log("Music to my ears!");
        //StartFollowing();
    }

    public void StartFollowing()
    {
        Debug.Log(gameObject.name + " is now a follower.");
        following = true;
    }

    void Follow()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerLocation(), speed * Time.deltaTime);
    }

    void EndFollowing()
    {
        Debug.Log(gameObject.name + " is no longer a follower.");
        following = false;
    }

    IEnumerator Idling()
    {
        idling = true;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(Random.Range(idleDistanceMin, idleDistanceMax), Random.Range(idleDistanceMin, idleDistanceMax), 0.0f), speed * Time.deltaTime);
        yield return new WaitForSeconds(Random.Range(idleDurationMin, idleDurationMax));
        idling = false;
    }
}
