using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(ZombieBoid))]
public class BoidCohesion : MonoBehaviour
{
    [Range(1, 50)]
    public float forceCohesion;

    private ZombieBoid boid;

    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        boid = GetComponent<ZombieBoid>();
    }

    // Update is called once per frame
    void Update()
    {
        var boids = FindObjectsOfType<ZombieBoid>();
        Vector3 average = Vector3.zero;
        int found = 0;

        foreach (var boid in boids.Where(b => b != boid))
        {
            var diff = boid.transform.position - transform.position;
            if(diff.magnitude < radius)
            {
                average += diff;
                found += 1;
            }
        }
        if(found > 0)
        {
            average = average / found;
            boid.velocity += Vector3.Lerp(Vector3.zero, average, average.magnitude / radius).normalized * forceCohesion;
        }
    }
}
