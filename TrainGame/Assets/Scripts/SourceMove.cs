using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SourceMove : MonoBehaviour
{
    public CharacterController controller;
    public Camera cam;
    public Transform groundCheck;

    public LayerMask layerMask;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public float speed = 16f;
    public float groundControl = 5f;
    public float airControl = 0.5f;
    public float jump;
    public float gravity = 9.82f;

    Vector3 moveDir;
    Vector3 velocity;
    Vector3 airDir;

    void Update()
    {
        bool grounded = Physics.CheckSphere(groundCheck.position, 0.3f, layerMask);
        Vector3 wishDir = Input.GetAxisRaw("Horizontal") * transform.right + Input.GetAxisRaw("Vertical") * transform.forward;

        wishDir = wishDir.normalized;

        Debug.DrawRay(transform.position - new Vector3(0, 0.5f, 0), wishDir, Color.magenta);
        
        if (grounded)
        {
            moveDir = Vector3.Lerp(moveDir, wishDir, groundControl * Time.deltaTime);
            airDir = Vector3.zero;
        }
        else
        {
            //Keep going in the same direction after jump, if move key is pressed
            airDir.x = wishDir.x != 0 ? wishDir.x : airDir.x;
            airDir.z = wishDir.z != 0 ? wishDir.z : airDir.z;

            moveDir = Vector3.Lerp(moveDir, airDir, airControl * Time.deltaTime);
        }
        controller.Move(moveDir * Time.deltaTime * speed);

        if (grounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = jump;
            }
            if (velocity.y > 0)
            {
                velocity.y -= 2f*Time.deltaTime;
            }

        }else
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        controller.Move(velocity*Time.deltaTime);
    }
}
