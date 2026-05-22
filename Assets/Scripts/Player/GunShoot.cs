using System;
using TMPro;
using UnityEngine;


public class GunShoot : MonoBehaviour
{
    private float range = 40f;
    public Camera playerCamera;
    public ParticleSystem muzzleFlash;
    public GunRecoil gunRecoil;
    public GameObject bulletImpactPrefab; // assign in inspector
    public Enemy enemy1;
    public Enemy enemy2;

    public GameObject gun;
    public float damage;
    public float impulse;
    public int bulletCount=5;
    public AudioSource gunShot;
    public TextMeshProUGUI text;
    void Start()
    {
 
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))    
        {
          // OnFire(); 
        }
    }
    public void updateText()
    {
        text.SetText("Ammo : "+bulletCount);
        
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
        updateText();
        if (!enemy1.isDead)
        {
            enemy1.explicitDiscover=true;
            enemy1.pos=playerCamera.transform.position;
        }
       
        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0)
        );
        LayerMask detectionMask = ~LayerMask.GetMask("Interactable");

        if (Physics.Raycast(ray ,out RaycastHit hit, range, detectionMask))
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
                    if (hit.collider.name.Contains("ENEMY01"))
                    {
                        enemy1.Damage();
                        

                    }
                    if (hit.collider.name.Contains("ENEMY02"))
                    {
                        enemy2.Damage();
                        

                    }
                    PlayerHealth playerHealth =
                    hit.collider.GetComponentInParent<PlayerHealth>();

                    if (playerHealth != null)
                    {
                            playerHealth.Damage(damage,damage+10);
                            return;
                    }
                  
                }
            }
        }
        else
        {
              if (muzzleFlash != null)
            {
                if (gunRecoil != null)
                {
                    gunRecoil.Recoil();
                    if (!muzzleFlash.isPlaying)
                        muzzleFlash.Play();
                    if(!gunShot.isPlaying)gunShot.Play();
                }
            }
        }
    }
}
