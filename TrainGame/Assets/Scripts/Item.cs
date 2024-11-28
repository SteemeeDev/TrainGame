using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] float returnSeconds = 0.3f;
    [SerializeField] bool fallToGround = false;
    [SerializeField] Transform holdPos;
    [SerializeField] Transform player;
    
    [HideInInspector] public bool holdItem;

    private Vector3 startPos;
    private Quaternion startRot;

    private Rigidbody Rigidbody;
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        startPos = transform.position;
        startRot = transform.rotation;
    }
    public IEnumerator LetGoOfItem()
    {
        Debug.Log("Letting go of item");
        if (fallToGround)
        {
            Rigidbody.useGravity = true;
            Rigidbody.isKinematic = false;
            Rigidbody.velocity = player.forward;
            Rigidbody.angularVelocity = new Vector3(45, 1, 1);
        }
        else if (!fallToGround)
        {
            Rigidbody.useGravity = false;
            Rigidbody.isKinematic = true;
            Vector3 letGoPos = transform.position;
            Quaternion letGoRot = transform.rotation;

            float elapsed = 0;
            float t = 0;

            while (elapsed < returnSeconds)
            {
                elapsed += Time.deltaTime;
                t = elapsed / returnSeconds;

                transform.position = Vector3.Lerp(letGoPos, startPos, Mathf.SmoothStep(0f, 1f, t));
                transform.rotation = Quaternion.Lerp(letGoRot, startRot, Mathf.SmoothStep(0f, 1f, t));

                yield return null;
            }
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        if (holdItem)
        {
            Rigidbody.useGravity = false;
            transform.position = Vector3.Lerp(transform.position, holdPos.position, 30 * Time.deltaTime);
            transform.rotation = holdPos.rotation;
        }
    }
}
