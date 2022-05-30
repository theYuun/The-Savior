using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Follower : MonoBehaviour
{
    [SerializeField]
    float speed = 1.0f;
    [SerializeField]
    float speedRange = 0.2f;

    [SerializeField]
    public bool isMale = true;

    Vector3 playerLocation()
    {
        return GameObject.FindWithTag("Player").transform.position;
    }

    [SerializeField]
    float distanceFromPlayer()
    {
        return Vector3.Distance(transform.position, playerLocation());
    }

    [SerializeField]
    float maxFollowDistance = 2f;

    [SerializeField]
    float maxPreachDistance = 0.9f;

    [SerializeField]
    float minTouchDistance = 0.2f;

    [SerializeField]
    bool preached = false;

    private void OnEnable()
    {
        Actions.OnPreach += OnPreach;
        Actions.OnUnpreach += OnUnpreach;
        Actions.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        Actions.OnPreach -= OnPreach;
        Actions.OnUnpreach -= OnUnpreach;
        Actions.OnGameOver -= OnGameOver;
    }

    // Start is called before the first frame update
    void Start()
    {
        speed += Random.Range(-speedRange, speedRange);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("distanceFromPlayer: " + distanceFromPlayer());
        if (preached)
        {
            if (distanceFromPlayer() < maxFollowDistance)
            {
                Follow();
                if(distanceFromPlayer() < minTouchDistance)
                {
                    Debug.Log(gameObject.name + " touched you. He has seen you are not the savior and has stopped following you.");
                    Actions.OnTouch.Invoke();
                    //Not Invoking the action, because that will make all followers stop following
                    OnUnpreach();
                }
            }
            else
            {
                Debug.Log("You ran too far away from " + gameObject.name + ". He has stopped following you.");
                //Not Invoking the action, because that will make all followers stop following
                OnUnpreach();
            }
        }
    }

    void OnPreach()
    {
        if (distanceFromPlayer() < maxPreachDistance && !preached)
        {
            Debug.Log(gameObject.name + " sees the truth in your words. He is now following you.");
            Actions.OnIncreaseFollowerCount();
            preached = true;
        }
    }

    void OnUnpreach()
    {
        Actions.OnDecreaseFollowerCount();
        preached = false;
    }

    void Follow()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerLocation(), speed * Time.deltaTime);
    }

    void OnGameOver()
    {
        preached = false;
    }
}
