using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;

public class SaveManager : MonoBehaviour
{

    public static SaveManager instance { get; private set; }

    public int currentFlight;
    public int money;
    public bool[] flightUnlocked = new bool[5] {true, false, false, false, false};
    public bool[] selected = new bool[5] {true, false, false, false, false};
    public float shipSensibility;
    
    void Awake(){
        if(instance != null && instance != this) Destroy(gameObject);
        else instance = this;

        DontDestroyOnLoad(gameObject);
        Load();
    }


    public void Load()
    {
        if(File.Exists(Application.persistentDataPath+"/playerInfo.dat")){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath+"/playerInfo.dat", FileMode.Open);
            PlayerData_Storage data = (PlayerData_Storage)bf.Deserialize(file);

            currentFlight = data.currentFlight;
            money = data.money;
            flightUnlocked = data.flightUnlocked;
            selected = data.selected;
            shipSensibility = data.shipSensibility;

            if(data.flightUnlocked == null) flightUnlocked = new bool[5]{true, false, false, false, false};
            if(data.selected == null) selected = new bool[5]{true, false, false, false, false};

            file.Close();
        }
    }


    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath+"/playerInfo.dat");
        PlayerData_Storage data = new PlayerData_Storage();

        data.currentFlight = currentFlight;
        data.money = money;
        data.flightUnlocked = flightUnlocked;
        data.selected = selected;
        data.shipSensibility = shipSensibility;

        bf.Serialize(file, data);
        file.Close();
    } 


    public void resetData(){
        PlayerPrefs.DeleteAll ();   
        Application.LoadLevel ("menuScene");
    }

}

[System.Serializable]
class PlayerData_Storage{
    public int currentFlight;
    public int money;
    public bool[] flightUnlocked;
    public bool[] selected;
    public float shipSensibility;
}  
