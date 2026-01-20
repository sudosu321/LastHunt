using UnityEngine;


public class GunShoot : MonoBehaviour
{
    private float range = 100f;
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
        if (bulletCount > 0)
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
        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0)
        );
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            //Debug.Log("Hit: " + hit.collider.name);
            if (muzzleFlash != null)
            {
                
                if (gunRecoil != null)
                {
                    Transform parent = hit.collider.transform.parent;

                    string name = parent != null ? parent.name : hit.collider.name;
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
                    /*if (bulletImpactPrefab != null)
                    {
                        Vector3 impactPos = hit.point + hit.normal * 0.01f;
                        Quaternion impactRot = Quaternion.LookRotation(hit.normal);
                        /*GameObject impact = Instantiate(bulletImpactPrefab, impactPos, impactRot);
                        impact.transform.SetParent(hit.collider.transform);
                        Destroy(impact, 10.00f); // destroys after 10 seconds
                        impact.transform.localScale *= UnityEngine.Random.Range(0.8f, 1.2f);
                        impact.transform.Rotate(0f, 0f, UnityEngine.Random.Range(0f, 360f));*/
                    
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
                    enemy.pos=playerCamera.transform.position;
                }
            }
        }
    }
}
