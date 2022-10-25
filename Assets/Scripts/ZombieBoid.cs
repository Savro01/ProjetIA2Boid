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

    new Animator animation;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<ReceiveDamage>().hitPoint;
        animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!willDie)
        {
            //Gestion du Boid
            if ((target.transform.position - transform.position).magnitude < 35)
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
                animation.SetBool("Cours", true);
                animation.SetBool("Marche", false);
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
                break;
            case Etat.Marche:
                animation.SetBool("Cours", false);
                animation.SetBool("Marche", true);
                if (lookPlayer && pv > 5)
                    etat = Etat.Cours;
                if (pv <= 0)
                {
                    willDie = true;
                    etat = Etat.Mort;
                }
                maxVelocity = 2;
                break;
            case Etat.Mort:
                animation.SetBool("Mort", true);
                transform.GetComponent<CapsuleCollider>().enabled = false;
                Invoke("ZombieMort", 2);
                break;
        }
    }

    void ZombieMort()
    {
        Destroy(gameObject);
    }
}
