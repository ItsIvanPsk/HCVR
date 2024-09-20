using System.Collections;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private Vector3 _closedRotation;
    [SerializeField] private Vector3 _openRotation;
    [SerializeField] private float _duration = 2.0f;

    private bool _isOpen = false;

    private void Start() {
        _door.transform.eulerAngles = _closedRotation;
    }

    public void ToggleDoor()
    {
        Debug.Log("[DoorOpener] - " + _isOpen);
        if (_isOpen)
        {
            StartCoroutine(RotateDoor(_openRotation, _closedRotation));
        }
        else
        {
            StartCoroutine(RotateDoor(_closedRotation, _openRotation));
        }
        _isOpen = !_isOpen;
    }

    private IEnumerator RotateDoor(Vector3 fromRotation, Vector3 toRotation)
    {
        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            _door.transform.eulerAngles = Vector3.Lerp(fromRotation, toRotation, elapsedTime / _duration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        _door.transform.eulerAngles = toRotation;  
    }
}
