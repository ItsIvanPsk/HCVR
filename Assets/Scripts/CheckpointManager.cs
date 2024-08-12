using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private int ActualCheckpoint = 0;
    public ApiController _apiController { get; set; }
    public PlayerManager _player;

    private void Start() {
        _apiController = new ApiController();
    }

    public void ChangeActualCheckpoint(int checkpointId) {
        ActualCheckpoint = checkpointId;
        Debug.Log(ActualCheckpoint);
        SetCheckpoint();
    }

    private async void SetCheckpoint() {
        Debug.Log(_player.SessionId);   
        var data = new PostCheckpoint
        {
            sessionId = _player.SessionId,
            checkpointId = ActualCheckpoint
        };
        await _apiController.SetCheckpointAPI(data);
    }
}
