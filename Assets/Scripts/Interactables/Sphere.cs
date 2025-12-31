using UnityEngine;
using UnityEngine.Rendering;

public class Sphere : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private GameObject door;
    private bool doorOpen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        doorOpen=!doorOpen;
        door.GetComponent<Animator>().SetBool("isOpen",doorOpen);
    }
}
