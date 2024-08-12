using UnityEngine;

public class TransitionCheckpoint : MonoBehaviour
{
    public SceneFader sceneFader; // Asegúrate de asignar esto en el inspector
    public int sceneIndex; // Índice de la escena a la que deseas cambiar

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name);
        
        if (other.gameObject.CompareTag("Player") && this.CompareTag("Checkpoint"))
        {
            Debug.Log("Player and Checkpoint detected.");
            if (sceneFader != null)
            {
                Debug.Log("SceneFader found, initiating fade.");
                sceneFader.FadeToScene(sceneIndex);
            }
            else
            {
                Debug.LogError("SceneFader no está asignado en el inspector.");
            }
        }
    }
}
