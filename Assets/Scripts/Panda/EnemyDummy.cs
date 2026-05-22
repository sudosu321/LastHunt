using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDummy : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] patrolPoints;
    public int currentPoint;
    private float waitCounter;
    public float walkSpeed = 10f;
    public float waitTime = 5f;
    private bool waiting = false;

    [Header("References")]
    public NavMeshAgent agent;
    public Animator anim;
    public AudioSource audioSource;
    public float normalPitch;
    public ParticleSystem system;

    [Header("State")]
    public bool security = true;
    public bool isDead = false;
    public float RespawnTime = 10f;

    void Start()
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isWalking", true);
        anim.SetBool("isLying", false);

        agent = GetComponent<NavMeshAgent>();

        if (patrolPoints.Length > 0)
            agent.SetDestination(patrolPoints[0].position);
    }

    void Update()
    {
        if (!security)
        {
            if (audioSource.isPlaying) audioSource.Stop();
            agent.isStopped = true;
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isLying", false);
            return;
        }

        if (isDead) return;

        HandleAudio();
        Patrol();
    }

    public void Damage()
    {
        if (!security || isDead) return;

        system.Play();
        agent.isStopped = true;
        anim.SetBool("isLying", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
        isDead = true;
        audioSource.Stop();
        Invoke("Respawn", RespawnTime);
    }

    void Respawn()
    {
        isDead = false;
        agent.isStopped = false;
        anim.SetBool("isLying", false);
        anim.SetBool("isWalking", true);
        anim.SetBool("isRunning", false);
        audioSource.Play();
    }

    void HandleAudio()
    {
        bool isMoving = agent.velocity.magnitude > 0.1f && !agent.isStopped;
        if (isMoving && !audioSource.isPlaying)
            audioSource.Play();
        else if (!isMoving && audioSource.isPlaying)
            audioSource.Stop();
    }

    void Patrol()
    {
        audioSource.pitch = normalPitch;
        agent.speed = walkSpeed;
        agent.isStopped = false;

        if (patrolPoints.Length == 0) return;

        int attempts = 0;
        while (attempts < patrolPoints.Length)
        {
            NavMeshPath path = new NavMeshPath();
            bool hasPath = agent.CalculatePath(patrolPoints[currentPoint].position, path);

            if (hasPath && path.status == NavMeshPathStatus.PathComplete)
            {
                agent.SetDestination(patrolPoints[currentPoint].position);
                break;
            }
            else
            {
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
                attempts++;
            }
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            waiting = true;
            anim.SetBool("isWalking", false);
            waitCounter += Time.deltaTime;

            if (waitCounter >= waitTime)
            {
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
                waitCounter = 0f;
                waiting = false;
                anim.SetBool("isWalking", true);
            }
        }
    }
}