using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTrackMover : MonoBehaviour
{
    [SerializeField] Transform trackTransform;
    [SerializeField] Transform trackTransform2;


    [SerializeField] GameObject turnObj;
    [SerializeField] public Animator turnAnim;

    [SerializeField] Transform train;
    [SerializeField] public bool turn;
    [SerializeField] float maxSpeed = 20;
    [SerializeField] float speed = 20;
    [SerializeField] float stepSize1;
    [SerializeField] float stepSize2;

    public bool turning = false;
    bool a = false; //Avoid checking the same thing every frame

    float t;

    private void Awake()
    {
        Debug.Log("moving ground!");
    }
    // Update is called once per frame
    void Update()
    {

        t = speed / maxSpeed;
        turnAnim.speed = t;


        if (!turning)
        {
            trackTransform.position += trackTransform.forward * -speed * Time.deltaTime;
            trackTransform2.position += trackTransform2.forward * -speed * Time.deltaTime;
        }

        if (trackTransform2.position.z < train.position.z && a == false)
        {
            trackTransform.position = trackTransform2.position + trackTransform.forward * stepSize2;
            if (turn)
            {
                turnObj.SetActive(true);
                trackTransform.gameObject.SetActive(false);
        
                turn = false;
            }
            a = true;
        }
        if (trackTransform.position.z < train.position.z && a == true)
        {
            trackTransform2.position = trackTransform.position + trackTransform2.forward * stepSize1;
            a = false;
        }

        if (turnAnim.GetCurrentAnimatorStateInfo(0).IsName("TurnLeft") || turnAnim.GetCurrentAnimatorStateInfo(0).IsName("TurnRight"))
        {
            trackTransform2.gameObject.SetActive(false);
            trackTransform.gameObject.SetActive(false);
            turning = true;
        }
        if (turning && turnAnim.GetCurrentAnimatorStateInfo(0).IsName("Nothing"))
        {
            turnObj.SetActive(false);
            trackTransform.gameObject.SetActive(true);
            trackTransform2.gameObject.SetActive(true);
            turn = false;
            turning = false;
         
        }
    }
}
