using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private AudioClip _initialAudio;
    [SerializeField] private AudioClip _finalAudio;
    [SerializeField] private Guide _guide;
    [SerializeField] private SceneFader _sceneFader;

    public void LoadAudio() {
        if (_initialAudio != null) {
            _guide.GetComponent<AudioSource>().Stop();
            _guide.GetComponent<AudioSource>().clip = _initialAudio;
            _guide.GetComponent<AudioSource>().Play();
            StartCoroutine(HandlePlayerRoomAudio(_initialAudio.length));
        }
    }

    private IEnumerator HandlePlayerSceneChange(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration + 1)
        {
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        _sceneFader.FadeToScene(3);
    }

    private IEnumerator HandlePlayerRoomAudio(float duration) {
        float elapsedTime = 0f;
        while (elapsedTime < duration + 1)
        {
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        if (_finalAudio != null) {
            _guide.GetComponent<AudioSource>().Stop();
            _guide.GetComponent<AudioSource>().clip = _finalAudio;
            _guide.GetComponent<AudioSource>().Play();
            StartCoroutine(HandlePlayerSceneChange(_finalAudio.length));
        }    
    }
}
