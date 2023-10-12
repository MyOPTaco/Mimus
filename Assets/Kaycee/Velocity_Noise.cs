using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity_Noise : MonoBehaviour
{
    //This Script is only to be applied to grabable objects, having alot of these may be a bit resource heavy
    private Rigidbody rb;
    public float biggestSpeed, speed, noiseMulti;
    private Vector3 currentVelo;
    public Transform noiseLocation;
    public bool hathCollided;
    private float noiseRadius;
    public LayerMask whatIsEnemy;
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        currentVelo = rb.velocity;
        speed = currentVelo.magnitude;
    }

    void DrawNoiseCircle()
    {
         noiseRadius = biggestSpeed * noiseMulti;
         Collider[] PossiblenoiseDetection = Physics.OverlapSphere(transform.position, noiseRadius, whatIsEnemy);
        foreach (Collider _ in PossiblenoiseDetection)
        {
            Enemy.GetComponent<Enemy_AIRecode>().noiseDetection = true;
            Enemy.GetComponent<Enemy_AIRecode>().noiseSource = noiseLocation;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        biggestSpeed = speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        hathCollided = true;
        if(biggestSpeed > 2f)
        {
            
            DrawNoiseCircle();
        }
        
    }
}
