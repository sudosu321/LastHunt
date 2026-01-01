using UnityEngine;

public class ChangeEyes : MonoBehaviour
{
    
    public void changeMat(Material mat)
    {
        GetComponent<Renderer>().material = mat;
    }
   
}
