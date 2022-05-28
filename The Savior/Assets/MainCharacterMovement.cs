using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterMovement : MonoBehaviour
{
    public float speedMultiplier = 0.005f;
    public float minMoveMagnitude = 100;
    public float maxMoveMagnitude = 1000;
    float stop = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            Debug.Log("Mouse position: " + getMouse());
            Debug.Log("Mouse position magnitude: " + getMouse().magnitude);

        transform.position = Vector3.MoveTowards(transform.position, getMouse(), 
            (getMouse().magnitude > minMoveMagnitude ?
                getMouse().magnitude < maxMoveMagnitude ?
                    getMouse().magnitude - (minMoveMagnitude * 0.5f)
                    : maxMoveMagnitude
                : stop) * speedMultiplier * Time.deltaTime);
    }
    Vector3 getMouse()
    {
        return Input.mousePosition - ((Vector3.one * 512) - (Vector3.forward * 512));
    }
}
