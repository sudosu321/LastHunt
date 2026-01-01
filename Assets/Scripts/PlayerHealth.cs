using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float playerHealth=100f;
    public Enemy enemy;
    public bool isAlive=true;
    public void Damage(float min,float max)
    {
        if (isAlive)
        {
            float randomFloat = Random.Range(min, max);

            playerHealth=playerHealth-randomFloat;
            Debug.Log("Player Health : "+playerHealth);
            if (playerHealth < 0f)
            {
                isAlive=false;
            }
        }
        else
        {
            Debug.Log("Player Dead");
        }
    }
}
