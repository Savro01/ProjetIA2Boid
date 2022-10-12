using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidGestion : MonoBehaviour
{
    public GameObject ZombieBoidPrefab;
    List<GameObject> totalBoidsZombie = new List<GameObject>();
    int maxZombie = 500;

    [Range(1f,100f)]
    public float valAttract;

    [Range(1f, 100f)]
    public float valAlign;

    [Range(1f, 100f)]
    public float valRepuls;

    [Range(1f, 100f)]
    public float distanceVision;

    // Start is called before the first frame update
    void Start()
    {
        for(int z = 0; z < 10; z++)
        {
            GenOneZombie();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Generation des zombies aprés le début de la partie
        /*if(maxZombie >= 0)
        {
            GenOneZombie();
            maxZombie--;
        }*/

        //Boucle principale
        foreach (GameObject boid in totalBoidsZombie)
        {
            List<GameObject> closeBoids = new List<GameObject>();
            foreach (GameObject otherboid in totalBoidsZombie)
            {
                if (otherboid == boid)
                    continue;
                if (boid.GetComponent<ZombieBoid>().distance(otherboid) < distanceVision)
                {
                    closeBoids.Add(otherboid);
                }
            }
            boid.GetComponent<ZombieBoid>().moveCloser(closeBoids, valAttract);
            boid.GetComponent<ZombieBoid>().moveWith(closeBoids, valAlign);
            boid.GetComponent<ZombieBoid>().moveAway(closeBoids, valRepuls, 10);
            
           // if(boid.transform.position.x < 9 )
            boid.GetComponent<ZombieBoid>().move();
        }
    }

    //Fonction qui généra un zombie à un endroit aléatoire
    void GenOneZombie()
    {
        float posX = Random.Range(-9, 9);
        float posZ = Random.Range(-15, -5);

        GameObject newZombie = Instantiate(ZombieBoidPrefab, new Vector3(posX, 0.5f, posZ), Quaternion.identity);
        newZombie.transform.rotation = new Quaternion(0, 180, 0, 0) ;
        newZombie.transform.parent = transform;
        totalBoidsZombie.Add(newZombie);
    }
}
