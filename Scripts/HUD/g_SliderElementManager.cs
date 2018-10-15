using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class g_SliderElementManager : MonoBehaviour {
    [SerializeField]
    Slider slider;
    [SerializeField]
    Button button;
    [SerializeField]
    Text costText;
    [SerializeField]
    float[] costList;
    float totalCost;
    PlayerMoneyScript moneyScript;

    void OnEnable()
    {
        moneyScript = GameObject.Find("Player").GetComponent<PlayerMoneyScript>();
       
    }
    
    void Start()
    {
        totalCost = costList[0];
    }
    void Update()
    {
        if (moneyScript.money >= totalCost)
        {
            button.GetComponent<BoxCollider>().enabled = true;
            costText.color = new Color(255, 120, 0);
        }
        else
        {
            button.GetComponent<BoxCollider>().enabled = false;
            costText.color = Color.red;
        }
    }

    public void UpdatePlayerMoney()
    {
        if (moneyScript != null)
        moneyScript.DecreaseMoney((int)totalCost);
    }

    public void UpdateTotalCost()
    {
        if (slider.value < slider.maxValue)
        {
            totalCost = costList[(int)slider.value];
            costText.text = "$" + totalCost.ToString();
        }
        else
        {
            costText.text = "MAX";
        }

    }
    

}
