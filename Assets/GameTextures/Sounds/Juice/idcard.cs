using UnityEngine;

public class idcard : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject controls;
    public GameObject idcardd;


    void Start()
    {
        promptMessage="security gaurd's id card";
    }
    public void close()
    {
        player.isIdCardOpened=false;

        controls.SetActive(true);
        idcardd.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
       
    }
    protected override void Interact()
    {
        player.isIdCardOpened=true;
        controls.SetActive(false);
        idcardd.SetActive(true);
    }
}
