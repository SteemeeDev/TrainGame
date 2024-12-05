using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMover : MonoBehaviour
{
    [SerializeField] Transform groundTransform;
    [SerializeField] Transform groundTransform2;
    [SerializeField] Transform train;
    [SerializeField] float speed = 20;
    [SerializeField] float stepSize;

    private void Awake()
    {
        Debug.Log("moving ground!");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        groundTransform.position  += groundTransform.forward * -speed * Time.fixedDeltaTime;
        groundTransform2.position += groundTransform2.forward * -speed * Time.fixedDeltaTime;


        if (groundTransform2.position.z < train.position.z)
        {
            groundTransform.position = groundTransform2.position + groundTransform.forward * stepSize;
        }
        if (groundTransform.position.z < train.position.z)
        {
            groundTransform2.position = groundTransform.position + groundTransform2.forward * stepSize;
        }
    }
}
