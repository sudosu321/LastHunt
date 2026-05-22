using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Explosion : MonoBehaviour
{
    public float delay = 1f;
    public float life = 30;
    public AudioSource expSound;
    public GameObject prt;
    public PlayerHold player;
    public NavMeshSurface nav;
    public void Start()
    {
        nav=FindAnyObjectByType<NavMeshSurface>();
   }
    public void Explode()
    {
        if(prt!=null)

        Instantiate(prt,gameObject.transform.position,gameObject.transform.rotation);

        Destroy(gameObject);
        if(expSound!=null)
        expSound.Play();
        if(nav!=null)nav.BuildNavMesh();
    }
    void desobj()
    {
        
    }
}
