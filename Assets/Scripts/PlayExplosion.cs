using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayExplosion : MonoBehaviour
{
    public static AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayDestroyAudio() {
        _audioSource.PlayOneShot(_audioSource.clip); 
    }
}
