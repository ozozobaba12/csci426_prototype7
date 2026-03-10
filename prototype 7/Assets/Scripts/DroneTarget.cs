using UnityEngine;

public class DroneTarget : TargetBase
{
    public float speed = 3f;
    public float directionChangeTime = 0.8f;

    private Vector2 moveDir;
    private float timer;

    void Start()
    {
        PickInwardStartDirection();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            PickNewDirection();
        }

        transform.position += (Vector3)(moveDir * speed * Time.deltaTime);
    }

    void PickInwardStartDirection()
    {
        if (transform.position.x < 0f)
        {
            moveDir = new Vector2(Random.Range(0.4f, 1f), Random.Range(-0.5f, 0.5f)).normalized;
        }
        else
        {
            moveDir = new Vector2(Random.Range(-1f, -0.4f), Random.Range(-0.5f, 0.5f)).normalized;
        }

        timer = directionChangeTime;
    }

    void PickNewDirection()
    {
        float xMin = transform.position.x < 0f ? 0.1f : -1f;
        float xMax = transform.position.x < 0f ? 1f : -0.1f;

        moveDir = new Vector2(Random.Range(xMin, xMax), Random.Range(-0.6f, 0.4f)).normalized;
        timer = directionChangeTime;
    }

    public override void OnShot(Vector2 shotDirection)
    {
        Destroy(gameObject);
    }
}