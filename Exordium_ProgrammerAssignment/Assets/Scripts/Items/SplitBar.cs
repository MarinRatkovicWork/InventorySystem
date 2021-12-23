using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SplitBar : MonoBehaviour
{
    public void AddAndSubtractSplitBar(bool add)
    {
        if(add == true)
        {
            gameObject.GetComponent<Slider>().value = gameObject.GetComponent<Slider>().value +1;
        }
        else if (add == false)
        {
            gameObject.GetComponent<Slider>().value = gameObject.GetComponent<Slider>().value -1;
        }
    }
    public void UpdateSplitValueWithSlider(GameObject moveStack)
    {
        GameObject splitBar = this.gameObject;
        moveStack.GetComponent<TMP_Text>().text = splitBar.GetComponent<Slider>().value.ToString();
    }
}
