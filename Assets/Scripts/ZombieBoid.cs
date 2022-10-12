using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBoid : MonoBehaviour
{
    public float speedX = 0.05f;
    public float speedZ = 0.05f;

    float maxVelocity = 0.2f;

    // Update is called once per frame
    void Update()
    {
       // transform.position += new Vector3(0, 0, -speedZ * Time.deltaTime);
    }

    public void moveCloser(List<GameObject> boidsZombie, float valAttract)
    {
        if (boidsZombie.Count < 1)
            return;
        float avgX = 0;
        float avgZ = 0;
        foreach(GameObject boid in boidsZombie)
        {
            if (boid.transform.position.x == transform.position.x && boid.transform.position.z == transform.position.z)
                continue;
            avgX += transform.position.x - boid.transform.position.x;
            avgZ += transform.position.z - boid.transform.position.z;
        }
        avgX /= boidsZombie.Count;
        avgZ /= boidsZombie.Count;

        //Set velocity
        speedX -= (avgX / valAttract);
        speedZ -= (avgZ / valAttract);
    }

    public void moveWith(List<GameObject> boidsZombie, float valAlign)
    {
        if (boidsZombie.Count < 1)
            return;
        float avgX = 0;
        float avgZ = 0;
        foreach (GameObject boid in boidsZombie)
        {
            avgX += boid.GetComponent<ZombieBoid>().speedX;
            avgZ += boid.GetComponent<ZombieBoid>().speedZ;
        }
        avgX /= boidsZombie.Count;
        avgZ /= boidsZombie.Count;

        //Set velocity
        speedX -= (avgX / valAlign);
        speedZ -= (avgZ / valAlign);
    }

    public void moveAway(List<GameObject> boidsZombie, float valRepuls, float minDistance)
    {
        if (boidsZombie.Count < 1)
            return;
        float distanceX = 0;
        float distanceZ = 0;
        int numClose = 0;
        foreach (GameObject boid in boidsZombie)
        {
            if(distance(boid) < minDistance)
            {
                numClose++;
                float xDiff = transform.position.x - boid.transform.position.x;
                float zDiff = transform.position.z - boid.transform.position.z;
                if (xDiff >= 0)
                    xDiff = Mathf.Sqrt(minDistance) - xDiff;
                else
                    xDiff = -Mathf.Sqrt(minDistance) - xDiff;
                if (zDiff >= 0)
                    zDiff = Mathf.Sqrt(minDistance) - zDiff;
                else
                    zDiff = -Mathf.Sqrt(minDistance) - zDiff;
                distanceX += xDiff;
                distanceZ += zDiff;
            }

        }
        if (numClose == 0)
            return;
        speedX -= (distanceX / valRepuls);
        speedZ -= (distanceZ / valRepuls);
    }

    public void move()
    {
        if(Mathf.Abs(speedX) > maxVelocity || Mathf.Abs(speedZ) > maxVelocity){
            float scaleFactor = maxVelocity / Mathf.Max(Mathf.Abs(speedX), Mathf.Abs(speedZ));

            speedX *= scaleFactor;
            speedZ *= scaleFactor;
        }
        transform.position += new Vector3(-speedX, 0, -speedZ);
    }

    public float distance(GameObject boidZombie)
    {
        float distX = transform.position.x - boidZombie.transform.position.x;
        float distZ = transform.position.z - boidZombie.transform.position.z;
        return Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distZ, 2));
    }
}
