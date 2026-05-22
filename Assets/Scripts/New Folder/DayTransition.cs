using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class DayTransition : MonoBehaviour
{
    public Image blackScreen;
    public TextMeshProUGUI dayText;

    public float fadeSpeed = 2f;
    public float textDisplayTime = 2f;

    void Start()
    {
        StartCoroutine(StartSequence("FIRST NIGHT"));
    }
    
    public IEnumerator StartSequence(string day)
    {
        
        // then your existing intro text
        dayText.text = day;
        yield return new WaitForSeconds(textDisplayTime);
        yield return FadeText(0);
        yield return Fade(0);

    }

    public IEnumerator DeathSequence(string nextDay)
    {
        dayText.text = nextDay;
        yield return Fade(1);
        yield return FadeText(1);

        yield return new WaitForSeconds(textDisplayTime);
        yield return FadeText(0);
        yield return Fade(0);
    }

    IEnumerator Fade(float targetAlpha)
    {
        float start = blackScreen.color.a;
        while (!Mathf.Approximately(start, targetAlpha))
        {
            start = Mathf.MoveTowards(start, targetAlpha, fadeSpeed * Time.deltaTime);
            blackScreen.color = new Color(0, 0, 0, start);
            yield return null;
        }
    }

    IEnumerator FadeText(float target)
    {
        float start = dayText.alpha;
        while (!Mathf.Approximately(start, target))
        {
            start = Mathf.MoveTowards(start, target, fadeSpeed * Time.deltaTime);
            Color c = dayText.color;
            c.a = start;
            dayText.color = c;
            yield return null;
        }
    }
}