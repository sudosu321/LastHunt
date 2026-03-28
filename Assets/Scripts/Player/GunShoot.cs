using System;
using UnityEngine;


public class GunShoot : MonoBehaviour
{
    private float range = 40f;
    public Camera playerCamera;
    public ParticleSystem muzzleFlash;
    public GunRecoil gunRecoil;
    public GameObject bulletImpactPrefab; // assign in inspector
    public Enemy enemy;
    public GameObject gun;
    public float damage;
    public float impulse;
    public int bulletCount=5;
    public AudioSource gunShot;
    void Start()
    {
 
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))    
        {
           //OnFire(); [desktop]
        }
    }
    public void OnFire()
    {
        if (bulletCount != 0)
        {
            if (gun.activeSelf)
            {
                if (!gunShot.isPlaying)
                {
                    bulletCount--;
                    Shoot();
                } 
            }  
        }
    }

    void Shoot()
    {
        enemy.explicitDiscover=true;
        enemy.pos=playerCamera.transform.position;
        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0)
        );
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            if (muzzleFlash != null)
            {
                if (gunRecoil != null)
                {
                    gunRecoil.Recoil();
                    if (!muzzleFlash.isPlaying)
                        muzzleFlash.Play();
                    Rigidbody rb = hit.collider.attachedRigidbody;
                    if(!gunShot.isPlaying)gunShot.Play();
                    if (rb != null)
                    {
                        Vector3 forceDir = ray.direction;
                        float impactForce = impulse; 
                        rb.AddForceAtPosition(forceDir * impactForce, hit.point, ForceMode.Impulse);
                    }
                    if (hit.collider.name.Contains("ENEMY"))
                    {
                        enemy.Damage();
                    }
                    PlayerHealth playerHealth =
                    hit.collider.GetComponentInParent<PlayerHealth>();

                    if (playerHealth != null)
                    {
                            playerHealth.Damage(damage,damage+10);
                            return;
                    }
                   enemy.explicitDiscover=true;
                   enemy.lastKnownPlayerPosition=gun.transform.position;
                }
            }
        }
    }
}
