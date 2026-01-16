using UnityEngine;

public class Server : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        taskActive=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.SERVER_FEUL_TASK)
        {
            promptMessage="Server is on";
        }
        else
        {
            promptMessage="servers are off";
        }
    }

}
