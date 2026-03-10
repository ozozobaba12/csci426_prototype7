using UnityEngine;

public class GunController : MonoBehaviour
{
    public Camera cam;
    public Transform barrelTip;
    public float shootRange = 100f;
    public LayerMask shootLayer;
    public GameObject muzzleFlashPrefab;
    public AudioSource audioSource;
    public AudioClip gunshotSFX;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        Vector2 dir = (mouseWorld - barrelTip.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(barrelTip.position, dir, shootRange, shootLayer);

        if (muzzleFlashPrefab != null)
        {
            Instantiate(muzzleFlashPrefab, barrelTip.position, Quaternion.identity);
        }

        CameraShake shake = Camera.main.GetComponent<CameraShake>();
        if (shake != null)
        {
            shake.Shake(0.1f, 0.1f);
        }

        if (audioSource != null && gunshotSFX != null)
        {
            audioSource.PlayOneShot(gunshotSFX);
        }

        if (hit.collider != null)
        {
            TargetBase target = hit.collider.GetComponent<TargetBase>();
            if (target != null)
            {
                target.OnShot(dir);
            }
        }
    }
}