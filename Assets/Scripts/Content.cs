using UnityEngine;

[CreateAssetMenu(fileName = "Audio", menuName = "Content")]
public class Content : ScriptableObject
{
    public int CheckpointId;
    public AudioClip audio;
}