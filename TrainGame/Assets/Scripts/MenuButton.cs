using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] float hoverSizeIncrease = 10;
    [SerializeField] float easingTime = 0.15f;


    TMP_Text m_text;
    float startFontSize;
    public void Awake()
    {
        m_text = GetComponent<TMP_Text>();
        startFontSize = m_text.fontSize;
    }

    public virtual void OnPointerClick(PointerEventData pointerEventData)
    {
        //Some basic button functionality
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        StartCoroutine(smoothHover(easingTime));
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        StartCoroutine(smoothExit(easingTime));
    }

    IEnumerator smoothHover(float smoothTime)
    {
        float elapsed = 0;
        float t;
        while (elapsed < smoothTime)
        {
            elapsed += Time.deltaTime;
            t = elapsed / smoothTime;
            m_text.fontSize = Mathf.Lerp(startFontSize, startFontSize + hoverSizeIncrease, 1 - Mathf.Pow(1 - t, 4));
            yield return null;
        }
    }
    IEnumerator smoothExit(float smoothTime)
    {
        float elapsed = smoothTime;
        float t;
        while (elapsed > 0)
        {
            elapsed -= Time.deltaTime;
            t = elapsed / smoothTime;
            m_text.fontSize = Mathf.Lerp(startFontSize, startFontSize + hoverSizeIncrease, 1 - Mathf.Pow(1 - t, 4));
            yield return null;
        }
    }
}