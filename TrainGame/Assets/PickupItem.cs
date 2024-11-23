using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    //Layer for all pickuppable items
    [SerializeField] LayerMask ItemLayer;

    RaycastHit hit;
    bool holdItem = false;
    bool letGoOfKey = false;

    private IEnumerator returnItem; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !holdItem)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5, ItemLayer, QueryTriggerInteraction.Collide))
            {
                // Grab item and initialise coroutine
                returnItem = hit.transform.gameObject.GetComponent<Item>().returnToStartPos(3);
                hit.transform.gameObject.GetComponent<Item>().holdItem = true;

                Debug.Log("Picking up item" + hit.transform.gameObject.name);
                holdItem = true;
            }
        }

        //Wait for user to let go of key
        if (Input.GetKeyUp(KeyCode.E) && holdItem)
        {
            letGoOfKey = true;
        }

        //Let of of item
        if (letGoOfKey && Input.GetKeyDown(KeyCode.E))
        {
            hit.transform.gameObject.GetComponent<Item>().holdItem = false;
            StartCoroutine(returnItem);
            holdItem = false;
            letGoOfKey = false;
        }

        //Debug
        Debug.DrawRay(transform.position, transform.forward*5, Color.magenta);
    }
}
