using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatBar : MonoBehaviour
{


    [SerializeField]
    private float fillAmount;

    [SerializeField]
    private Image content;

    /*
    [SerializeField]
    Gun[] gun;
    */

    public float maxValue { get; set; }
    public float value
    {
        set
        {
            fillAmount = Map(value, 0, maxValue, 0, 1);
        }

    }


    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }


    private void HandleBar()
    {
        if (fillAmount != content.fillAmount)
        {
            content.fillAmount = fillAmount;
        }

    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {

        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
