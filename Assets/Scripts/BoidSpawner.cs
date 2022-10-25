using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoidSpawner : MonoBehaviour
{
    public GameObject target;

    public GameObject zombie;

    int maxZombie = 50;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            genZombie();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(maxZombie > 0)
        {
            Invoke("genZombie", 2);
            maxZombie--;
        }
        GameObject[] zombieList = GameObject.FindGameObjectsWithTag("Zombie");
        if (zombieList.Length == 0)
        {
            GameObject textGO = GameObject.Find("TextFin");
            textGO.GetComponent<TextMeshProUGUI>().text = "Tous les zombies ont été éliminés !";
            float x = textGO.transform.parent.transform.position.x;
            textGO.transform.position = new Vector3(0 + x, textGO.transform.position.y, textGO.transform.position.z);
            Time.timeScale = 0;
        }
    }

    void genZombie()
    {
        Vector2 origin = new Vector2(0, 0);
        Vector2 posZombieAnnulus = RandomPointInAnnulus(origin, 80, 120);

        Vector3 posZombie = new Vector3(posZombieAnnulus.x, 1, posZombieAnnulus.y);
        GameObject zombieGO = Instantiate(zombie, posZombie, Quaternion.identity);
        zombieGO.transform.parent = transform;
        zombieGO.GetComponent<ZombieBoid>().target = target;

    }

    public Vector2 RandomPointInAnnulus(Vector2 origin, float minRadius, float maxRadius)
    {
        var randomDirection = Random.insideUnitCircle.normalized;
        var randomDistance = Random.Range(minRadius, maxRadius);
        var point = origin + randomDirection * randomDistance;
        return point;
    }
}
