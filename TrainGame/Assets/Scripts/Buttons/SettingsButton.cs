using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SettingsButton : MenuButton
{
    [SerializeField] GameObject settingsObj;
    [SerializeField] GameObject mainMenuObj;

    [SerializeField] bool inGame = false;
    [SerializeField] PlayerMenuManager menuManager;
    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        base.OnPointerClick(pointerEventData);
        settingsObj.SetActive(true);
        mainMenuObj.SetActive(false);

        if (inGame)
        {
            try
            {
                menuManager.activeMenu = settingsObj;
            }
            catch
            {
                Debug.Log("Menu manager not asssigned!");
            }
        }
    }
}
