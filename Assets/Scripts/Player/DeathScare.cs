using UnityEngine;
using System.Collections;

public class DeathScare : MonoBehaviour
{
    [Header("References")]
    public Camera playerCam;
    public Transform enemyFace;
    public Transform enemyhead;

    public AudioSource scareSound;
    public AudioSource stomp;


    [Header("Settings")]
    public float zoomFOV = 30f;
    public float zoomSpeed = 8f;
    public float shakeIntensity = 0.02f;
    public float duration = 2f;

    private float originalFOV;
    private Vector3 originalCamPos;

    void Start()
    {
        originalFOV = playerCam.fieldOfView;
        originalCamPos = playerCam.transform.localPosition;
    }

    public void PlayDeathScare()
    {
        GetComponentInChildren<PlayerLook>().enabled = false;
        GetComponentInChildren<MouseLook>().enabled = false;

        GetComponent<PlayerMovement>().enabled = false;
        scareSound.Play();
        StartCoroutine(JumpscareRoutine());
    }

    IEnumerator JumpscareRoutine()
    {
        float timer = 0f;   
        enemyFace.position =
        playerCam.transform.position +
        playerCam.transform.forward * 6f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            //enemyFace.transform.LookAt(playerCam.transform);
            // Look at enemy
            playerCam.transform.LookAt(enemyhead);
            if (timer > duration / 2)
            {
                if (stomp != null)
                {
                    if(stomp.isPlaying==false)stomp.Play();
                }
            }
            // Zoom in
            playerCam.fieldOfView = Mathf.Lerp(
                playerCam.fieldOfView,
                zoomFOV,
                Time.deltaTime * zoomSpeed
            );

            // Camera shake
            playerCam.transform.localPosition = originalCamPos + Random.insideUnitSphere * shakeIntensity;


            yield return null;
        }
        
        // Fade to black / restart
        yield return new WaitForSeconds(0.8f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
}
