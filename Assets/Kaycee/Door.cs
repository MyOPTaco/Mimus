using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent DoorOpen;
    
    private void OnTriggerEnter(Collider other)
    {
        DoorOpen.Invoke();
    }
}
