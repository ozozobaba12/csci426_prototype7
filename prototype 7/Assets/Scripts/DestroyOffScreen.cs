using UnityEngine;

public class DestroyOffscreen : MonoBehaviour
{
    public float leftBound = -10f;
    public float rightBound = 10f;
    public float lowerBound = -7f;
    public float upperBound = 7f;
    public bool countsAsMiss = true;

    void Update()
    {
        Vector3 pos = transform.position;

        if (pos.x < leftBound || pos.x > rightBound || pos.y < lowerBound || pos.y > upperBound)
        {
            if (countsAsMiss && GameManager.Instance != null)
            {
                GameManager.Instance.RegisterMiss();
            }

            Destroy(gameObject);
        }
    }
}