using UnityEngine;

public class LightChear : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Interact()
    {
        player.GetComponent<TaskElectric>().lights();

    }
}
