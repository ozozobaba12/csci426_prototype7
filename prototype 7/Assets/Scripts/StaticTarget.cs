using UnityEngine;

public class StaticTarget : TargetBase
{
    public GameObject hitEffectPrefab;
    public bool isStarterTarget = false;

    public AudioSource audioSource;
    public AudioClip spawnSFX;
    public AudioClip hitSFX;

    public float lifetimeBeforeMiss = 5f;

    private float timer;

    void Start()
    {
        timer = lifetimeBeforeMiss;

        if (audioSource != null && spawnSFX != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(spawnSFX);
        }
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.gameOver)
            return;

        timer -= Time.deltaTime;

        if (timer < 1.5f)
        {
            sr.color = Color.Lerp(Color.white, Color.red, 1f - (timer / 1.5f));
        }

        if (timer <= 0f)
        {
            MissedTarget();
        }
    }

    void MissedTarget()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterMiss();
        }

        Destroy(gameObject);
    }

    public override void OnShot(Vector2 shotDirection)
    {
        if (hitSFX != null)
        {
            AudioSource.PlayClipAtPoint(hitSFX, transform.position, 1f);
        }

        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }

        if (isStarterTarget)
        {
            TargetSpawner spawner = FindFirstObjectByType<TargetSpawner>();
            if (spawner != null)
            {
                spawner.BeginSpawning();
            }
        }

        Destroy(gameObject);
    }
}