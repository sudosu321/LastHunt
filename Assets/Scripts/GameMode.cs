using UnityEngine;

public class GameMode : MonoBehaviour
{
    public bool pc=true;//pc=!andorid
    public GameObject controls;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (pc)
        {
            GetComponent<PlayerHold>().ispc=true;
            Destroy(controls);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
