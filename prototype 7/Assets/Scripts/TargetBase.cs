using UnityEngine;

public abstract class TargetBase : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    public abstract void OnShot(Vector2 shotDirection);
}