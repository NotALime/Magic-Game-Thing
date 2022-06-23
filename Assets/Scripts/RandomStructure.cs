using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStructure : MonoBehaviour
{

    public GameObject[] structures;
    void Start()
    {
        int random = Random.Range(0, 4);
        float rotation = 0;
        if (random == 0)
        {
            rotation = 0;
        }
        else if (random == 1)
        {
            rotation = 90;
        }
        else if (random == 2)
        {
            rotation = 180;
        }
        else if (random == 3)
        {
            rotation = 270;
        }
        Instantiate(structures[Random.Range(0, structures.Length)], transform.position, Quaternion.Euler(new Vector3(0, rotation, 0)));
        Destroy(this.gameObject);
    }
}
