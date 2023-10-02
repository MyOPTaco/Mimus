using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    public GameObject lastHit;
    public Vector3 collision = Vector3.zero;
    public LayerMask layer;
    public SphereCollider activationCollider;
    public bool CollisionOn;
    public GameObject MainCamera;
    // Start is called before the first frame update
    void Start()
    {
        activationCollider = GetComponent<SphereCollider>();
        activationCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(MainCamera.transform.position, MainCamera.transform.TransformDirection(Vector3.forward) * 10, Color.green);
        var ray = new Ray(origin: MainCamera.transform.position, direction: this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance: 100))
        {
            lastHit = hit.transform.gameObject;
            collision = hit.point;
        }

        if (collision == hit.point && Input.GetKeyDown(KeyCode.E))
        {
            CollisionOn = true;
            StartCoroutine(CollideTime());
        }
    }

    private void OnDrawGizmos()
    {
        Update();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collision, radius: 0.2f);
    }

    IEnumerator CollideTime()
    {
        if (CollisionOn == true)
        {
            activationCollider.enabled = true;
            activationCollider.transform.position = collision;
            yield return new WaitForSeconds(1f);

            activationCollider.enabled = false;
            CollisionOn = false;
            Debug.Log("FinishedInteract");
        }
    }
}

