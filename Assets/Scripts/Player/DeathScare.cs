using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using GoogleMobileAds.Api;

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
    public GameObject exrNight;

    public GameObject gun;
    public Transform gunPos;

    private float originalFOV;
    private Vector3 originalCamPos;

    public int lives = 3;
    public int deaths = 0;

    public Transform checkpoint;

    // ADMOB
    private string adUnitId = "ca-app-pub-3940256099942544/5224354917"; // test ID, replace with ca-app-pub-8271564870750045/1802206839 for release
    private RewardedAd rewardedAd;

    void Start()
    {
        originalFOV = playerCam.fieldOfView;
        originalCamPos = playerCam.transform.localPosition;

        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("Ads Initialized");
            LoadAd();
        });
    }

    void LoadAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        var adRequest = new AdRequest();

        RewardedAd.Load(adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.Log("LOAD FAILED: " + error);
                return;
            }

            Debug.Log("Reward Ad Loaded");
            rewardedAd = ad;

            rewardedAd.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("AD CLOSED - Reloading...");
                LoadAd();
            };

            rewardedAd.OnAdFullScreenContentFailed += (AdError adError) =>
            {
                Debug.Log("AD FAILED: " + adError);
                LoadAd();
            };
        });
    }

    public void PlayDeathScare()
    {
        if (comp != null)
            comp.SetActive(false);

        contr.SetActive(false);
        NOTE.SetActive(false);
        idobj.SetActive(false);

        GetComponent<ExitNote>().OnClick();

        if (GetComponent<PlayerInteract>().current != null)
        {
            GetComponent<PlayerInteract>().current = null;
        }

        GetComponentInChildren<PlayerLook>().enabled = false;
        GetComponentInChildren<MouseLook>().enabled = false;

        GetComponent<PlayerMovement>().sprinholdactive = false;

        if (GetComponent<PlayerMovement>().sound.isPlaying)
            GetComponent<PlayerMovement>().sound.Pause();

        GetComponent<PlayerMovement>().sprinting = false;
        GetComponent<PlayerMovement>().sprinholdactive = false;
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

            if (timer > duration / 2)
            {
                if (stomp != null)
                {
                    if (stomp.isPlaying == false)
                        stomp.Play();
                }
            }

            playerCam.fieldOfView = Mathf.Lerp(
                playerCam.fieldOfView,
                zoomFOV,
                Time.deltaTime * zoomSpeed
            );

            playerCam.transform.localPosition =
                originalCamPos +
                Random.insideUnitSphere * shakeIntensity;

            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        deaths++;

        if (deaths >= lives)
        {
            GetComponent<DayTransition>().StartCoroutine(
                    GetComponent<DayTransition>()
                    .FadeToBlack()
                );
            Invoke("askForLife",3);
            
        }
        else
        {
            if (deaths == 1)
            {
                GetComponent<DayTransition>().StartCoroutine(
                    GetComponent<DayTransition>()
                    .DeathSequence("SECOND NIGHT")
                );
            }
            else if (deaths == 2)
            {
                GetComponent<DayTransition>().StartCoroutine(
                    GetComponent<DayTransition>()
                    .DeathSequence("LAST NIGHT")
                );
            }

            Invoke("LocatePlayer", 3);
        }
    }
    public void askForLife()
    {
        enemy.gameObject.SetActive(false);
        exrNight.SetActive(true);
    }
    // BUTTON CALL
    public void watchAd()
    {
        exrNight.SetActive(false);

        WatchAdForLife();
    }

    public void WatchAdForLife()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            exrNight.SetActive(false);
            rewardedAd.Show((Reward reward) =>
            {
                Debug.Log("EXTRA NIGHT GIVEN");
                deaths = 2;
                GetComponent<DayTransition>().StartCoroutine(
                    GetComponent<DayTransition>()
                    .DeathSequence("LAST NIGHT")
                );
                Invoke("LocatePlayer", 3);
                LoadAd();
            });
        }
        else
        {
            exrNight.SetActive(false);
            Debug.Log("AD NOT READY - Reloading...");
            LoadAd();
        }
    }

    public void dontWatchAd()
    {
        exrNight.SetActive(false);

        GetComponent<DayTransition>().StartCoroutine(
            GetComponent<DayTransition>()
            .DeathSequence("")
        );

        Invoke("loadDeath", 3);
    }

    public void loadDeath()
    {
        SceneManager.LoadScene("death");
    }

    public void LocatePlayer()
    {
        if (player.torchLight != null &&
            player.torchIcon != null)
        {
            player.torchItem.transform.SetParent(null);

            player.torchItem.SetActive(true);

            player.torchLight.SetActive(false);

            player.torchIcon.SetActive(false);
        }

        player.drop();

        enemy.gameObject.SetActive(false);

        CharacterController cc =
            GetComponent<CharacterController>();

        cc.enabled = false;

        transform.position = checkpoint.position;
        transform.rotation = checkpoint.rotation;

        cc.enabled = true;

        playerCam.fieldOfView = originalFOV;

        playerCam.transform.localPosition =
            originalCamPos;

        exrNight.SetActive(false);

        contr.SetActive(true);

        GetComponentInChildren<PlayerLook>().enabled = true;
        GetComponentInChildren<MouseLook>().enabled = true;
        GetComponentInChildren<PlayerInteract>().enabled = true;

        GetComponent<PlayerMovement>().enabled = true;

        GetComponent<PlayerMovement>().sprinholdactive = false;
        GetComponent<PlayerMovement>().sprinting = false;

        Invoke("setactiveafter", 15);
    }

    public void setactiveafter()
    {
        enemy.playerCaught = false;
        enemy.patrolling = true;

        
        enemy.playerDetected = false;
        enemy.investigatingLastPosition = false;
        enemy.playerDead = false;
        enemy.explicitDiscover = false;
        enemy.waiting = false;

        enemy.anim.SetBool("isWalking", true);
        enemy.anim.SetBool("isRunning", false);

        enemy.hasHit = false;
        enemy.gameObject.SetActive(true);
        enemy.agent.isStopped = false;
        enemy.agent.ResetPath();

    }
}