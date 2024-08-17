using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SQ-", menuName = "Image Sequence", order = 0)]
public class ImageSequence : ScriptableObject {
    public int Id;
    public Sprite Image;
    public float StartingTime;
    public int ImagePosition;
    public float Duration;
}
