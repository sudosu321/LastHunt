using UnityEngine;
using UnityEngine.UI;

public class BloodEffect : MonoBehaviour
{
    public Image bloodImage;
    public float flashAlpha = 0.5f;
    public float fadeSpeed = 3f;

    void Update()
    {
        if (bloodImage.color.a > 0)
        {
            Color c = bloodImage.color;
            c.a = Mathf.Lerp(c.a, 0, Time.deltaTime * fadeSpeed);
            bloodImage.color = c;
        }
    }

    public void ShowBlood()
    {
        Color c = bloodImage.color;
        c.a = flashAlpha;
        bloodImage.color = c;
    }
}