using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public string SessionId { get; set; }
    public CheckpointManager _checkpointManager { get; set; }

    private ApiController _apiController;
    public PathMover pathMover;

    private void Start() {
        _apiController = new ApiController();     
        CreateNewSession();
    }

    public async void CreateNewSession() {
        SessionId = await _apiController.CreateSessionId();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pathMover.StartMovement(); // Iniciar movimiento al presionar Space
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            pathMover.StopMovement(); // Detener movimiento al presionar S
        }
    }
}
