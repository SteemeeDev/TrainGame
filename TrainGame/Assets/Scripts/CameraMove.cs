using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float Sensititivity;

    // Player gameobject (parent)
    public Transform player;

    float xRotation;
    float yRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        xRotation += mouseX * Sensititivity;
        yRotation -= mouseY * Sensititivity;

        yRotation = Mathf.Clamp(yRotation, -90, 90);

        transform.rotation = Quaternion.Euler(yRotation, xRotation, 0);
        player.rotation = Quaternion.Euler(0, xRotation, 0);
    }
}
