using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] patrolPoints;
    public int currentPoint;
    private float waitCounter;
    public float runSpeed = 10;
    public float walkSpeed = 10;
    [Header("Detection")]
    public float detectionRange = 50f;
    public float caughtRange = 5f;
    public NavMeshAgent agent;
    public Transform player;
    public bool playerDetected = false;
    public bool playerCaught = false;
    public Vector3 lastKnownPlayerPosition;

    [Header("Investigation")]
    public float investigateStoppingDistance = 1.5f;
    public bool investigatingLastPosition = false;
    public float waitTime = 5f;

    [Header("References")]
    public PlayerMovement playerMovement;
    public DeathScare death;
    public Transform eyes;
    public AudioSource audioSource;
    public AudioSource audioSourcegun;
    public float normalPitch;
    public float fastPitch;
    public Animator anim;
    public bool isHostile = true;
    public bool patrolling = false;
    public bool isDead = false;
    public int hits = 0;
    public float RespawnTime = 10f;
    public bool security = true;
    public bool explicitDiscover = false;
    public Vector3 pos;
    public float timer;
    public bool waiting = false;
    public int deaths = 0;

    public bool hasHit = false;
    public bool playerDead = false;
    public NavMeshSurface navMeshSurface;
    public ParticleSystem system;
    public float easyDetectionRange;
    public float extrDetectionRange;
    public float easySprintFind = 20;
    public float extremeSprintFind = 40;
    public bool sprintFind = true;
    public float gotocheckpointtime = 8f;
    public Transform checkpointEnemy;
    public bool goBackDone = false;
    public bool DUMMY=false;
    public AudioSource playerDet;
    public AudioSource robotDed;
    public float extraSprintFind = 20;

    [Header("Informer Mode")]
    public AudioSource HOWL;
    public bool isInformer = false;
    public Enemy primaryEnemy;
    private bool isInforming = false;
    private float informCooldown = 5f;
    private float informTimer = 0f;
    public float informerStopDistance = 10f;

    // ─────────────────────────────────────────────
    //  PRIVATE STATE
    // ─────────────────────────────────────────────
    private float waitCounter2 = 0f;
    private float waiter = 0f;
    private float afterChaseWait = 0f;
    private float playTimer = 3f;
    private float countTimer = 3f;

    void Start()
    {
        system.Play();
        path = new NavMeshPath();
        anim.SetBool("isRunning", false);
        anim.SetBool("isWalking", true);
        anim.SetBool("isLying", false);
        int val=0;
        if (GameSettings.Instance != null)
        {
            val = GameSettings.Instance.difficulty;
            sprintFind = GameSettings.Instance.sprintDetect;

            if (val == 0)
            {
                isHostile = false;
                if (!isHostile)
                {
                    detectionRange = 0;
                    extraSprintFind = 0;
                    caughtRange = 0;
                }
            }
            else if (val == 1)
            {
                isHostile=true;

                detectionRange = easyDetectionRange;
                if (!GameSettings.Instance.sprintDetect)
                    extraSprintFind = 0;
            }
            else if (val == 2)
            {
                isHostile=true;
                detectionRange = extrDetectionRange;
                runSpeed = 15;
                sprintFind = true;
                extraSprintFind = extremeSprintFind;
            }
        }
        if (DUMMY)
        {
            detectionRange = 0;
            extraSprintFind = 0;
            caughtRange = 0;
        }
        if (isInformer)
        {
            if (GameSettings.Instance != null && val == 0)
            {
                 detectionRange = 0;
                extraSprintFind = 0;
                caughtRange = 0;
            }
            else
            {
                detectionRange = extrDetectionRange;
                extraSprintFind = 0;
                sprintFind = false;
            }
            
        }
        
        agent = GetComponent<NavMeshAgent>();
        patrolling = true;
        if (patrolPoints.Length > 0);
            //agent.SetDestination(patrolPoints[0].position);
    }
    private float timerPathCalc;
    void calcPath()
    {
        path=new NavMeshPath();
    }
    void Update()
    {
        timerPathCalc+=Time.deltaTime;
        if (timerPathCalc > 0.5f)
        {
            calcPath();
            timerPathCalc=0;
        }
        if (!security)
        {
            if (audioSource.isPlaying) audioSource.Stop();
            agent.isStopped = true;
            patrolling = false;
            playerDetected = false;
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isLying", false);
            return;
        }

        if (isDead) return;

        if (isInformer)
        {
            //UpdateAnimations();
            HandleAudio();
            HandleInformer();
            return; 
        }

        if (!isHostile)
        {
            patrolling = true;
            playerCaught = false;
            playerDetected = false;
            investigatingLastPosition = false;
            explicitDiscover = false;
            Patrol();
            UpdateAnimations();
            HandleAudio();
            return;
        }

        UpdateAnimations();
        HandleAudio();

        if (explicitDiscover)
        {
            patrolling = false;
            playerDetected = false;
            anim.SetBool("isRunning", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isLying", false);
            ChasePoint(pos);
            return;
        }

        DetectPlayer();

        if (playerCaught)
        {
            if (!goBackDone)
            {
                deaths++;
                goBackDone = true;
                Invoke("goBack", gotocheckpointtime);
            }
            FacePlayer();
            audioSource.Stop();
            agent.isStopped = true;
            return;
        }

        if (playerDetected)
            ChasePlayer();
        else if (investigatingLastPosition)
            InvestigateLastPosition();
        else if (patrolling)
            Patrol();
    }
    
    void HandleInformer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        informTimer += Time.deltaTime;
        LayerMask detectionMask = ~LayerMask.GetMask("Interactable");
        Vector3 origin = eyes.position;
        Vector3 targetPoint = player.position + Vector3.up * 1.2f;
        Vector3 direction = (targetPoint - origin).normalized;
        Debug.DrawRay(origin, direction * detectionRange, Color.red);
        if (Physics.Raycast(origin, direction, out RaycastHit hit, detectionRange, detectionMask))
        {
            if (distance > informerStopDistance && hit.collider.CompareTag("Player"))
            {
                patrolling = false;
                agent.speed = runSpeed;
                if(!audioSource.isPlaying)
                audioSource.pitch = getPitch(fastPitch);
                agent.isStopped = false;
                if (Vector3.Distance(agent.destination, player.position) > 1.5f)
                    agent.SetDestination(player.position);

                anim.SetBool("isRunning", true);
                anim.SetBool("isWalking", false);
            }
            else if(distance <= informerStopDistance && hit.collider.CompareTag("Player"))
            {
                agent.isStopped = true;
                FacePlayer();
                if(HOWL.isPlaying==false)
                {
                     HOWL.pitch = Random.Range(0f, 1.0f);
                    HOWL.Play();
                }
                anim.SetBool("isRunning", false);
                anim.SetBool("isWalking", false);
                if (informTimer >= informCooldown && primaryEnemy != null)
                {
                    primaryEnemy.pos = player.position;
                    primaryEnemy.explicitDiscover = true;
                    informTimer = 0f;
                }
            }
            else
            {
                if(!audioSource.isPlaying)
                audioSource.pitch = getPitch(normalPitch);

                agent.isStopped = false;
                patrolling = true;
                anim.SetBool("isRunning", false);
                anim.SetBool("isWalking", true);
                Patrol();
            }
        }
        else
        {
              if(!audioSource.isPlaying)
                audioSource.pitch = getPitch(normalPitch);
            agent.isStopped = false;
            patrolling = true;
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", true);
            Patrol();
        }
    }
    public void goBack()
    {
        transform.SetPositionAndRotation(checkpointEnemy.position, checkpointEnemy.rotation);
        Invoke("turnTrue", 10);
    }
    void turnTrue()
    {
        goBackDone = false;
    }
    public void Damage()
    {
        if (security == false) return;
        hits++;
        if (hits > 0)
        {
            robotDed.Play();
            system.Play();
            agent.isStopped = true;
            anim.SetBool("isLying", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
            playerDetected = false;
            playerCaught = false;
            investigatingLastPosition = false;
            patrolling = false;
            isDead = true;
            audioSource.Stop();
            Invoke("respawn", RespawnTime);
            hits = 0;
        }
    }
    void respawn()
    {
        isDead = false;
        agent.isStopped = false;
        anim.SetBool("isLying", false);
        anim.SetBool("isWalking", true);
        anim.SetBool("isRunning", false);
        playerDetected = false;
        playerCaught = false;
        investigatingLastPosition = false;
        patrolling = true;
    }
    void UpdateAnimations()
    {
        if (waiting)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isLying", false);
        }
        if (explicitDiscover)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isLying", false);
        }
        if (playerCaught && !hasHit)
        {
            playerDead = true;
            anim.SetTrigger("hit");
            hasHit = true;
        }
        if (playerDetected)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isLying", false);
        }
        else if (investigatingLastPosition)
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
        else if (patrolling && waiting)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isLying", false);
        }
    }
    void HandleAudio()
    {
        if (playerDetected)
        {
            countTimer += Time.deltaTime;
            if (countTimer > playTimer)
            {
                if (!playerDet.isPlaying)
                {
                    playerDet.pitch = Random.Range(0.5f, 1.5f);
                    playerDet.Play();
                    countTimer = 0;
                }
            }
        }
    }

    void InvestigateLastPosition()
    {
        if (!agent.pathPending && agent.remainingDistance <= investigateStoppingDistance)
        {
            agent.isStopped = true;
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);

            waiter += Time.deltaTime;
            if (waiter > waitTime)
            {
                agent.isStopped = false;
                investigatingLastPosition = false;
                patrolling = true;
                waiting = false;
                waitCounter = 0f;
                waiter = 0f;
            }
        }
    }
    void LosePlayer()
    {
        if (playerDetected)
        {
            investigatingLastPosition = true;
            agent.isStopped = false;
            agent.SetDestination(lastKnownPlayerPosition);
        }
        playerDetected = false;
    }
    private float timerDetect=0;
    void DetectPlayer()
    {
        timerDetect+=Time.deltaTime;
        if(timerDetect>0.3f==false)return;
        timerDetect=0;
        if (playerCaught) return;

        float distance = Vector3.Distance(transform.position, player.position);
         Vector3 origin = eyes.position;
        Vector3 targetPoint = player.position + Vector3.up * 1.2f;
        Vector3 direction = (targetPoint - origin).normalized;
        Debug.DrawRay(origin, direction * detectionRange, Color.red);
        LayerMask detectionMask = ~LayerMask.GetMask("Interactable");
        if (distance < caughtRange && !playerCaught)
        {
             if (Physics.Raycast(origin, direction, out RaycastHit hit1, 10, detectionMask))
            {
                if (hit1.collider.CompareTag("Player"))
                {
                    if (!playerDead)
                    {
                        playerCaught = true;
                        playerDead = true;
                        agent.isStopped = true;
                        player.GetComponent<PlayerHold>().dropItem();
                        agent.ResetPath();
                        anim.SetTrigger("hit");
                        death.PlayDeathScare();
                        return;
                    }
                }
                else{
                    return;
                }
            }
        }

        if (distance < detectionRange + extraSprintFind &&
            (playerMovement.sprinholdactive || playerMovement.sprinting) && sprintFind)
        {
            lastKnownPlayerPosition = player.position;
            playerDetected = true;
            patrolling = false;
            waiting = false;
            waitCounter = 0f;
            return;
        }

        if (distance > detectionRange && !playerDetected)
        {
            playerDetected = false;
            patrolling = true;
            return;
        }

       

        if (Physics.Raycast(origin, direction, out RaycastHit hit, detectionRange, detectionMask))
        {
            if (hit.collider.CompareTag("Player"))
            {
                lastKnownPlayerPosition = player.position;
                playerDetected = true;
                patrolling = false;
                waiting = false;
                waitCounter = 0f;
                explicitDiscover = false;
            }
            else
            {
                LosePlayer();
            }
        }
    }
    float getPitch(float ptch)
    {
        return Random.Range(ptch-0.2f,ptch+0.2f);
    }
    void ChasePoint(Vector3 position)
    {
       if(!audioSource.isPlaying)
                audioSource.pitch = getPitch(fastPitch);
        patrolling = false;
        agent.speed = runSpeed;
        agent.isStopped = false;
        agent.SetDestination(position);

       
         //path = new NavMeshPath();
        timerpath+=Time.deltaTime;
        bool hasPath=false;
        hasPath=cachedPath;
        if (timerpath > 0.5f)
        {
             cachedPath = agent.CalculatePath(player.position, path);
             hasPath=cachedPath;
             timerpath=0;
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 5f)
        {
            explicitDiscover = false;
            patrolling = true;
            waiting = false;
            waitCounter = 0f;
            return;
        }

        if (!hasPath || path.status == NavMeshPathStatus.PathPartial || path.status == NavMeshPathStatus.PathInvalid)
        {
            afterChaseWait += Time.deltaTime;
            if (afterChaseWait > waitTime)
            {
                explicitDiscover = false;
                patrolling = true;
                playerDetected = false;
                afterChaseWait = 0;
                waiting = false;
                waitCounter = 0f;
            }
        }
        else
        {
            afterChaseWait = 0;
        }
    }
    private NavMeshPath path;
    private float timerpath;
    bool cachedPath=true;
    void ChasePlayer()
    {
     if(!audioSource.isPlaying)
                audioSource.pitch = getPitch(fastPitch);
        agent.speed = runSpeed;
        agent.isStopped = false;

        if (Vector3.Distance(agent.destination, player.position) > 1.5f)
            agent.SetDestination(player.position);
        
       // path = new NavMeshPath();
        timerpath+=Time.deltaTime;
        bool hasPath=false;
        hasPath=cachedPath;
        if (timerpath > 0.5f)
        {
             cachedPath = agent.CalculatePath(player.position, path);
             hasPath=cachedPath;
             timerpath=0;
        }
        Debug.Log("cache" +cachedPath);
        Debug.Log("remain dist "+agent.remainingDistance);
        Debug.Log("pathstst "+agent.remainingDistance);

        
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 5f)
            explicitDiscover = false;

        if (!hasPath || path.status == NavMeshPathStatus.PathPartial || path.status == NavMeshPathStatus.PathInvalid)
        {
            afterChaseWait += Time.deltaTime;
            if (afterChaseWait > waitTime)
            {
                playerDetected = false;
                patrolling = true;
                agent.ResetPath();
                afterChaseWait = 0;
                waiting = false;
                waitCounter = 0f;
            }
        }
        else
        {
            afterChaseWait = 0;
        }
    }
    void FacePlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);
    }
    void Patrol()
    {
     if(!audioSource.isPlaying)
                audioSource.pitch = getPitch(normalPitch);
        agent.speed = walkSpeed;
        agent.isStopped = false;
        if (patrolPoints.Length == 0) return;
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance+2f)
        {
            waitCounter += Time.deltaTime;
            waiting = true;
            if (waitCounter >= waitTime)
            {
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
                waitCounter = 0f;
                waiting = false;
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
            }
        }
    }
}