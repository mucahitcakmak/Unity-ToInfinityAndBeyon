using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyCounter : MonoBehaviour
{
    TextMeshProUGUI moneyTxt;

    void Awake()
    {
        moneyTxt = GetComponent<TextMeshProUGUI>();
    }

    
    void Update()
    {
        moneyTxt.text = SaveManager.instance.money.ToString();
    }

}
