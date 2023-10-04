using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity_Noise : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float BiggestSpeed;
    private Vector3 CurrentVelo;
    public bool HathCollided;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentVelo = rb.velocity;
        speed = CurrentVelo.magnitude;
    }

    void DrawNoiseCircle()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        BiggestSpeed = speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        HathCollided = true;
        DrawNoiseCircle();
    }
}
