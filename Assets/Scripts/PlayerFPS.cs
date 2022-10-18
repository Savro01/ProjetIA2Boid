using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public string etat = "Pistolet";

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Mouvement du joueur
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float speedZ = Input.GetAxis("Vertical");
        float speedX = Input.GetAxis("Horizontal");
        float speedY = moveDirection.y;
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

        gestionEtat();
    }

    void gestionEtat()
    {
        switch (etat)
        {
            case "Pistolet":
                if (switchGun)
                {
                    etat = "Fusil";
                    switchGun = false;
                }
                transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                break;
            case "Fusil":
                if (switchGun)
                {
                    etat = "Pistolet";
                    switchGun = false;
                }
                if (isRunning)
                    etat = "Pistolet";
                transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                break;
        }
    }
}
