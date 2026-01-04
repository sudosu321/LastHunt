using UnityEngine;

public class PickGun : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private GameObject gun;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        gun.SetActive(true);
        Destroy(gameObject);
    }
}
