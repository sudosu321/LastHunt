using UnityEngine;
using UnityEngine.AI;

public class Explosion : MonoBehaviour
{
    public int cubesPerAxis = 6;
    public float delay = 1f;
    public float force = 300f;
    public float radius = 2f;
    public float life = 30;
    public bool isSuperExploder=false;
    public GameObject desServer;
    public PlayerHold player;
    public void Explode()
    {
        for (int x = 0; x < cubesPerAxis; x++)
        {
            for (int y = 0; y < cubesPerAxis; y++)
            {
                for (int z = 0; z < cubesPerAxis; z++)
                {
                    CreateCube(new Vector3(x, y, z));
                }
            }
        }
        if(gameObject.GetComponent<Respawn>()==null)
            if (isSuperExploder)
            {
                player.isCorruptedServerDestroyed=true;
                Destroy(desServer);

                Destroy(gameObject);  
            }
            else
            {
                Destroy(gameObject);  
            }
        else
            gameObject.SetActive(false);
    }
    void desobj()
    {
        
    }
    void CreateCube(Vector3 coordinates)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        Renderer rd = cube.GetComponent<Renderer>();
        rd.material = GetComponent<Renderer>().material;

        cube.transform.localScale = transform.localScale / cubesPerAxis;

        Vector3 firstCube =
            transform.position
            - transform.localScale / 2
            + cube.transform.localScale / 2;

        cube.transform.position =
            firstCube + Vector3.Scale(coordinates, cube.transform.localScale);

        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.AddExplosionForce(force, transform.position, radius);

        Destroy(cube,life);
    }
}
