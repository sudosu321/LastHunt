using UnityEngine;

public class LyingScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        animator.Play("Idle_14_ao|Laying", 0, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
