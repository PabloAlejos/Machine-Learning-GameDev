using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverEffectMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Text theText;
    public Color baseColor;
    public Color hoverColor;


    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = hoverColor; //Or however you do your color
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = baseColor; //Or however you do your color
    }
}