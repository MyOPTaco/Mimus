using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public string checkpointID;
    public Transform Player;
    public float XSave, YSave, ZSave;
    //trigger this function upon interaction with objective, SHOULD save and load location and progress of the player upon interaction with object
    void SaveLoc()
    {
        XSave = Player.transform.position.x;
        YSave = Player.transform.position.y;
        ZSave = Player.transform.position.z;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt(checkpointID, 1);
        PlayerPrefs.SetFloat("X", XSave);
        PlayerPrefs.SetFloat("Y", YSave);
        PlayerPrefs.SetFloat("Z", ZSave);
    }
   
}
