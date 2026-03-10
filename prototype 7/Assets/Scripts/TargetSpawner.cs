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

    public float roundLength = 35f;
    private bool spawningStopped = false;

    public int maxToughTargetsOnScreen = 3;
    public float spawnInterval = 1.25f;
    public int maxTargetsOnScreen = 6;

    public bool spawningStarted = false;

    private float timer;
    private float gameTime;

    void Start()
    {
        SpawnStarterTarget();
    }

    void Update()
    {
        if (!spawningStarted)
            return;

        if (GameManager.Instance != null && (GameManager.Instance.gameOver || GameManager.Instance.hasWon))
            return;

        gameTime += Time.deltaTime;
        timer -= Time.deltaTime;

        if (!spawningStopped && gameTime >= roundLength)
        {
            spawningStopped = true;
        }

        if (!spawningStopped && timer <= 0f)
        {
            int currentTargets = FindObjectsByType<TargetBase>(FindObjectsSortMode.None).Length;

            if (currentTargets < maxTargetsOnScreen)
            {
                SpawnByPhase();
            }

            timer = spawnInterval;
        }

        if (spawningStopped)
        {
            int currentTargets = FindObjectsByType<TargetBase>(FindObjectsSortMode.None).Length;

            if (currentTargets == 0)
            {
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.TriggerWin();
                }
            }
        }
    }

    public void BeginSpawning()
    {
        spawningStarted = true;
        timer = 0f;
    }

    void SpawnStarterTarget()
    {
        Vector3 pos = new Vector3(0f, 1.5f, 0f);
        GameObject obj = Instantiate(staticTargetPrefab, pos, Quaternion.identity);

        StaticTarget starter = obj.GetComponent<StaticTarget>();
        if (starter != null)
        {
            starter.isStarterTarget = true;
        }
    }

    void SpawnByPhase()
    {
        if (gameTime < 8f)
        {
            SpawnStaticTarget();
        }
        else if (gameTime < 16f)
        {
            int roll = Random.Range(0, 2);
            if (roll == 0) SpawnStaticTarget();
            else SpawnToughTarget();
        }
        else if (gameTime < 28f)
        {
            int roll = Random.Range(0, 3);
            if (roll == 0) SpawnStaticTarget();
            else if (roll == 1) SpawnToughTarget();
            else SpawnBombTarget();
        }
        else
        {
            int roll = Random.Range(0, 4);
            if (roll == 0) SpawnStaticTarget();
            else if (roll == 1) SpawnToughTarget();
            else if (roll == 2) SpawnBombTarget();
            else SpawnDroneTarget();
        }
    }

    void SpawnStaticTarget()
    {
        Vector3 pos = new Vector3(Random.Range(-7f, 7f), Random.Range(-1f, 3.5f), 0f);
        Instantiate(staticTargetPrefab, pos, Quaternion.identity);
    }

    void SpawnToughTarget()
    {
        if (CountTargetsOfType<ToughTarget>() >= maxToughTargetsOnScreen)
            return;

        Transform spawn = GetRandomBottomSpawn();
        GameObject obj = Instantiate(toughTargetPrefab, spawn.position, Quaternion.identity);

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 force = new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(7f, 9f));
            rb.linearVelocity = Vector2.zero;
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
            rb.linearVelocity = Vector2.zero;
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

    int CountTargetsOfType<T>() where T : TargetBase
    {
        return FindObjectsByType<T>(FindObjectsSortMode.None).Length;
    }
}