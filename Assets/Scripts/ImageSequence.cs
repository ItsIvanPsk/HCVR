using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImageSequence", menuName = "Image", order = 0)]
public class ImageSequence : ScriptableObject {
    public int Id;
    public Sprite Image;
    public float StartingTime;
    public int ImagePosition;
    public float Duration;
}
