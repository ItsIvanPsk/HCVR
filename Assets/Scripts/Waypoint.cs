using UnityEngine;

[CreateAssetMenu(fileName = "NewWaypoint", menuName = "Waypoint")]
public class Waypoint : ScriptableObject
{
    public int id;
    public Vector3 position;
    public float translationTime;
    public Vector3 direction;
    public float directionTime;
}
