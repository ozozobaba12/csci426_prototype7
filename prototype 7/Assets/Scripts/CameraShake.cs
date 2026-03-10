using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    Vector3 originalPos;

    void Awake()
    {
        originalPos = transform.position;
    }

    public void Shake(float duration, float magnitude)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Vector3 offset = Random.insideUnitCircle * magnitude;
            transform.position = originalPos + offset;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }
}