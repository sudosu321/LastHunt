
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float playerHealth=100f;
    public float maxHealth=100f;
    public bool isAlive=true;
    public Explosion explosion;
    public int Spawn_time=45;
    private Vector3 initPos;
    void Start()
    {
        initPos=transform.position;;
        if (GetComponent<StarterValues>() != null)
        {
             Spawn_time=GetComponent<StarterValues>().COMEBACK_TIME;
             explosion.life=Spawn_time+2;
        }
           
        maxHealth=playerHealth;
    }   
 
    public void Damage(float min,float max)
    {
        if (isAlive)
        {
            float randomFloat = Random.Range(min, max);
            playerHealth=playerHealth-randomFloat;
            if (playerHealth < 0f)
            {
                isAlive=false;
                Invoke("destruct",explosion.delay);
            }
        }
    }
    void destruct()
    {   
        explosion.Explode();
        Respawn obj=gameObject.GetComponent<Respawn>();
        if (obj!=null)
        {
            Invoke("Spawn",Spawn_time);
        }
    }
    void Spawn()
    {
        isAlive=true;
        playerHealth=maxHealth;
        transform.position=initPos;
        gameObject.SetActive(true);
    }
}
