using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public Transform rayOffset;

    public float maxRaycastDistance = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * maxRaycastDistance, Color.green);
        if (Physics.Raycast(ray, out hit, maxRaycastDistance))
        {
            // The ray hit an object.
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            
            // You can perform further actions here, such as interacting with the object.
        }
    }
}
