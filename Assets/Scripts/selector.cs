    using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class selector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Toggle bt;
     public TMP_Dropdown difficultyDropdown;
     public TextMeshProUGUI  textMeshPro;
    void Start()
    {
        GameSettings.Instance.difficulty = 0;
        bt.interactable=false;
        bt.isOn=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void starter()
    {
        SceneManager.LoadScene("intro_scene");
    }
    public void onToggleChange()
    {
        GameSettings.Instance.sprintDetect = bt.isOn;

        // 0 = Practice, 1 = Easy, 2 = Extreme
       
    }
    public void onChange()
    {
          int val = difficultyDropdown.value;
        // 0 = Practice, 1 = Easy, 2 = Extreme
        GameSettings.Instance.difficulty = val;
        if (val == 0)
        {
            bt.isOn=false;
             bt.interactable=false;
            textMeshPro.SetText("practice mode allows you to explore the map");
        }
        else if (val == 1)
        {
            bt.isOn=true;

             bt.interactable=true;
            textMeshPro.SetText("Machine walks slower, dark, good for cowards");
        }
         else if (val == 2)
        {
             bt.interactable=false;
            bt.isOn=true;
            textMeshPro.SetText("Machine walks faster, darker, only if you are a man");
        }
    }

    public void back()
    {
        SceneManager.LoadScene("main_menu");
    }
}
