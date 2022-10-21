using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBoid : MonoBehaviour
{

    public Vector3 velocity;

    public float maxVelocity;

    public GameObject target;

    bool lookPlayer = false;

    public enum Etat { Cours, Marche, Mort };
    public Etat etat;

    int pv;

    bool willDie = false;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<ReceiveDamage>().hitPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (!willDie)
        {
            //Gestion du Boid
            if ((target.transform.position - transform.position).magnitude < 50)
            {
                velocity += target.transform.position - transform.position;
                lookPlayer = true;
            }
            else
            {
                lookPlayer = false;
            }

            if (velocity.magnitude > maxVelocity)
            {
                velocity = velocity.normalized * maxVelocity;
            }
            transform.position += velocity * Time.deltaTime;
            transform.position -= new Vector3(0, transform.position.y, 0);
            velocity -= new Vector3(0, velocity.y, 0);
            if (lookPlayer)
                transform.LookAt(target.transform.position);
            else
                if (velocity != Vector3.zero)
                    transform.rotation = Quaternion.LookRotation(velocity);
        }
        else
        {
            velocity = Vector3.zero;
        }

        //Gestion de l'etat
        pv = GetComponent<ReceiveDamage>().hitPoint;
        gestionEtatVie();
    }

    void gestionEtatVie()
    {
        switch (etat)
        {
            case Etat.Cours:
                if (!lookPlayer)
                    etat = Etat.Marche;
                if(pv <= 0)
                {
                    willDie = true;
                    etat = Etat.Mort;
                }
                if (pv <= 5)
                    etat = Etat.Marche;
                maxVelocity = 10;
                //Play Animation Cours
                break;
            case Etat.Marche:
                if (lookPlayer)
                    etat = Etat.Cours;
                if (pv <= 0)
                {
                    willDie = true;
                    etat = Etat.Mort;
                }
                maxVelocity = 2;
                //Play Animation Marche
                break;
            case Etat.Mort:
                Invoke("ZombieMort", 2);
                //Play Animation Dead
                break;
        }
    }


    void ZombieMort()
    {
        Destroy(gameObject);
    }
}
