using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsActive : MonoBehaviour
{
    public GameObject Switch;
    public bool Test = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Switch.GetComponent<Switch>().Activate == true)
        {
            
        }
    }
}
