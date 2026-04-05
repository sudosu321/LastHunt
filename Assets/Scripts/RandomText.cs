using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RandomText : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    string[] deathMessages = new string[]
{
    // Dark / Psychological
    "It was watching long before you noticed.",
    "You were never alone.",
    "It didn’t chase you… it waited.",
    "You remembered too late.",
    "Some doors should stay closed.",
    "It learned how you breathe.",

    // Mind-bending / Unsettling
    "Did you really die… or just wake up?",
    "This has happened before.",
    "You’ve already made this mistake.",
    "It knew where you would run.",
    "You chose this path.",

    // Entity / Stalker
    "I saw you.",
    "You can’t hide from me.",
    "I was right behind you.",
    "Run again.",
    "This time, try harder.",

    // Loop / Day system
    "Day resets. It remembers.",
    "Another day. Same fate.",
    "You made it this far… again.",
    "It gets closer every day.",
    "You’re running out of days.",

    // Minimal
    "Too slow.",
    "Caught.",
    "Seen.",
    "Over.",
    "Again."
};
    public TextMeshProUGUI text;
    public Image blackScreen;

    public float fadeSpeed = 2f;
    public float displayTime = 3f;
    void Start()
    {
        string randomMessage = deathMessages[Random.Range(0, deathMessages.Length)];
        text.SetText(randomMessage);
          // start sequence
        StartCoroutine(DeathSequence());
    }
    IEnumerator DeathSequence()
    {
        yield return Fade(1f);

        yield return new WaitForSeconds(displayTime);

        yield return Fade(0f);
        Invoke("laod",2);
       
    }
    public void laod()
    {
         SceneManager.LoadScene("main_menu");
    }
    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = blackScreen.color.a;

        while (!Mathf.Approximately(startAlpha, targetAlpha))
        {
            startAlpha = Mathf.MoveTowards(startAlpha, targetAlpha, fadeSpeed * Time.deltaTime);
            // apply to background
            blackScreen.color = new Color(0, 0, 0, startAlpha);
            // apply to text    
             Color c = text.color;
            c.a = startAlpha;
            text.color = c;

            yield return null;
        }
    }
}