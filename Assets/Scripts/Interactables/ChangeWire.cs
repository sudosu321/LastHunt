using UnityEngine;

public class ChangeWire : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform wires;
    public Transform[] wiresAll;

    public Material toChange;
    void Start()
    {
        
    }
    public void change()
    {
        if(wires==null)return;
        wires.GetComponent<Renderer>().material=toChange;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void colorAllWires()
    {
        for(int i = 0; i < wiresAll.Length; i++)
        {
            if (wiresAll[i] != null)
            {
                wiresAll[i].GetComponent<Renderer>().material= toChange;
            }
        }
    }
}
