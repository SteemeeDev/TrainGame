using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTrackMover : MonoBehaviour
{
    [SerializeField] Transform trackTransform;
    [SerializeField] Transform trackTransform2;


    [SerializeField] GameObject turnObj;
    [SerializeField] Animator turnAnim;

    [SerializeField] Transform train;
    [SerializeField] bool turn;
    [SerializeField] float speed = 20;
    [SerializeField] float stepSize1;
    [SerializeField] float stepSize2;

    bool turning = false;

    private void Awake()
    {
        Debug.Log("moving ground!");
    }
    // Update is called once per frame
    void Update()
    {
        if(!turn)
        {
            trackTransform.position += trackTransform.forward * -speed * Time.deltaTime;
            trackTransform2.position += trackTransform2.forward * -speed * Time.deltaTime;
        }

        if (trackTransform2.position.z < train.position.z)
        {
            if (!turn)
            {
                Debug.Log("No turning!");
                trackTransform.position = trackTransform2.position + trackTransform.forward * stepSize2;
            }
            if (turn)
            {
                Debug.Log("TURN!");
                turnObj.SetActive(true);
                trackTransform.gameObject.SetActive(false);
                trackTransform2.gameObject.SetActive(false);
            }
        }
        if (trackTransform.position.z < train.position.z)
        {
            trackTransform2.position = trackTransform.position + trackTransform2.forward * stepSize1;
        }

        if (turnAnim.GetCurrentAnimatorStateInfo(0).IsName("TurnLeft") || turnAnim.GetCurrentAnimatorStateInfo(0).IsName("TurnRight"))
        {
            turning = true;
        }
        if (turning && turnAnim.GetCurrentAnimatorStateInfo(0).IsName("Nothing"))
        {
            Debug.Log("ANIMATION DONE");
            turnObj.SetActive(false);
            trackTransform.gameObject.SetActive(true);
            trackTransform2.gameObject.SetActive(true);
            turn = false;
            turning = false;
        }
    }
}
