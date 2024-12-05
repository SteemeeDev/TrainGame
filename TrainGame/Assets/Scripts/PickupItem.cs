using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    //Layer for all item hitboxes (item parent)
    [SerializeField] LayerMask ItemHitboxLayer;
    [SerializeField] LayerMask InteractionLayer;
    [SerializeField] int HeldItemLayer = 9;
    [SerializeField] Camera itemCamera;
    Camera mainCamera;

    RaycastHit itemHit;
    RaycastHit interactHit;
    bool holdItem = false;
    bool canDropItem = false;
    bool letGoOfKey = false;

    private IEnumerator returnItem;
    private IEnumerator grabItem;
    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    bool leverFlipped = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (Physics.Raycast(transform.position, transform.forward, out interactHit, 5, InteractionLayer))
            {
                Debug.Log("Flipping lever");
                canDropItem = false;

                leverFlipped = !leverFlipped;
                interactHit.transform.GetComponent<FlipLever>().leverFlipped = leverFlipped;
                Animator animator = interactHit.transform.GetComponent<FlipLever>().animator;

                animator.SetBool("LeverFlipped", leverFlipped);
            }
            else
            {
                canDropItem= true;
            }

            if (!holdItem && Physics.Raycast(transform.position, transform.forward, out itemHit, 5, ItemHitboxLayer, QueryTriggerInteraction.Collide))
            {
                //Change itemcameras culling mask so that it renders the item and stop the main camera from rendering it
                itemCamera.cullingMask |= (1 << HeldItemLayer);
                mainCamera.cullingMask &= ~(1 << HeldItemLayer);

                for (int i = 0; i < itemHit.transform.childCount; i++)
                {
                    itemHit.transform.GetChild(i).gameObject.layer = HeldItemLayer;
                } 

                // Grab item and initialise coroutine
                returnItem = itemHit.transform.gameObject.GetComponent<Item>().LetGoOfItem();
                itemHit.transform.gameObject.GetComponent<Item>().holdItem = true;
                

                Debug.Log("Picking up item " + itemHit.transform.gameObject.name);
                holdItem = true;

                canDropItem = true;
            }
        }

        //Wait for user to let go of key
        if (Input.GetKeyUp(KeyCode.E) && holdItem)
        {
            letGoOfKey = true;
        }

        //Let of of item
        if (letGoOfKey && Input.GetKeyDown(KeyCode.E) && canDropItem)
        {
            StartCoroutine(returnItem);

            letGoOfKey = false;
            holdItem = false;

            itemCamera.cullingMask &= ~(1 << HeldItemLayer);
            mainCamera.cullingMask |= (1 << HeldItemLayer);
            for (int i = 0; i < itemHit.transform.childCount; i++)
            {
                itemHit.transform.GetChild(i).gameObject.layer = 0;
            }
        }

        //Debug
        Debug.DrawRay(transform.position, transform.forward*5, Color.magenta);
    }
}
