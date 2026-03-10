using UnityEngine;

public class BombTarget : TargetBase
{
    public GameObject explosionEffectPrefab;

    public override void OnShot(Vector2 shotDirection)
    {
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        CameraShake shake = Camera.main.GetComponent<CameraShake>();
        if (shake != null)
        {
            shake.Shake(0.2f, 0.2f);
        }

        Destroy(gameObject);
    }
}