using UnityEngine;

public class Torch : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject torchLight;
    public GameObject torchIcon;
    public GameObject torchItem;
    public Light l1;
    void Start()
    {
        torchItem=gameObject;
        promptMessage="an electric torch";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        l1.intensity=1f;
        player.torchLight=torchLight;
        player.torchIcon=torchIcon;
        player.torchItem=torchItem;
        torchLight.SetActive(true);
        torchIcon.SetActive(true);
        torchItem.transform.SetParent(player.transform);
        torchItem.SetActive(false);
        player.toggleTorch();

    }
}
