using UnityEngine;

public class ghost : Interactable

{
    public Material ghostMat;
    public AudioSource huwahh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ghostMat=GetComponent<Renderer>().material;
        GetComponent<Renderer>().enabled=false;
    }
    protected override void enterState()
    {
        GetComponent<Renderer>().enabled=true;
            
            if(!huwahh.isPlaying)
            huwahh.Play();
         Invoke("setBlank",1.5f);

    }
    // Update is called once per frame
    void Update()
    {
         
    }
    public void setBlank()
    {
        
        Destroy(gameObject);
    }
}
