using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Control : MonoBehaviour
{
    AudioSource AudioSource;
    public AudioClip explosion_se;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.PlayOneShot(explosion_se);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
