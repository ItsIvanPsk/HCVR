using System.Collections;
using UnityEngine;

public class NewRoomManager : MonoBehaviour
{
    [SerializeField] private DoorOpener _door;
    [SerializeField] private AudioClip _initialAudio;
    [SerializeField] private Guide _guide;

    public void Start() {
        if (_initialAudio != null) {
            _guide.GetComponent<AudioSource>().Stop();
            _guide.GetComponent<AudioSource>().clip = _initialAudio;
            _guide.GetComponent<AudioSource>().Play();
            StartCoroutine(WaitAudioFinish());
        }
    }

    private IEnumerator WaitAudioFinish() {
        var elapsedTime = 0f;
        var duration = _initialAudio.length;
        while (elapsedTime <= duration) {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _door.ToggleDoor();
        _guide.GetComponent<PathMover>().MoveNext();
    }

}
