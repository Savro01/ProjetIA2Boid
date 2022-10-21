using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ZombieBoid))]
public class BoidContainer : MonoBehaviour
{
    private ZombieBoid boid;

    public float radius;
    [Range(1, 50)]
    public float boundaryForce;

    // Start is called before the first frame update
    void Start()
    {
        boid = GetComponent<ZombieBoid>();
    }

    // Update is called once per frame
    void Update()
    {
        if(boid.transform.position.magnitude > radius)
        {
            boid.velocity += transform.position.normalized * (radius - boid.transform.position.magnitude) * boundaryForce * Time.deltaTime;
        }
    }
}
