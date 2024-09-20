using Autohand;
using UnityEngine;

public class ExperienceStartManager : MonoBehaviour
{
    [SerializeField] private AutoHandPlayer _player;
    [SerializeField] private GameObject _firstCheckpoint;
    [SerializeField] private int _timesPressed;

    public void LoadExperience () {
        _timesPressed++;
        Debug.Log(_timesPressed);
        if (_timesPressed == 2) {
            _player.maxMoveSpeed = 1.25f;
            _firstCheckpoint.SetActive(true);
        }
    }

    private void Start() {
        if (_player != null) 
            _player.maxMoveSpeed = 0f;
            Debug.Log("[ExperienceStartManager] - Player");
        if (_firstCheckpoint != null)
            _firstCheckpoint.SetActive(false);
            Debug.Log("[ExperienceStartManager] - First Checkpoint");
    }
}
