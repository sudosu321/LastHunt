
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCatcher : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] patrolPoints;
    public int currentPoint;
    private float waitCounter;
    public float runSpeed = 10;
    public float walkSpeed = 10;

    [Header("Detection")]
    public float detectionRange = 80f;
    public float caughtRange = 10f;
    public NavMeshAgent agent;
    public Transform enemy;
    public GameObject e;
    
    public bool enemyDetect = false;
    public bool enemyCaught = false;

    public float waitTime = 5f;
    [Header("References")]
    public Transform eyes; // empty object at head
    public AudioSource audioSource;
    public float normalPitch;
    public float fastPitch;
    public Animator anim;
    public bool isHostile = true;
    public bool patrolling = false;
    public bool explicitDiscover = false;
    public bool waiting = false;

    public bool eneemyDeads=false;
    public NavMeshSurface navMeshSurface;
    public ParticleSystem system;
    void Start()
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isWalking", true);
        anim.SetBool("isLying", false);
        agent = GetComponent<NavMeshAgent>();
        patrolling = true;
        if (patrolPoints.Length > 0)
            agent.SetDestination(patrolPoints[0].position);
    }
    public float extraSprintFind=20;
    void Update()
    {
        UpdateAnimations();
        HandleAudio();
        DetectPlayer();
        if(eneemyDeads){
            patrolling=true;
            enemyDetect=false;
            Patrol();return;
        }
        if (enemyCaught)
        {
            FacePlayer();
            audioSource.Stop();
            agent.isStopped = true;
            return;
        }
        if (enemyDetect)
            ChasePlayer();
        else if (patrolling)
            Patrol();
    }
    public bool hasHit=false;
    void UpdateAnimations()
    {
        if (waiting)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isLying", false);
        }
        if (enemyCaught && !hasHit)
        {
            eneemyDeads=true;
            anim.SetTrigger("hit");
            hasHit = true;
        }
        if (enemyDetect) // Chase
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isLying", false);
        }
        else if (patrolling && !waiting)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", true);
            anim.SetBool("isLying", false);
        }
        else if(patrolling && waiting)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isLying", false);
            
        }
    }

    void HandleAudio()
    {
        bool isMoving = agent.velocity.magnitude > 0.1f && !agent.isStopped;
        if (isMoving && !audioSource.isPlaying)
            audioSource.Play();
        else if (!isMoving && audioSource.isPlaying)
            audioSource.Stop();
    }

    private float waiter=0f;
    void DetectPlayer()
    {
        if (enemyCaught) return;

        float distance = Vector3.Distance(transform.position, enemy.position);

        if (distance < caughtRange && !enemyCaught)
        {
            if (eneemyDeads== false)
            {
                eneemyDeads=true;
                e.SetActive(false);
                system.Play();
                agent.ResetPath();
                anim.SetTrigger("hit");
                return;
            }
            
        }
        if (distance > detectionRange && !enemyDetect)
        {
            enemyDetect = false;
            patrolling = true;
            return;
        }

        Vector3 origin = eyes.position;
        Vector3 targetPoint = enemy.position + Vector3.up * 1.2f;
        Vector3 direction = (targetPoint - origin).normalized;
        Debug.DrawRay(origin, direction * detectionRange, Color.red);

        if (Physics.Raycast(origin, direction, out RaycastHit hit, detectionRange))
        {
            if (hit.collider.transform.name.Contains("ENEMY"))
            {
                enemyDetect = true;
                patrolling=false;
                explicitDiscover = false;
            }
            else
            {
            }
        }
        else
        {
            //LosePlayer();
        }
    }

    private float afterChaseWait = 0;
    void ChasePoint(Vector3 position)
    {
        audioSource.pitch = (fastPitch);
        patrolling=false;
                
        agent.speed = runSpeed;
        FacePlayer();
        agent.isStopped = false;
        agent.SetDestination(position);
        NavMeshPath path = new NavMeshPath();
        bool hasPath = agent.CalculatePath(position,path);
        if (agent.remainingDistance < 8)
        {
            explicitDiscover = false;
        }

        if (!hasPath || path.status==NavMeshPathStatus.PathPartial ||path.status==NavMeshPathStatus.PathInvalid)
        {
            afterChaseWait += Time.deltaTime;
            if (afterChaseWait > waitTime)
            {
                patrolling=true;
                enemyDetect = false;
                afterChaseWait = 0;
            }
        }
        else
        {
            afterChaseWait = 0;
        }
    }
    void ChasePlayer()
    {
        audioSource.pitch = (fastPitch);

        agent.speed = runSpeed;
        FacePlayer();
        agent.isStopped = false;
        agent.SetDestination(enemy.position);
        NavMeshPath path = new NavMeshPath();
        bool hasPath = agent.CalculatePath(enemy.position, path);
        if (agent.remainingDistance < 8)
        {
            explicitDiscover = false;
        }
        if (!hasPath || path.status==NavMeshPathStatus.PathPartial||path.status==NavMeshPathStatus.PathInvalid)
        {
            afterChaseWait += Time.deltaTime;
            if (afterChaseWait > waitTime)
            {
                enemyDetect = false;
                patrolling=true;
                agent.ResetPath();
                afterChaseWait = 0;
            }
        }
        else
        {
            // IMPORTANT: reset timer if path is valid again
            afterChaseWait = 0;
        }
    }

    void FacePlayer()
    {
        Vector3 dir = enemy.position - transform.position;
        dir.y = 0;
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);
    }

    void Patrol()
    {
        audioSource.pitch = (normalPitch);
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
            waitCounter += Time.deltaTime;
            waiting=true;
            if (waitCounter >= waitTime)
            {

                currentPoint = (currentPoint + 1) % patrolPoints.Length;
                waitCounter = 0f;
                waiting=false;
            }
        }
    }
}
