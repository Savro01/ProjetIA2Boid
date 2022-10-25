using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : MonoBehaviour
{
    //Port�e du tir
    public float weaponRange = 200f;

    //Force de l'impact du tir
    public float hitForce = 100f;

    //Temps entre chaque tir (en secondes) 
    public float fireRate = 0.10f;

    //Float : m�morise le temps du prochain tir possible
    private float nextFire;

    //D�termine sur quel Layer on peut tirer
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
        // V�rifie si le joueur a press� le bouton pour faire feu (ex:bouton gauche souris)
        // Time.time > nextFire : v�rifie si suffisament de temps s'est �coul� pour pouvoir tirer � nouveau
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
            //Met � jour le temps pour le prochain tir
            //Time.time = Temps �coul� depuis le lancement du jeu
            //temps du prochain tir = temps total �coul� + temps qu'il faut attendre
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