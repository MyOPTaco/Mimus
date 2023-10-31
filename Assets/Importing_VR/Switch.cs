using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public AudioSource source;
    public AudioClip switchSound;

    public bool on = false;
    public bool switchHit = false;
    public float switchRotation = 100;
    private GameObject switchBase;
    

    public Light spotLight;
    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        switchBase = transform.GetChild(0).gameObject;
        spotLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(switchHit == true)
        {
            source.PlayOneShot(switchSound);
            switchHit = false;
            on = !on;

            if(on == true)
            {
                spotLight.enabled = true;
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x + switchRotation, transform.eulerAngles.y, transform.eulerAngles.z);
            }
            else
            {
                spotLight.enabled = false;
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x - switchRotation, transform.eulerAngles.y, transform.eulerAngles.z);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            switchHit = true;
        }
    }
}
