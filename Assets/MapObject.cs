using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    public Sprite[] varients;

    int index;

    public LayerMask destroyLayers;

    public bool flippable = true;
    public bool randomRotation = true;
    // Start is called before the first frame update
    void Awake()
    {
        if (varients.Length > 0)
        {
            index = Random.Range(0, varients.Length);
            GetComponent<SpriteRenderer>().sprite = varients[index];

            if (flippable == true)
            {
                int randomx = Random.Range(0, 2);
                if (randomx == 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }

            if (randomRotation == true)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
            }
        }
    }
}

