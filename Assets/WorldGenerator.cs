using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public int size = 10;
    public float chunkSize = 3;
    public Chunk[] chunkTypes;
    void Start()
    {
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                int random = Random.Range(0, 4);
                float rotation = 0;
                if (random == 0)
                {
                    rotation = 0;
                }
                else if(random == 1)
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
                Instantiate(chunkTypes[Random.Range(0, chunkTypes.Length)], new Vector3(x * chunkSize, 0, y * chunkSize), Quaternion.Euler(new Vector3(0, rotation, 0)));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
