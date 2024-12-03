using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    [SerializeField] Transform holdPos;
    [SerializeField] public Transform player;
    SourceMove playerMove; 
    
    public bool holdItem;


    private Vector3 startPos;
    private Quaternion startRot;

    public Rigidbody m_Rigidbody;

    [SerializeField] float returnSeconds = 0.3f;

    [Header("Smoothing settings")]
    [SerializeField, Tooltip("Whether or not the item should lag behind")] bool smooth;
    [SerializeField] float smoothDelay;

    public virtual void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        playerMove = player.GetComponent<SourceMove>();
        startPos = transform.position;
        startRot = transform.rotation;
    }

    [HideInInspector] public bool returned = false;

    public virtual IEnumerator LetGoOfItem()
    {
        Debug.Log("Letting go of item");

        returned = false;
        holdItem = false;

        m_Rigidbody.useGravity = false;
        m_Rigidbody.isKinematic = true;
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


    Vector3 zero = Vector3.zero;
    public virtual void Update()
    {
        if (holdItem && smooth)
        {
            m_Rigidbody.useGravity = false;
            m_Rigidbody.isKinematic = false;
            if (playerMove.wishDir.magnitude <= 0.01f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, holdPos.position, ref zero, smoothDelay);
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, holdPos.position, ref zero, smoothDelay / 3);
            }
            transform.rotation = holdPos.rotation;
        }
        if (holdItem && !smooth)
        {
            m_Rigidbody.useGravity = false;
            m_Rigidbody.isKinematic = false;
            if (Vector3.Distance(transform.position, holdPos.position) > 0.3f && !returned)
            {
                transform.position = Vector3.Lerp(transform.position, holdPos.position, 20 * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, holdPos.rotation, 20 * Time.deltaTime);
            }else
            {
                returned = true;
            }
            if (returned)
            {
                transform.position = holdPos.position;
                transform.rotation = holdPos.rotation;
            }
        }
    }
}
