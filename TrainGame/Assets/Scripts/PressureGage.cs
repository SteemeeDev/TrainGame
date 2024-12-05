using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureGage : MonoBehaviour
{
    [SerializeField] public float maxPressure = 100;
    [SerializeField] public float currentPressure = 0;
    [SerializeField] public float height = 1;

    [SerializeField, Tooltip("How fast the pressure should decay per second")] 
    public float decay = 1;

    float t = 0;
    Vector3 startPos;

    private void Awake()
    {
            startPos = transform.position;
    }

    private void FixedUpdate()
    {
        currentPressure = Mathf.Clamp(currentPressure, 0, maxPressure);
        t = currentPressure / maxPressure;

        transform.localScale = new Vector3(transform.localScale.z, t * height, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, startPos.y + t * height * 0.5f, transform.position.z);

        currentPressure -= decay * Time.fixedDeltaTime;
    }
}
