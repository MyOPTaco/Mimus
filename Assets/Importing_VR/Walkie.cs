using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Walkie : MonoBehaviour
{
    public UnityEvent soundque;
    // Start is called before the first frame update
    void Start()
    {
        soundque.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
