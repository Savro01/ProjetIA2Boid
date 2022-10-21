using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ReceiveDamage : MonoBehaviour
{
    //Points de vie actuels
    public int hitPoint = 10;

    //Apr�s avoir re�u un d�g�t :
    //La cr�ature est invuln�rable quelques instants
    public bool isInvulnerable;

    //Temps d'invuln�rabilit�
    public float invulnerabiltyTime;

    //Temps depuis le dernier d�g�t
    private float timeSinceLastHit = 0.0f;

    private void Start()
    {
        isInvulnerable = false;
    }

    private void Update()
    {
        //Si invuln�rable
        if (isInvulnerable)
        {
            //Compte le temps depuis le dernier d�g�t
            //timeSinceLastHit = temps depuis le dernier d�g�t
            //Time.deltaTime = temps �coul� depuis la derni�re frame
            timeSinceLastHit += Time.deltaTime;

            if (timeSinceLastHit > invulnerabiltyTime)
            {
                //Le temps est �coul�, il n'est plus invuln�rable
                timeSinceLastHit = 0.0f;
                isInvulnerable = false;

            }
        }
    }

    //Permet de recevoir des dommages
    public void GetDamage(int damage)
    {
        if (isInvulnerable)
            return;

        isInvulnerable = true;

        //Applique les dommages aux points de vies actuels
        hitPoint -= damage;
    }
}