using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LastDoor : MonoBehaviour
{
    public GameObject Switch, Switch2, Switch3;
    public UnityEvent OpenExit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Switch.GetComponent<Switch>().Activate == true && Switch2.GetComponent<Switch>().Activate == true && Switch3.GetComponent<Switch>().Activate == true)
        {
            OpenExit.Invoke();
        }
    }
}
