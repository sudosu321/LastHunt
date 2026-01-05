
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float playerHealth=100f;
    public float maxHealth=100f;

    public bool isAlive=true;
    public Explosion explosion;
    public Image healthBar;

    private RectTransform r;
    private float currenWidth;
    void Start()
    {
        maxHealth=playerHealth;
        if (healthBar != null)
        {
            r=healthBar.rectTransform;
        }
    }   
    public void MoveBar(float a)
    {
        float newWidth=a*0.80f ;
        r.anchoredPosition += Vector2.right * newWidth/2;
        newWidth=-newWidth;
        currenWidth+=(newWidth);
        r.sizeDelta = new Vector2(currenWidth, r.sizeDelta.y);
    }   
    public void Damage(float min,float max)
    {
        if (isAlive)
        {
            float randomFloat = Random.Range(min, max);
            playerHealth=playerHealth-randomFloat;
            if (healthBar != null){
                MoveBar(-randomFloat);
            }
            Debug.Log("Player Health : "+playerHealth);
            
            if (playerHealth < 0f)
            {
                isAlive=false;
                Invoke("destruct",1);
            }
        }
        else
        {
            Debug.Log("Player Dead");
        }
    }
    void destruct()
    {   
        explosion.Explode();
        Respawn obj=gameObject.GetComponent<Respawn>();
        if (obj!=null)
        {
            Invoke("Spawn",45);
        }
    }
    void Spawn()
    {
        isAlive=true;
        playerHealth=100f;
        gameObject.SetActive(true);
        
    }
}
