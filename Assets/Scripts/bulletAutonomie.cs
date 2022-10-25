using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletAutonomie : MonoBehaviour
{
    //La caméra
    private Camera fpsCam;
    Vector3 camPosition;

    Rigidbody bulletRigidbody;
    float speed = 50;

    Vector3 targetPoint;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
        fpsCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        camPosition = fpsCam.transform.position;
        bulletRigidbody = GetComponent<Rigidbody>();

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        // Check whether your are pointing to something so as to adjust the direction
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(1000); // You may need to change this value according to your needs
                                              // Create the bullet and give it a velocity according to the target point computed 

        Invoke("setVisibleBullet", 0.15f);
        Invoke("AutoDestruction", 2);
    }

    // Update is called once per frame
    void Update()
    {
        bulletRigidbody.velocity = (targetPoint - transform.position).normalized * speed;
    }

    void setVisibleBullet()
    {
        GetComponent<Renderer>().enabled = true;
    }

    void AutoDestruction()
    {
        Destroy(this.gameObject);
    }
}
