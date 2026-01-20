
    using UnityEngine;
    using UnityEngine.AI;

    public class Enemy : MonoBehaviour
    {
        [Header("Patrol")]
        public Transform[] patrolPoints;
        private int currentPoint;
        private float waitCounter;
        public float runSpeed=10;
        public float walkSpeed=10;

        [Header("Detection")]
        public float detectionRange = 50f;
        public float caughtRange = 5f;
        private NavMeshAgent agent;
        public Transform player;
        private bool playerDetected = false;
        private bool playerCaught = false;
        public Vector3 lastKnownPlayerPosition;

        [Header("Investigation")]
        public float investigateStoppingDistance = 1.5f;
        public bool investigatingLastPosition = false;
        private float waitTime = 5f;
        [Header("References")]
        public PlayerMovement playerMovement;
        public DeathScare death;
        public Transform eyes; // empty object at head
        public AudioSource audioSource;
        public AudioSource audioSourcegun;
        public float normalPitch;
        public float fastPitch;
        public Animator anim;
        public bool isHostile=true;
        bool patrolling=false;
        public bool isDead=false;
        public int hits=0;
        public float RespawnTime=10f;
        public bool security=true;
        public bool explicitDiscover=false;
        public Vector3 pos;
        public float timer;
        void Start()
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", true);
            anim.SetBool("isLying", false);
            if (GameSettings.Instance != null)
                isHostile= !GameSettings.Instance.debugMode;
            agent = GetComponent<NavMeshAgent>();
            patrolling=true;

            

            if (patrolPoints.Length > 0)
                agent.SetDestination(patrolPoints[0].position);
        }

        void Update()
        {
            if (!security)
            {
                if(audioSource.isPlaying)audioSource.Stop();
                agent.isStopped=true;
                anim.SetBool("isRunning", false);
                anim.SetBool("isWalking", false);
                anim.SetBool("isLying", false);
                return;
            }
            if (isDead)
            {
                return;
            }
            UpdateAnimations();
            HandleAudio();
            if (explicitDiscover)
            {
                anim.SetBool("isRunning", true);
                anim.SetBool("isWalking", false);
                anim.SetBool("isLying", false);
                ChasePoint(pos);
                return;
            }
            if(!isHostile)
            {
                patrolling=true;
                playerCaught=false;
                playerDetected=false;
                investigatingLastPosition=false;
                Patrol();   
                return;
            }
            DetectPlayer();
            if (playerCaught)
            {
                FacePlayer();
                audioSource.Stop();
                agent.isStopped = true;
                return;
            }
            if (playerDetected)
                ChasePlayer();
            else if (investigatingLastPosition)
                InvestigateLastPosition();
            else if(patrolling)
                Patrol();
        }
        public void Damage()
        {
            hits++;
            if (hits >= 3)
            {
                agent.isStopped=true;
                anim.SetBool("isLying", true);
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                playerDetected=false;
                playerCaught=false;
                investigatingLastPosition=false;
                patrolling=false;
                isDead=true;
                audioSource.Stop();
                Invoke("respawn",RespawnTime);
                hits=0;
            }
        }
        void respawn()
        {
            isDead=false;
            agent.isStopped=false;
            anim.SetBool("isLying", false);
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);
            playerDetected=false;
            playerCaught=false;
            investigatingLastPosition=false;
            patrolling=true;
            isDead=false;
            audioSource.Play();

        }
        void UpdateAnimations()
        {
            if (explicitDiscover)
            {
                anim.SetBool("isRunning", true);
                anim.SetBool("isWalking", false);
                anim.SetBool("isLying", false);
            }
            if (playerCaught)
            {
                anim.SetTrigger("hit");

                return;
            }
            if (playerDetected) // Chase
            {
                anim.SetBool("isRunning", true);
                anim.SetBool("isWalking", false);
                anim.SetBool("isLying", false);

            }
            else if (investigatingLastPosition) // Patrol/Investigate
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isWalking", true);
            }
            else if (patrolling)
            
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isWalking", false);
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

        void InvestigateLastPosition()
        {
            if (!agent.pathPending && agent.remainingDistance <= investigateStoppingDistance)
                investigatingLastPosition = false;
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

        void DetectPlayer()
        {
            if (playerCaught) return;

            float distance = Vector3.Distance(transform.position, player.position);

            if (distance < caughtRange && !playerCaught)
            {
                    playerCaught = true;
                    agent.isStopped = true;
                    agent.ResetPath();
                    anim.SetTrigger("hit");
                    death.PlayDeathScare();
                    return;
            }
            if (distance < detectionRange + 30 && (playerMovement.sprinholdactive || playerMovement.sprinting))
            {
                playerDetected=true;
                return;
            }
            if (distance > detectionRange )
            {
                playerDetected = false;
                patrolling=true;
                return;
            }

            Vector3 origin = eyes.position;
            Vector3 targetPoint = player.position + Vector3.up * 1.2f;
            Vector3 direction = (targetPoint - origin).normalized;

            Debug.DrawRay(origin, direction * detectionRange, Color.red);

            if (Physics.Raycast(origin, direction, out RaycastHit hit, detectionRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    lastKnownPlayerPosition = player.position;
                    playerDetected = true;
                    explicitDiscover=false;
                }
                else
                {
                        LosePlayer();
                }
            }
            else
            {
                LosePlayer();
            }
        }

        private float afterChaseWait=0f;
        void ChasePoint(Vector3 position)
        {
            audioSource.pitch=(fastPitch);

            agent.speed = runSpeed;
            FacePlayer();
            agent.isStopped = false;
            agent.SetDestination(position);
            bool hasPath = agent.CalculatePath(player.position, new NavMeshPath());
            if (agent.remainingDistance < 10)
            {
                explicitDiscover=false;
            }
            if (!hasPath)
            {
                afterChaseWait+=Time.deltaTime;
                if (afterChaseWait > 5)
                {
                    playerDetected=false;
                    afterChaseWait=0;
                }
            }
        }
        void ChasePlayer()
        {
            audioSource.pitch=(fastPitch);

            agent.speed = runSpeed;
            FacePlayer();
            agent.isStopped = false;
            agent.SetDestination(player.position);
            NavMeshPath path = new NavMeshPath();
            bool hasPath = agent.CalculatePath(player.position, path);
            if (!hasPath)
            {
                afterChaseWait+=Time.deltaTime;
                if (afterChaseWait > 5)
                {
                    playerDetected=false;
                    afterChaseWait=0;
                }
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
            audioSource.pitch=(normalPitch);
            agent.speed=walkSpeed;
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
                //patrolling=false;
                if (waitCounter >= waitTime)
                {
                    
                    currentPoint = (currentPoint + 1) % patrolPoints.Length;
                    waitCounter = 0f;
                     //patrolling=true;

                }
            }
        }
    }
