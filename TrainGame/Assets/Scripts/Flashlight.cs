using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Item
{
    [SerializeField] Light m_light;
    public override IEnumerator LetGoOfItem()
    {
        Debug.Log("Letting go of item");

        holdItem = false;

        m_Rigidbody.useGravity = true;
        m_Rigidbody.isKinematic = false;
        m_Rigidbody.velocity = player.forward * 5 + player.up*3;

        yield return null;
    }

    public override void Update()
    {
        base.Update();
        if (holdItem && Input.GetMouseButtonDown(0))
        {
            m_light.enabled = !m_light.enabled;
        }
    }
}
