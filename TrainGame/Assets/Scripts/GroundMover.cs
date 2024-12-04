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
    void Update()
    {
        groundTransform.position += new Vector3(0, 0, -speed*Time.deltaTime);
        groundTransform2.position += new Vector3(0, 0, -speed*Time.deltaTime);

        
        if (groundTransform2.position.z < train.position.z)
        {
            groundTransform.position = groundTransform2.position + new Vector3(0, 0, stepSize);
        }
        if (groundTransform.position.z < train.position.z)
        {
            groundTransform2.position = groundTransform.position + new Vector3(0, 0, stepSize);
        }
    }
}
