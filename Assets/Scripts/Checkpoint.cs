using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CollisionEvent : UnityEvent<Collision> { }

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private CheckpointManager _checkpointManager;
    [SerializeField] private PathMover _mover;
    [SerializeField] private int _checkpointId;
    [SerializeField] private GameObject _nextCheckpoint;
    [SerializeField] private Content _content;
    [SerializeField] private Guide _guide;
    [SerializeField] private bool _openDoor;
    [SerializeField] private DoorOpener? _doorOpener;

    public CollisionEvent OnCollisionEnterEvent;
    public CollisionEvent OnCollisionExitEvent;

    private void OnCollisionEnter(Collision other)
    {
        if (gameObject.CompareTag("Checkpoint")) {
            OnCollisionEnterEvent?.Invoke(other);
            DisableCheckpoint(other.gameObject);
        }
    }

    private void DisableCheckpoint(GameObject col)
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        if (_content.audio != null)
        {
            _guide.LoadAudio(_content.audio);
            StartCoroutine(DisableNextBlocker(_content.audio.length));
        }
    }

    public IEnumerator DisableNextBlocker(float duration) {
        var elapsedTime = 0f;
        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _nextCheckpoint.GetComponent<BoxCollider>().enabled = false;
        _nextCheckpoint.GetComponent<MeshRenderer>().enabled = false;
    }
}
