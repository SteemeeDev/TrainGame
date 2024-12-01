using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    //Layer for all item hitboxes (item parent)
    [SerializeField] LayerMask ItemHitboxLayer;
    [SerializeField] int HeldItemLayer = 9;
    [SerializeField] Camera itemCamera;
    Camera mainCamera;

    RaycastHit hit;
    bool holdItem = false;
    bool letGoOfKey = false;

    private IEnumerator returnItem;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !holdItem)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5, ItemHitboxLayer, QueryTriggerInteraction.Collide))
            {
                //Change itemcameras culling mask so that it renders the item and stop the main camera from rendering it
                itemCamera.cullingMask |= (1 << HeldItemLayer);
                mainCamera.cullingMask &= ~(1 << HeldItemLayer);

                // Grab item and initialise coroutine
                returnItem = hit.transform.gameObject.GetComponent<Item>().LetGoOfItem();
                hit.transform.gameObject.GetComponent<Item>().holdItem = true;

                Debug.Log("Picking up item " + hit.transform.gameObject.name);
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
            StartCoroutine(returnItem);

            letGoOfKey = false;
            holdItem = false;

            itemCamera.cullingMask &= ~(1 << HeldItemLayer);
            mainCamera.cullingMask |= (1 << HeldItemLayer);
        }

        //Debug
        Debug.DrawRay(transform.position, transform.forward*5, Color.magenta);
    }
}
