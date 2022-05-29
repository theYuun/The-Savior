using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterMovement : MonoBehaviour
{

    [SerializeField]
    float speedMultiplier = 0.005f;
    
    public float minMoveMagnitude = 100f;
    public float maxMoveMagnitude = 1000f;

    public Vector3 getRelativeMousePosition()
    {
        return Input.mousePosition - ((Vector3.one * 512) - (Vector3.forward * 512));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public bool isMoving = false;
    public bool isMovingFastest = false;

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, getRelativeMousePosition(), 
            (CanIMove() ? NotFastest() ? Moving() : Fastest() : Stop()) * speedMultiplier * Time.deltaTime);
    }

    bool CanIMove()
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

}
