using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Tweenables.Primitives;

public class TutorialRoomManager : MonoBehaviour
{
    [SerializeField] private AudioClip _introGuide;
    [SerializeField] private AudioClip _comeWithMe;
    [SerializeField] private AudioClip _rememberToMove;
    [SerializeField] private GameObject _guide;
    [SerializeField] private PathMover _mover;
    [SerializeField] private SceneFader _sceneFader;
    [SerializeField] private bool audioActive = false;

    private float RememberToMoveEleapsedTime = 0f;
    
    private AudioSource _audio;

    private void Start() {
        if (_guide != null) {
            _audio = _guide.GetComponent<AudioSource>();
        }
        LoadIntroGuide();
    }

    public void LoadIntroGuide() {
        if (!audioActive) {
            audioActive = true;
            Debug.Log("[TutorialRoomManager] - LoadIntroGuide Start -  Audio Active => " + audioActive);
            _audio.Pause();
            _audio.clip = _introGuide;
            _audio.Play();
            StartCoroutine(AudioIsOn(_introGuide.length));
            StartCoroutine(RememberToMove());
            Debug.Log("[TutorialRoomManager] - LoadIntroGuide End -  Audio Active => " + audioActive);
        }
    }

    public IEnumerator RememberToMove() {
        while (true) {
            var duration = 20f;
            while (RememberToMoveEleapsedTime < duration) {
                RememberToMoveEleapsedTime += Time.deltaTime;
                yield return null;
            }
            if (!audioActive) {
                LoadRememberAudio();
            }
            
        }
    }

    public void LoadRememberAudio() {
        if (!audioActive) {
            audioActive = true;
            _mover.MoveNext();
            _audio.Pause();
            _audio.clip = _rememberToMove;
            _audio.Play();
            StartCoroutine(AudioIsOn(_rememberToMove.length));
            RememberToMoveEleapsedTime = 0f;
        }
    }

    public IEnumerator AudioIsOn(float duration) {
        var elapsedTime = 0f;
        Debug.Log("[TutorialRoomManager] - AudioIsOn Start -  Audio Active => " + audioActive);
        while (elapsedTime < duration + 1) {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        audioActive = false;
        Debug.Log("[TutorialRoomManager] - AudioIsOn End -  Audio Active => " + audioActive);
    }

    public void ChangeScene() {
        _sceneFader.FadeToScene(2);
    }
}
