using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    float timer = 0;

    public GameObject target;

    public GameObject zombie;
    public float radius;
    int maxZombie = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            genZombie();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float time = 0.15f;
        if (timer <= time)
            timer += Time.deltaTime;

        if (timer > time && maxZombie > 0)
        {
            genZombie();
            timer = 0;
        }
    }

    void genZombie()
    {
        float x = Random.Range(-100, 100);
        float z = Random.Range(-100, 100);

        Vector3 posZombie = new Vector3(x, 1, z);
        GameObject zombieGO = Instantiate(zombie, posZombie, Quaternion.identity);
        zombieGO.GetComponent<ZombieBoid>().target = target;

    }
}
