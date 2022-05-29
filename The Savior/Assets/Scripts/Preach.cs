using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Preach : MonoBehaviour
{

    public float maxFollowDistance = 100f;

    [SerializeField]
    float preachDuration = 2.0f;

    [SerializeField]
    bool preaching = false;
    
    [SerializeField]
    ParticleSystem pulse;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if (!GetComponent<MainCharacterMovement>().isMoving && !preaching)
                DoPreach();
        }
    }

    void DoPreach()
    {
        StartCoroutine(PreachCoroutine());
    }

    IEnumerator PreachCoroutine()
    {
        Debug.Log("Preach!");
        
        preaching = true;

        pulse.Play();
        Actions.DoPreach();

        yield return new WaitForSeconds(preachDuration);

        preaching = false;
    }
}
