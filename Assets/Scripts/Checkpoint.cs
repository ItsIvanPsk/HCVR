using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private CheckpointManager _checkpointManager;
    [SerializeField] private PathMover _mover;
    [SerializeField] private int _checkpointId;
    [SerializeField] private Content _content;
    [SerializeField] private Guide _guide;


    private void OnCollisionEnter(Collision other)
    {
        _checkpointManager.ChangeActualCheckpoint(_checkpointId);
        _mover.MoveNext();
        DisableCheckpoint(other.gameObject);
    }

    private void DisableCheckpoint(GameObject col) {
        Debug.Log(col.name + " se ha ocultado, checkpoint actual = " + _checkpointId);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        if (_content.audio != null) {
            _guide.LoadAudio(_content.audio);
        }
    }

}   