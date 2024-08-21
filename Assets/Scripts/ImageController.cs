using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImageController : MonoBehaviour
{
    [SerializeField] private List<ImageSequence> _sequence;
    [SerializeField] private PathMover _mover;
    [SerializeField] private TutorialRoomManager _tutorialManager;
    [SerializeField] private SceneFader _sceneFader;
    [SerializeField] private GameObject ImageRenderer1, ImageRenderer2, ImageRenderer3, ImageRenderer4, ImageRenderer5, ImageRenderer6, ImageRenderer7;

    private Dictionary<GameObject, Vector3> initialPositions;

    private void Awake() {
        ResetAllRenderers();
        StoreInitialPositions();
    }

    private void Start() {
        StartCoroutine(IntroGuide(5));
        foreach (var imageSequence in _sequence) {
            StartCoroutine(ScheduleImage(imageSequence));
        }
    }

    private void StoreInitialPositions() {
        initialPositions = new Dictionary<GameObject, Vector3>
        {
            { ImageRenderer1, ImageRenderer1.transform.position },
            { ImageRenderer2, ImageRenderer2.transform.position },
            { ImageRenderer3, ImageRenderer3.transform.position },
            { ImageRenderer4, ImageRenderer4.transform.position },
            { ImageRenderer5, ImageRenderer5.transform.position },
            { ImageRenderer6, ImageRenderer6.transform.position },
            { ImageRenderer7, ImageRenderer7.transform.position }
        };
    }

    private IEnumerator ScheduleImage(ImageSequence imageSequence) {
        yield return new WaitForSeconds(imageSequence.StartingTime);
        StartCoroutine(ImageAnimations(imageSequence));
        
    }

    private IEnumerator ImageAnimations(ImageSequence imageSequence) {
        GameObject selectedRenderer = GetImageRenderer(imageSequence.ImagePosition);
        SpriteRenderer spriteRenderer = selectedRenderer.GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = imageSequence.Image;
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        spriteRenderer.enabled = true;

        Vector3 initialPosition = initialPositions[selectedRenderer];
        Vector3 targetPosition = initialPosition + new Vector3(0, 0, -1);

        // Move to target position with fade-in
        yield return StartCoroutine(AnimateImage(selectedRenderer, spriteRenderer, initialPosition, targetPosition, 1.0f, true));
        
        // Wait for the duration specified in the ImageSequence
        yield return new WaitForSeconds(imageSequence.Duration);
        
        // Fade-out at the target position and reset position
        yield return StartCoroutine(FadeOutImage(selectedRenderer, spriteRenderer, initialPosition, 1.0f));
    }

    private GameObject GetImageRenderer(int position) {
        return position switch
        {
            1 => ImageRenderer1,
            2 => ImageRenderer2,
            3 => ImageRenderer3,
            4 => ImageRenderer4,
            5 => ImageRenderer5,
            6 => ImageRenderer6,
            7 => ImageRenderer7,
            _ => null,
        };
    }

    private IEnumerator AnimateImage(GameObject renderer, SpriteRenderer spriteRenderer, Vector3 startPosition, Vector3 endPosition, float duration, bool fadeIn) {
        float elapsedTime = 0f;
        Color initialColor = spriteRenderer.color;
        Color targetColor = fadeIn ? new Color(initialColor.r, initialColor.g, initialColor.b, 1f) : initialColor;

        while (elapsedTime < duration) {
            renderer.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            spriteRenderer.color = Color.Lerp(initialColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        renderer.transform.position = endPosition;
        spriteRenderer.color = targetColor;
    }

    private IEnumerator FadeOutImage(GameObject renderer, SpriteRenderer spriteRenderer, Vector3 initialPosition, float duration) {
        float elapsedTime = 0f;
        Color initialColor = spriteRenderer.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        while (elapsedTime < duration) {
            spriteRenderer.color = Color.Lerp(initialColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = targetColor;
        spriteRenderer.sprite = null;
        renderer.transform.position = initialPosition; 
    }

    private IEnumerator IntroGuide(float duration) {
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _mover.MoveNext();
        _tutorialManager.LoadIntroGuide();
    }


    private void ResetAllRenderers() {
        ImageRenderer1.GetComponent<SpriteRenderer>().sprite = null;
        ImageRenderer2.GetComponent<SpriteRenderer>().sprite = null;
        ImageRenderer3.GetComponent<SpriteRenderer>().sprite = null;
        ImageRenderer4.GetComponent<SpriteRenderer>().sprite = null;
        ImageRenderer5.GetComponent<SpriteRenderer>().sprite = null;
        ImageRenderer6.GetComponent<SpriteRenderer>().sprite = null;
        ImageRenderer7.GetComponent<SpriteRenderer>().sprite = null;
    }
}
