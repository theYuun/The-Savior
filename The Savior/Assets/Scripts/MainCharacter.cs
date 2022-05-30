using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{

    [SerializeField]
    float speedMultiplier = 0.005f;
    
    public float minMoveMagnitude = 100f;
    public float maxMoveMagnitude = 1000f;

    public Vector3 getRelativeMousePosition()
    {
        return Input.mousePosition - ((Vector3.one * 512) - (Vector3.forward * 512));
    }

    public bool isMoving = false;
    public bool isMovingFastest = false;

    [SerializeField]
    ParticleSystem pulse;

    bool gameOver = true;

    private void OnEnable()
    {
        Actions.OnPreach += Preach;
        Actions.OnGameStarted += OnGameStarted;
        Actions.OnGameOver += OnGameOver;
        /*
        Actions.OnSpawn += OnSpawn;
        Actions.OnUnpreach += OnUnpreach;
        Actions.OnTouch += OnTouch;
        */
    }

    private void OnDisable()
    {
        Actions.OnPreach -= Preach;
        Actions.OnGameStarted -= OnGameStarted;
        Actions.OnGameOver -= OnGameOver;
        /*
        Actions.OnSpawn -= OnSpawn;
        Actions.OnUnpreach -= OnUnpreach;
        Actions.OnTouch -= OnTouch;
        */
    }

    // Start is called before the first frame update
    void OnGameStarted()
    {
        Debug.Log("Started Main Character");
        gameOver = false;
    }

    void OnGameOver()
    {
        Debug.Log("Game over Main Character");
        gameOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            Move();
            if(Input.GetMouseButtonDown(0))
            {
                if (!isMoving)
                {
                   Actions.OnPreach.Invoke();
                }
            }
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, getRelativeMousePosition(), 
            (canIMove() ? NotFastest() ? Moving() : Fastest() : Stop()) * speedMultiplier * Time.deltaTime);
    }

    bool canIMove()
    {
        return getRelativeMousePosition().magnitude > minMoveMagnitude;
    }

    bool NotFastest()
    {
        isMovingFastest = false;
        return getRelativeMousePosition().magnitude < maxMoveMagnitude;
    }

    float Moving()
    {
        isMoving = true;
        return getRelativeMousePosition().magnitude - (minMoveMagnitude * 0.5f);
    }

    float Fastest()
    {
        isMovingFastest = true;
        return maxMoveMagnitude;
    }

    float Stop()
    {
        isMoving = false;
        isMovingFastest = false;
        return 0f;
    }

    void Preach()
    {
        pulse.Play();
    }
}
