using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class QuitButton : MenuButton
{
    [SerializeField] bool inGame = false;
    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        base.OnPointerClick(pointerEventData);
        if (!inGame) Application.Quit();
        if (inGame) SceneManager.LoadScene("MenuScene");
    }
}
