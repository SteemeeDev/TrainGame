using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    public GameObject activeMenu;
    [SerializeField] Slider sensitivtySlider;
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioSource ambience;
    CameraMove CameraMove;

    public bool menuOpen = false;

    private void Awake()
    {
        CameraMove = GetComponent<CameraMove>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                activeMenu = mainMenu;
                mainMenu.SetActive(true);
                CameraMove.canMove = false;
                menuOpen = true;
                return;
            }
            else if (menuOpen)
            {
                closeMenu();
            }
        }
        if (menuOpen)
        {
            ambience.volume = volumeSlider.value;
        }
    }

    public void closeMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        activeMenu.SetActive(false);
        mainMenu.SetActive(false);
        CameraMove.canMove = true;
        CameraMove.Sensititivity = sensitivtySlider.value;
        menuOpen = false;
    }
}
