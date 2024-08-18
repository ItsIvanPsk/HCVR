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
            // Interpola la rotación de la puerta
            _door.transform.eulerAngles = Vector3.Lerp(fromRotation, toRotation, elapsedTime / _duration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        // Asegúrate de que la puerta esté exactamente en la rotación final
        _door.transform.eulerAngles = toRotation;  
    }
}
