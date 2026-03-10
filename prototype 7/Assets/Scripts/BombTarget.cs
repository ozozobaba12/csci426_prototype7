using UnityEngine;

public class BombTarget : TargetBase
{
    public GameObject explosionEffectPrefab;
    public AudioClip explosionSFX;

    public override void OnShot(Vector2 shotDirection)
    {
        if (explosionSFX != null)
        {
            AudioSource.PlayClipAtPoint(explosionSFX, transform.position, 1f);
        }

        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        CameraShake shake = Camera.main.GetComponent<CameraShake>();
        if (shake != null)
        {
            shake.Shake(0.2f, 0.2f);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterBombShot();
        }

        Destroy(gameObject);
    }
}