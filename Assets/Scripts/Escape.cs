using UnityEngine;
using UnityEngine.SceneManagement;
public class Escape : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    protected override void Interact()
    {
        SceneManager.LoadScene("final_scene");

    }
}
