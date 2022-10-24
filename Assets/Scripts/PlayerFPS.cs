using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerFPS : MonoBehaviour
{
    public Camera playerCamera;

    CharacterController characterController;

    public float walkingSpeed = 7.5f;
    public float runningSpeed = 15f;

    Vector3 moveDirection;

    bool isRunning = false;
    bool switchGun = false;

    float rotationX = 0;
    public float rotationSpeed = 2.0f;
    public float rotationXLimit = 45.0f;

    public enum Etat { Pistolet, Fusil };
    public Etat etat;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;

        //Mouvement du joueur
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float speedZ = Input.GetAxis("Vertical");
        float speedX = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.F))
        {
            switchGun = true;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (isRunning)
        {
            speedX = speedX * runningSpeed;
            speedZ = speedZ * runningSpeed;
        }
        else
        {
            speedX = speedX * walkingSpeed;
            speedZ = speedZ * walkingSpeed;
        }

        moveDirection = forward * speedZ + right * speedX;

        //Applique le mouvement
        characterController.Move(moveDirection * Time.deltaTime);

        //Rotation de la caméra
        rotationX += -Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, -rotationXLimit, rotationXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);

        //Gestion de l'etat du joueur
        gestionEtat();       
    }

    void gestionEtat()
    {
        switch (etat)
        {
            case Etat.Pistolet:
                if (switchGun)
                {
                    etat = Etat.Fusil;
                    switchGun = false;
                }
                transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                break;
            case Etat.Fusil:
                if (switchGun)
                {
                    etat = Etat.Pistolet;
                    switchGun = false;
                }
                if (isRunning)
                    etat = Etat.Pistolet;
                transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            GameObject textGO = GameObject.Find("TextFin");
            textGO.GetComponent<TextMeshProUGUI>().text = "Vous avez perdu, les Zombies vous ont mordus, vous êtes un des leurs désormais...";
            float x = textGO.transform.parent.transform.position.x;
            textGO.transform.position = new Vector3(0 + x, textGO.transform.position.y, textGO.transform.position.z);
            Time.timeScale = 0;
        }
    }
}
