using UnityEngine;
using System.Collections;

public class BlindfoldOpacity : MonoBehaviour
{
    [SerializeField] private Renderer cubeRenderer;
    [SerializeField] private bool isHidden;
    [SerializeField] private float TransitionTime;

    private void Start() {
        Color color = cubeRenderer.material.color;
        color.a = 0f;
        cubeRenderer.material.color = color;
        isHidden = true; // Assuming you start hidden
    }

    public void UpdateBlindfoldOpacity() {
        StopAllCoroutines();
        if (isHidden) {
            StartCoroutine(FadeTo(1f));
        } else {
            StartCoroutine(FadeTo(0f));
        }
        isHidden = !isHidden;
    }

    private IEnumerator FadeTo(float targetAlpha) {
        Color color = cubeRenderer.material.color;
        float startAlpha = color.a;
        float elapsedTime = 0;

        while (elapsedTime < TransitionTime) {
            color.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / TransitionTime);
            cubeRenderer.material.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = targetAlpha; // Ensure the final alpha is set
        cubeRenderer.material.color = color;
    }
}
