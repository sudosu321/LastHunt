using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public float range = 100f;
    public Camera playerCamera;
    public ParticleSystem muzzleFlash;
    public GunRecoil gunRecoil;
    public GameObject bulletImpactPrefab; // assign in inspector
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        
    }

    void Shoot()
    {
        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0)
        );

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);
            if (muzzleFlash != null)
            {
                muzzleFlash.Play();
                if (gunRecoil != null)
                {
                    Debug.Log("Recoil: " + hit.collider.name);

                    gunRecoil.Recoil();
                    if (bulletImpactPrefab != null)
                    {
                        Vector3 impactPos = hit.point + hit.normal * 0.01f;
                        Quaternion impactRot = Quaternion.LookRotation(hit.normal);

                        GameObject impact = Instantiate(bulletImpactPrefab, impactPos, impactRot);
                        Destroy(impact, 10f); // destroys after 10 seconds
                        impact.transform.localScale *= Random.Range(0.8f, 1.2f);
                        impact.transform.Rotate(0f, 0f, Random.Range(0f, 360f));


                    }
                }
            }
        }
    }
}
