using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CollisionEvent : UnityEvent<Collision> { }

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private CheckpointManager _checkpointManager;
    [SerializeField] private PathMover _mover;
    [SerializeField] private int _checkpointId;
    [SerializeField] private Content _content;
    [SerializeField] private Guide _guide;
    [SerializeField] private bool _openDoor;
    [SerializeField] private DoorOpener? _doorOpener;

    // Define eventos de colisión
    public CollisionEvent OnCollisionEnterEvent;
    public CollisionEvent OnCollisionExitEvent;

    private void OnCollisionEnter(Collision other)
    {
        OnCollisionEnterEvent?.Invoke(other);

        _checkpointManager.ChangeActualCheckpoint(_checkpointId);
        DisableCheckpoint(other.gameObject);
        if (_openDoor)
        {
            OpenDoor();
        }
        _mover.MoveNext();
    }

    private void OpenDoor()
    {
        _doorOpener?.ToggleDoor();
    }

    private void DisableCheckpoint(GameObject col)
    {
        Debug.Log(col.name + " se ha ocultado, checkpoint actual = " + _checkpointId);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        if (_content.audio != null)
        {
            _guide.LoadAudio(_content.audio);
        }
    }
}
