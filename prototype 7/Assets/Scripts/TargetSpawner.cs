using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject staticTargetPrefab;
    public GameObject toughTargetPrefab;
    public GameObject bombTargetPrefab;
    public GameObject droneTargetPrefab;

    public Transform bottomLeft;
    public Transform bottomMiddle;
    public Transform bottomRight;
    public Transform topLeft;
    public Transform topRight;

    public float spawnInterval = 1.25f;
    private float timer;

    void Start()
    {
        SpawnStaticTarget();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnRandomTarget();
            timer = spawnInterval;
        }
    }

    void SpawnRandomTarget()
    {
        int roll = Random.Range(0, 4);

        switch (roll)
        {
            case 0:
                SpawnStaticTarget();
                break;
            case 1:
                SpawnToughTarget();
                break;
            case 2:
                SpawnBombTarget();
                break;
            case 3:
                SpawnDroneTarget();
                break;
        }
    }

    void SpawnStaticTarget()
    {
        Vector3 pos = new Vector3(Random.Range(-7f, 7f), Random.Range(-1f, 3.5f), 0f);
        Instantiate(staticTargetPrefab, pos, Quaternion.identity);
    }

    void SpawnToughTarget()
    {
        Transform spawn = GetRandomBottomSpawn();
        GameObject obj = Instantiate(toughTargetPrefab, spawn.position, Quaternion.identity);

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 force = new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(7f, 9f));
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    void SpawnBombTarget()
    {
        Transform spawn = GetRandomBottomSpawn();
        GameObject obj = Instantiate(bombTargetPrefab, spawn.position, Quaternion.identity);

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 force = new Vector2(Random.Range(-2f, 2f), Random.Range(8f, 11f));
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    void SpawnDroneTarget()
    {
        Transform spawn = Random.value > 0.5f ? topLeft : topRight;
        Instantiate(droneTargetPrefab, spawn.position, Quaternion.identity);
    }

    Transform GetRandomBottomSpawn()
    {
        int roll = Random.Range(0, 3);

        if (roll == 0) return bottomLeft;
        if (roll == 1) return bottomMiddle;
        return bottomRight;
    }
}