using UnityEngine;
using UnityEngine.EventSystems;

public class BackButton : MenuButton
{
    [SerializeField] GameObject returnObj;
    [SerializeField] GameObject parentObj;
    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        base.OnPointerClick(pointerEventData);

        returnObj.SetActive(true);
        parentObj.SetActive(false);
    }
}
