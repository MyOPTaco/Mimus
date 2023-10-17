using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorCol : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Door")
        {
            if(other.GetComponent<AutomicDoor>().Moving == false)
            {
                other.GetComponent<AutomicDoor>().Moving = true;
            }
        }
    }
}
