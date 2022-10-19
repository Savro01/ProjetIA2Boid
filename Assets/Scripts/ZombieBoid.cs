using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBoid : MonoBehaviour
{

    public Vector3 velocity;

    public float maxVelocity;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        //velocity = transform.forward * maxVelocity;
        if(target.transform.position.magnitude - transform.position.magnitude < 50)
            velocity = (target.transform.position - transform.position) * maxVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.transform.position.magnitude - transform.position.magnitude < 50)
            velocity += (target.transform.position - transform.position) * maxVelocity;
        else
            velocity += new Vector3(1,0,0) * maxVelocity;
        if (velocity.magnitude > maxVelocity)
        {
            velocity = velocity.normalized * maxVelocity;
        }
        transform.position += velocity * Time.deltaTime;
        transform.position -= new Vector3(0, transform.position.y, 0);
        transform.rotation = Quaternion.LookRotation(velocity);
    }
}
