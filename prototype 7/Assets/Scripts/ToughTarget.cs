using UnityEngine;

public class ToughTarget : TargetBase
{
    public int hitsRemaining = 3;
    public float bounceForce = 7f;
    public Color healthyColor = Color.yellow;
    public Color mediumColor = new Color(1f, 0.6f, 0.2f);
    public Color weakColor = Color.red;
    public GameObject hitEffectPrefab;

    void Start()
    {
        UpdateColor();
    }

    public override void OnShot(Vector2 shotDirection)
    {
        hitsRemaining--;

        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }

        if (hitsRemaining <= 0)
        {
            Destroy(gameObject);
            return;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
        }

        UpdateColor();
    }

    void UpdateColor()
    {
        if (sr == null) return;

        if (hitsRemaining == 3) sr.color = healthyColor;
        else if (hitsRemaining == 2) sr.color = mediumColor;
        else sr.color = weakColor;
    }
}