using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTurn : MonoBehaviour
{
    [SerializeField] Vector3 targetPoint;
    [SerializeField] Animator turn;
    [SerializeField] FlipLever leverFlip;
    [SerializeField] float speed = 20;
    [SerializeField] float stepSize1;

    Vector3 startPoint;

    private void Awake()
    {
        startPoint = transform.position;
        Debug.Log("moving ground!");
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,0,-speed * Time.deltaTime);


        if (transform.position.z < targetPoint.z)
        {
            transform.position = startPoint;
            if (leverFlip.leverFlipped == true)
            {
                turn.SetTrigger("Turn right");
            }
            else
            {
                turn.SetTrigger("TurnLeft");
            }
            transform.gameObject.SetActive(false);
        }
        
    }
}
