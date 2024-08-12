using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] public SceneFader sceneFader;

    public void ChangeScene(int sceneIndex)
    {
        sceneFader.FadeToScene(sceneIndex);
    }
}
