using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryRoomManager : MonoBehaviour
{
    [SerializeField] private SceneFader _sceneFader;

    public void ChangeScene() {
        _sceneFader.FadeToScene(2);
    }
}
