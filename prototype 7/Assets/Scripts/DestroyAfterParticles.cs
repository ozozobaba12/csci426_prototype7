using UnityEngine;

public class DestroyAfterParticles : MonoBehaviour
{
    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();

        if (ps != null)
        {
            float totalLifetime = ps.main.duration + ps.main.startLifetime.constantMax;
            Destroy(gameObject, totalLifetime);
        }
        else
        {
            Destroy(gameObject, 0.2f);
        }
    }
}