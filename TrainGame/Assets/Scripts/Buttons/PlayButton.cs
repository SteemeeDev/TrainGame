using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayButton : MenuButton
{
    [SerializeField, Header("Only for ingame buttons")] bool isInGame = false;
    [SerializeField] PlayerMenuManager menuManager;

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        base.OnPointerClick(pointerEventData);

        if (!isInGame) { SceneManager.LoadScene("MainScene"); return; }

        if (isInGame)
        {
            try
            {
                menuManager.closeMenu();
            }
            catch
            {
                Debug.Log("Menu manager not assigned!");
            }
            
        }
    }
}
