using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private Vector3 _initialPosition;
    [SerializeField] private Vector3 _openPosition;
    [SerializeField] private float _duration = 2.0f;

    private bool _isOpen = false;

    private void Start() {
        _door.GetComponent<Transform>().Rotate(_initialPosition);
    }

    public void ToggleDoor()
    {
        if (_isOpen)
        {
            StartCoroutine(MoveDoor(_openPosition, _initialPosition));
        }
        else
        {
            StartCoroutine(MoveDoor(_initialPosition, _openPosition));
        }
        _isOpen = !_isOpen;
    }

    private IEnumerator MoveDoor(Vector3 fromPosition, Vector3 toPosition)
    {
        float elapsedTime = 0f;
        while (elapsedTime < _duration)
        {
            _door.transform.Rotate(-_openPosition/_duration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        _door.transform.position = toPosition;  
    }
}
