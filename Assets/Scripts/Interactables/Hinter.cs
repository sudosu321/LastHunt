using System;
using UnityEngine;

public class Hinter : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public String defaultMEssage;
    public String onInteract;

    void Start()
    {
        promptMessage=defaultMEssage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        
        promptMessage=onInteract;
    }
}
