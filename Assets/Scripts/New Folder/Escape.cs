using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
using UnityEngine.SceneManagement;

public class Escape : Interactable
{
    public Image blackScreen;
    public float fadeSpeed = 2f;

    void Start()
    {
        
    }

    protected override void Interact()
    {
        StartCoroutine(DullScreen());
        Invoke("neww",5);
    }
    public void neww()
    {
        SceneManager.LoadScene("final_scene");
        
    }
    public IEnumerator DullScreen()
    {
        yield return StartCoroutine(Fade(1));
    }

    IEnumerator Fade(float targetAlpha)
    {
        float current = blackScreen.color.a;
        while (!Mathf.Approximately(current, targetAlpha))
        {
            current = Mathf.MoveTowards(current, targetAlpha, fadeSpeed * Time.deltaTime);
            blackScreen.color = new Color(0, 0, 0, current);
            yield return null;
        }
    }
}