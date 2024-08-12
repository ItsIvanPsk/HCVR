using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage; // Asegúrate de asignar esto en el inspector
    public float fadeDuration = 1f;

    private bool isFading = false; // Booleano para controlar si una animación de fundido está en curso

    private void Start()
    {
        // Asegúrate de que la imagen esté completamente transparente al inicio
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0f, 0f, 0f, 0f);
        }
        else
        {
            Debug.LogError("Fade Image no está asignado en el inspector.");
        }
    }

    public void FadeToScene(int sceneIndex)
    {
        if (!isFading)
        {
            StartCoroutine(FadeOutIn(sceneIndex));
        }
    }

    private IEnumerator FadeOutIn(int sceneIndex)
    {
        isFading = true; // Indicar que una animación de fundido está en curso

        // Fundido a negro
        yield return StartCoroutine(Fade(1f));
        
        // Cargar nueva escena
        SceneManager.LoadScene(sceneIndex);
        
        // Esperar a que la escena cargue
        yield return null;
        
        // Fundido de negro a transparente
        yield return StartCoroutine(Fade(0f));

        isFading = false; // Indicar que la animación de fundido ha terminado
    }

    private IEnumerator Fade(float targetAlpha)
    {
        if (fadeImage == null)
        {
            Debug.LogError("Fade Image no está asignado en el inspector.");
            yield break;
        }

        float startAlpha = fadeImage.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        // Asegúrate de que el alpha final es exactamente el targetAlpha
        fadeImage.color = new Color(0f, 0f, 0f, targetAlpha);
    }
}
