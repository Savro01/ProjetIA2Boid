using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    float timer = 0;

    public GameObject zombie;
    int maxZombie = 400;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 100; i++)
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
        float x = Random.Range(-9, 9);
        float y = Random.Range(5, 15);

        Vector3 posZombie = new Vector3(x, 2, y);
        Instantiate(zombie, posZombie, Quaternion.identity);
        
    }
}
