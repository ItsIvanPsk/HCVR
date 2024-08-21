using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRoomManager : MonoBehaviour
{
    [SerializeField] private AudioClip _comeWithMe;
    [SerializeField] private AudioClip _introGuide;
    [SerializeField] private GameObject _guide;
    [SerializeField] private PathMover _mover;
    [SerializeField] private SceneFader _sceneFader;
    
    private AudioSource _audio;

    private void Start() {
        if (_guide != null) {
            _audio = _guide.GetComponent<AudioSource>();
        }
    }

    public void LoadComeWithMeAudio() {
        GetNullableLogs();
        if (_guide != null && _audio != null && _comeWithMe != null) {
            _audio.Pause();
            _audio.clip = _comeWithMe;
            _audio.Play();
        }
    }

    public void LoadIntroGuide() {
        GetNullableLogs();
        if (_guide != null && _audio != null && _introGuide != null) {
            _mover.MoveNext();
            _audio.Pause();
            _audio.clip = _introGuide;
            _audio.Play();
        }
    }

    public void GetNullableLogs() {
        Debug.Log("[TutorialRoomManager] - Guía = " + _guide);
        Debug.Log("[TutorialRoomManager] - AudioGuia = " + _audio);
        Debug.Log("[TutorialRoomManager] - ComeWMe = " + _comeWithMe);
    }

    public void ChangeScene() {
        _sceneFader.FadeToScene(2);
    }
}
