using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldColor : MonoBehaviour
{
    public SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
}
