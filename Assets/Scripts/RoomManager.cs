using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private AudioClip _initialAudio;
    [SerializeField] private AudioClip _finalAudio;
    [SerializeField] private Guide _guide;
    [SerializeField] private DoorOpener _door;
    [SerializeField] private SceneFader _sceneFader;
    [SerializeField] private bool hasFade = true;

    private void Start() {
        Debug.Log(!hasFade);
        if (!hasFade) {
            LoadAudio();
        }
    }

    public void LoadAudio() {
        Debug.Log("[Room Manager] - Load Audio Start");
        if (_initialAudio != null) {
            Debug.Log("[Room Manager] - InitialAudio != NULL");
            _guide.GetComponent<AudioSource>().Stop();
            _guide.GetComponent<AudioSource>().clip = _initialAudio;
            _guide.GetComponent<AudioSource>().Play();
            StartCoroutine(HandlePlayerRoomAudio(_initialAudio.length));
        }
        Debug.Log("[Room Manager] - Load Audio END");
    }

    private IEnumerator HandlePlayerSceneChange(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration + 1)
        {
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        if (hasFade) {
            _sceneFader.FadeToScene(4);
        } else {
            _door.GetComponent<DoorOpener>().ToggleDoor();
            _guide.GetComponent<PathMover>().MoveNext();
        }
    }

    private IEnumerator HandlePlayerRoomAudio(float duration) {
        float elapsedTime = 0f;
        while (elapsedTime < duration + 15)
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
