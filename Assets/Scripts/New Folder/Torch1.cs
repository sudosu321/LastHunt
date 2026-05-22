using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Torch1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject torchLight;
    public bool torchOn=true;
    public Light light1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onPress()
    {   
        torchOn=!torchOn;
        if (torchOn)
        {
            GetComponent<PlayerHold>().toggleTorch();
            if(GetComponent<PlayerHold>().electricTaskComplete==false)
                light1.intensity=1f;
            torchLight.SetActive(true);
        }
        else
        {
            if(GetComponent<PlayerHold>().electricTaskComplete==false)light1.intensity=0.3f;

            GetComponent<PlayerHold>().toggleTorch();
            torchLight.SetActive(false);
        }
    }
}
