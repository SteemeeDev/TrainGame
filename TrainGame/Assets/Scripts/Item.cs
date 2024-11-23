using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] Transform holdPos;
    [SerializeField] Transform player;
    public bool holdItem;
    private Vector3 startPos;
    private Quaternion startRot;
    private void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }
    public IEnumerator returnToStartPos(float lerpDuration)
    {
        Debug.Log("Letting go of item");
        float elapsed = 0;
        while (elapsed < lerpDuration/2-1f)
        {
            elapsed += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, startPos, Mathf.SmoothStep(0f, 1f, elapsed));
            transform.rotation = Quaternion.Lerp(transform.rotation, startRot, Mathf.SmoothStep(0f, 1f, elapsed));

            yield return null;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (holdItem)
        {
            transform.position = Vector3.Lerp(transform.position, holdPos.position, 30 * Time.deltaTime);
            transform.rotation = player.rotation;
        }
    }
}
