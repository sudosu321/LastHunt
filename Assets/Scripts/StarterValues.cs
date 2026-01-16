using UnityEngine;

public class StarterValues : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool MODE_DEBUG=false;
    public PlayerMovement player;
    public int bulletCount=1000;
    public float PLAYER_GUN_DAMAGE=20;
    public float ENEMY_GUN_DAMAGE=20;
    public float ENEMY_HEALTH_INITIAL=20;
    public float ENEMY_SPEED=20;
    public float ENEMY_ATTACK_DIST=20;

    public float ENEMY_CHASE_DIST=20;
    public int COMEBACK_TIME;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
