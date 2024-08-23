using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Vector3 InitialPosition;
    [SerializeField] private Quaternion InitialRotation;

    public string SessionId { get; set; }
    public CheckpointManager _checkpointManager { get; set; }

    private ApiController _apiController;
    public PathMover pathMover;

    private void Start() {
        _apiController = new ApiController();     
        CreateNewSession();
        GetComponent<Transform>().SetPositionAndRotation(InitialPosition, InitialRotation);
    }

    public async void CreateNewSession() {
        SessionId = await _apiController.CreateSessionId();
    }
}
