using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Tweenables.Primitives;

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

    public void LoadIntroGuide() {

        if (_guide != null && _audio != null && _introGuide != null) {
            _mover.MoveNext();
            _audio.Pause();
            _audio.clip = _introGuide;
            _audio.Play();
            StartCoroutine(WaitToChangeScene());
        }
    }

    public IEnumerator WaitToChangeScene() {
        var elapsedTime = 0f;
        var duration = _introGuide.length;
        var offsetTime = 3f;
        while (elapsedTime < duration + offsetTime) {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ChangeScene();
    }

    public void ChangeScene() {
        _sceneFader.FadeToScene(2);
    }
}
