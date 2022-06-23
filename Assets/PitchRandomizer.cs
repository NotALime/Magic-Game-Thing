using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchRandomizer : MonoBehaviour
{
    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;
    void Start()
    {
        GetComponent<AudioSource>().pitch = Random.Range(minPitch, maxPitch);
    }
}
