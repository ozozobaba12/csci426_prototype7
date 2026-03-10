using UnityEngine;

public class StaticTarget : TargetBase
{
    public GameObject hitEffectPrefab;

    public override void OnShot(Vector2 shotDirection)
    {
        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}