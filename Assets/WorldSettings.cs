using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSettings : MonoBehaviour
{
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        foreach (WorldColor worldColor in FindObjectsOfType<WorldColor>())
        {
            worldColor.sr.color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
