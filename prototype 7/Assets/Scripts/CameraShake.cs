using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPos;
    private Coroutine currentShake;

    void Awake()
    {
        originalPos = transform.position;
    }

    public void Shake(float duration, float magnitude)
    {
        if (currentShake != null)
        {
            StopCoroutine(currentShake);
        }

        currentShake = StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Vector2 offset2D = Random.insideUnitCircle * magnitude;
            transform.position = originalPos + new Vector3(offset2D.x, offset2D.y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
        currentShake = null;
    }
}