using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;
    [SerializeField]
    private TextMeshProUGUI hintText;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void updateText(string m)
    {
        promptText.text=m;
    }
}
