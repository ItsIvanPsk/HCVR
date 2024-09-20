using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMover : MonoBehaviour
{
    public Vector3 startPosition;
    public Quaternion startRotation;
    public List<Waypoint> waypoints;
    private int currentWaypointIndex = 0;
    private bool isMoving = false;
    private bool waitingForNextMove = true;
    private float lerpTime = 0f;

    void Start()
    {
        gameObject.GetComponent<Transform>().SetPositionAndRotation(startPosition, startRotation);
    }

    void Update()
    {
        if (isMoving)
        {
            MoveAlongPath();
        }
    }

    void MoveAlongPath()
    {
        if (waypoints.Count == 0 || currentWaypointIndex >= waypoints.Count)
            return;

        Waypoint currentWaypoint = waypoints[currentWaypointIndex];

        if (lerpTime < 1f)
        {
            lerpTime += Time.deltaTime / currentWaypoint.translationTime;
            transform.position = Vector3.Lerp(startPosition, currentWaypoint.position, lerpTime);
        }
        else
        {
            startPosition = currentWaypoint.position;
            lerpTime = 0f;
            StartCoroutine(RotateToDirection(currentWaypoint.direction, currentWaypoint.directionTime));
            isMoving = false; // Detener el movimiento hasta que se llame a MoveNext
        }
    }

    IEnumerator RotateToDirection(Vector3 direction, float duration)
    {
        Quaternion targetRotation = Quaternion.Euler(direction);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        waitingForNextMove = true;
    }

    public void MoveNext()
    {
        if (waitingForNextMove && currentWaypointIndex < waypoints.Count)
        {
            currentWaypointIndex++;
            waitingForNextMove = false;
            MoveToNextWaypoint();
        }
    }

    public void MoveToNextWaypoint()
    {
        isMoving = true;
    }

    public void StartMovement()
    {
        isMoving = true;
        currentWaypointIndex = 0;
        startPosition = transform.position;
        lerpTime = 0f;
    }

    public void StopMovement()
    {
        isMoving = false;
    }
}
