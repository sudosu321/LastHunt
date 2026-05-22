using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class DeathScare : MonoBehaviour
{
    [Header("References")]
    public Camera playerCam;
    public Transform enemyFace;
    public Transform enemyhead;

    public AudioSource scareSound;
    public AudioSource stomp;
    public Enemy enemy;
    public PlayerHold player;
    [Header("Settings")]
    public float zoomFOV = 30f;
    public float zoomSpeed = 8f;
    public float shakeIntensity = 0.02f;
    public float duration = 2f;
    public GameObject contr;
    public GameObject NOTE;
    public GameObject idobj;
    public GameObject comp;


    public GameObject gun;
    public Transform gunPos;
    private float originalFOV;
    private Vector3 originalCamPos;
    public int lives=3;
    public int deaths=0;
    public Transform checkpoint;
    void Start()
    {
        originalFOV = playerCam.fieldOfView;
        originalCamPos = playerCam.transform.localPosition;
    }

    public void PlayDeathScare()
    {
        if(comp!=null)comp.SetActive(false);
        contr.SetActive(false);
        NOTE.SetActive(false);
        idobj.SetActive(false);
        GetComponent<ExitNote>().OnClick();
        if (GetComponent<PlayerInteract>().current != null)
        { 
        GetComponent<PlayerInteract>().current=null;
            
        }
       

        GetComponentInChildren<PlayerLook>().enabled = false;
        GetComponentInChildren<MouseLook>().enabled = false;
        GetComponent<PlayerMovement>().sprinholdactive=false;
        if(GetComponent<PlayerMovement>().sound.isPlaying)
            GetComponent<PlayerMovement>().sound.Pause();
        GetComponent<PlayerMovement>().sprinting=false;
        GetComponent<PlayerMovement>().sprinholdactive=false;
        GetComponent<PlayerMovement>().enabled = false;
        scareSound.Play();
        StartCoroutine(JumpscareRoutine());
    }

    IEnumerator JumpscareRoutine()
    {
        float timer = 0f;  
        playerCam.transform.LookAt(enemyhead); 
        enemyFace.position =
        playerCam.transform.position +
        playerCam.transform.forward * 5f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            //enemyFace.transform.LookAt(playerCam.transform);
            // Look at enemy
            
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
        yield return new WaitForSeconds(0.1f);
        deaths++;
        if (deaths >= lives)
        {
             GetComponent<DayTransition>().StartCoroutine(
                    GetComponent<DayTransition>().DeathSequence("")
                );
                Invoke("loadDeath",5);
        }
        else
        {
            if (deaths == 1)
            {
                 GetComponent<DayTransition>().StartCoroutine(
                    GetComponent<DayTransition>().DeathSequence("SECOND NIGHT")
                );
            }
            else if (deaths == 2)
            {
                 GetComponent<DayTransition>().StartCoroutine(
                    GetComponent<DayTransition>().DeathSequence("LAST NIGHT")
                );
            }
            Invoke("LocatePlayer",3);
            
        }
        
    }
    public void loadDeath()
    {
            SceneManager.LoadScene("death");
        
    }
    public void LocatePlayer()
    {
        if(player.torchLight!=null && player.torchIcon != null)
        {
            player.torchItem.transform.SetParent(null);
            player.torchItem.SetActive(true);
            player.torchLight.SetActive(false);
            
            player.torchIcon.SetActive(false);
        }
        player.drop();
        enemy.playerCaught=false;
        enemy.patrolling=true;
        enemy.agent.isStopped = false;
        enemy.agent.ResetPath();
        enemy.playerDetected = false;
        enemy.investigatingLastPosition = false;
        enemy.playerDead=false; 
        enemy.explicitDiscover = false;
        enemy.waiting = false;
        enemy.anim.SetBool("isWalking", true);
        enemy.anim.SetBool("isRunning", false);
        enemy.hasHit=false;
        enemy.gameObject.SetActive(false);
        CharacterController cc = GetComponent<CharacterController>();

        cc.enabled = false;
        transform.position = checkpoint.position;
        transform.rotation = checkpoint.rotation;
        cc.enabled = true;
        playerCam.fieldOfView = originalFOV;
        playerCam.transform.localPosition = originalCamPos;

        // Enable controls again
        contr.SetActive(true);
        
        GetComponentInChildren<PlayerLook>().enabled = true;
        GetComponentInChildren<MouseLook>().enabled = true;
        GetComponentInChildren<PlayerInteract>().enabled = true;

        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerMovement>().sprinholdactive=false;
        GetComponent<PlayerMovement>().sprinting=false;
        Invoke("setactiveafter",20);

    }
    public void setactiveafter()
    {
        enemy.gameObject.SetActive(true);
        
    }
}
