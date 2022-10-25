using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : MonoBehaviour
{
    //Portée du tir
    public float weaponRange = 200f;

    //Force de l'impact du tir
    public float hitForce = 100f;

    //Temps entre chaque tir (en secondes) 
    public float fireRate = 0.10f;

    //Float : mémorise le temps du prochain tir possible
    private float nextFire;

    //Détermine sur quel Layer on peut tirer
    public LayerMask layerMask;

    public GameObject bullet;

    bool shoot = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Vérifie si le joueur a pressé le bouton pour faire feu (ex:bouton gauche souris)
        // Time.time > nextFire : vérifie si suffisament de temps s'est écoulé pour pouvoir tirer à nouveau
        //GetButtonDown to only clic
        if (transform.parent.parent.GetComponent<PlayerFPS>().etat == PlayerFPS.Etat.Pistolet)
        {
            if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
                shoot = true;
        }
        else if (transform.parent.parent.GetComponent<PlayerFPS>().etat == PlayerFPS.Etat.Fusil)
        {
            if (Input.GetButton("Fire1") && Time.time > nextFire)
                shoot = true;
        }
        if(shoot)
        {
            //Met à jour le temps pour le prochain tir
            //Time.time = Temps écoulé depuis le lancement du jeu
            //temps du prochain tir = temps total écoulé + temps qu'il faut attendre
            nextFire = Time.time + fireRate;

            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            ParticleSystem fireshot = transform.Find("Particle System").GetComponent<ParticleSystem>();
            fireshot.Play();

            //On instantie une balle et on l'envoie a grande vitesse devant
            GameObject bulletMagasine = transform.Find("Casing").GetChild(0).gameObject;
            Vector3 bulletPos = bulletMagasine.transform.position;
            Quaternion bulletRotation = bulletMagasine.transform.rotation;
            GameObject currentBullet = Instantiate(bullet, bulletPos, bulletRotation);

            shoot = false;
        }
    }
}