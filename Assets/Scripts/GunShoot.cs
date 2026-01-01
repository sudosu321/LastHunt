using System;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public float range = 1000f;
    public Camera playerCamera;
    public ParticleSystem muzzleFlash;
    public GunRecoil gunRecoil;
    public GameObject bulletImpactPrefab; // assign in inspector
    public PlayerHealth enemy;
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
                    Debug.Log("Recoil: " + hit.collider.transform.parent.name);
                    string name = hit.collider.transform.parent.name;
                    gunRecoil.Recoil();
                    if (bulletImpactPrefab != null)
                    {
                        Vector3 impactPos = hit.point + hit.normal * 0.01f;
                        Quaternion impactRot = Quaternion.LookRotation(hit.normal);
                        //create game objects
                        GameObject impact = Instantiate(bulletImpactPrefab, impactPos, impactRot);
                        impact.transform.SetParent(hit.collider.transform);
                        Destroy(impact, 10.00f); // destroys after 10 seconds
                        impact.transform.localScale *= UnityEngine.Random.Range(0.8f, 1.2f);
                        impact.transform.Rotate(0f, 0f, UnityEngine.Random.Range(0f, 360f));
                    }
                if(name.Contains("Enemy"))
                {
                    enemy.Damage(20,30);     
                }
                }
            }
        }
    }
}
