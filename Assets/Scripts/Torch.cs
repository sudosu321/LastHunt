using UnityEngine;

public class Torch : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject torch;
    void Start()
    {
        promptMessage="an electric torch";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        torch.SetActive(true);
        Destroy(gameObject);

    }
}
