using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environnement : MonoBehaviour
{

    public GameObject Tree1;
    public GameObject Tree2;
    public GameObject Tree3;
    public GameObject Tree4;
    public GameObject Tree5;

    List<GameObject> listTree = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        listTree.Add(Tree1);
        listTree.Add(Tree2);
        listTree.Add(Tree3);
        listTree.Add(Tree4);
        listTree.Add(Tree5);
        for (int i = 0; i < 100; i++)
        {
            genArbre();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void genArbre()
    {
        Vector2 origin = new Vector2(0, 0);
        Vector2 posTreeAnnulus = RandomPointInAnnulus(origin, 5, 120);


        Vector3 posTree = new Vector3(posTreeAnnulus.x, 0, posTreeAnnulus.y);
        GameObject actualTree = listTree[Random.Range(0, listTree.Count)];
        GameObject treeGO = Instantiate(actualTree, posTree, Quaternion.identity);
        treeGO.transform.parent = transform;
    }

    public Vector2 RandomPointInAnnulus(Vector2 origin, float minRadius, float maxRadius)
    {
        var randomDirection = Random.insideUnitCircle.normalized;
        var randomDistance = Random.Range(minRadius, maxRadius);
        var point = origin + randomDirection * randomDistance;
        return point;
    }
}
