using UnityEngine;

public class CameraRender : MonoBehaviour
{
    private Camera cam;
    private float timer;
    private float randomInterval;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.enabled = false;
        randomInterval = Random.Range(0.5f, 1f);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= randomInterval)
        {
            cam.Render();
            timer = 0f;
            randomInterval = Random.Range(0.5f, 1f); // new random interval each time
        }
    }
}