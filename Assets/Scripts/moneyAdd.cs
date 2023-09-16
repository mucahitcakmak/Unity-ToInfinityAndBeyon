using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneyAdd : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H)){
            SaveManager.instance.money += 100; 
            SaveManager.instance.Save();
        }else if(Input.GetKeyDown(KeyCode.J)){
            SaveManager.instance.money -= 100; 
            SaveManager.instance.Save();
        }
    }

}
