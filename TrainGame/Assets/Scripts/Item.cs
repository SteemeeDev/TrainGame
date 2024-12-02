using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] float returnSeconds = 0.3f;
    [SerializeField] Transform holdPos;
    [SerializeField] public Transform player;
   // SourceMove playerMove; 
    
    public bool holdItem;

    private Vector3 startPos;
    private Quaternion startRot;

    public Rigidbody m_Rigidbody;
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
       // playerMove = player.GetComponent<SourceMove>();
        startPos = transform.position;
        startRot = transform.rotation;
    }

    public virtual IEnumerator LetGoOfItem()
    {
        Debug.Log("Letting go of item");

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

    // Update is called once per frame
    public virtual void Update()
    {
        if (holdItem)
        {
            m_Rigidbody.useGravity = false;
            /*// TODO fix flashlight not being visible when moving forwards
            if (playerMove.wishDir.magnitude > 0) {
                transform.position = holdPos.position;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, holdPos.position, 30 * Time.deltaTime);
            }*/
            transform.position = Vector3.Lerp(transform.position, holdPos.position, 30 * Time.deltaTime);
            transform.rotation = holdPos.rotation;
        }

    }
}
