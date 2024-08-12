using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{

    public void LoadAudio(AudioClip _clip) {
        if (_clip == null) {
            return;
        }
        var audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Stop();
        gameObject.GetComponent<AudioSource>().clip = _clip; 
        audioSource.Play();
    }

}
