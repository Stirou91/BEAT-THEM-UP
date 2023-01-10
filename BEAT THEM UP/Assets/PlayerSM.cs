using UnityEngine;

public class PlayerSM : MonoBehaviour
{
    [SerializeField] GameObject graphics;
    [SerializeField] PlayerState currentState;
    [SerializeField] Animator animator;
    Rigidbody2D rb2D;
    [SerializeField] float jumpHeight = 10f;
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float sprintSpeed = 5f;
    [SerializeField] AnimationCurve jumpCurve;
    [SerializeField] float jumpDuration = 2f;
    float jumpTimer;
    CapsuleCollider2D cc2D;
    float currentSpeed;
    bool attackTime;
    bool right = true;


    public enum PlayerState
    {
        IDLE,
        WALK,
        JUMP,
        ATTACK,
        SPRINT,
        DEATH

    }
    bool jumpInput;
    Vector2 dirInput;
    bool sprintInput;
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        cc2D = GetComponent<CapsuleCollider2D>();
        currentState = PlayerState.IDLE;
        OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {

        

        GetInput();
        OnStateUpdate();
    }
    private void GetInput()
    {

        dirInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        

        if (dirInput != Vector2.zero)
        {
            // ROTATION GAUCHE DROITE
            right = dirInput.x > 0;
            graphics.transform.rotation = right ? Quaternion.identity : Quaternion.Euler(0, 180, 0);

            animator.SetFloat("DirX", dirInput.x);
        }


        animator.SetFloat("DirMagnitude", dirInput.magnitude);
        sprintInput = Input.GetButton("Sprint");
        animator.SetBool("SPRINT", sprintInput && dirInput.magnitude > 0);
        animator.SetBool("WALK", dirInput.magnitude > 0);
        
        
    }


    void OnStateEnter()
    {
        switch (currentState)
        {
            case PlayerState.IDLE:
                break;
            case PlayerState.WALK:
                break;
            case PlayerState.SPRINT:
                break;
            case PlayerState.JUMP:
                animator.SetTrigger("JUMP");
                break;
            case PlayerState.ATTACK:
                animator.SetTrigger("ATTACK");
                break;
            case PlayerState.DEATH:
                break;
            default:
                break;
        }
    }

    void OnStateUpdate()
    {
        switch (currentState)
        {
            case PlayerState.IDLE:

                // STOP MOVEMENT
                rb2D.velocity = Vector2.zero;

                // TO WALK
                if (dirInput != Vector2.zero)
                {
                    TransitionToState(sprintInput ? PlayerState.SPRINT : PlayerState.WALK);
                }

                // TO ATTACK
                if (Input.GetButtonDown("Attack"))
                {
                    
                    TransitionToState(PlayerState.ATTACK);
                }

                // TO JUMP
                if (Input.GetButtonDown("Jump"))
                {
                    TransitionToState(PlayerState.JUMP);
                }

                break;
            case PlayerState.WALK:

                rb2D.velocity = dirInput.normalized * walkSpeed * 5f;

                // TO IDLE
                if (dirInput == Vector2.zero)
                {
                    TransitionToState(PlayerState.IDLE);
                }

                // TO SPRINT
                if (sprintInput)
                {
                    TransitionToState(PlayerState.SPRINT);
                }

                // TO ATTACK
                if (Input.GetButtonDown("Attack"))
                {

                    TransitionToState(PlayerState.ATTACK);
                }

                // TO JUMP
                if (Input.GetButtonDown("Jump"))
                {
                    TransitionToState(PlayerState.JUMP);
                }

                break;
            case PlayerState.SPRINT:

                rb2D.velocity = dirInput.normalized * sprintSpeed * 5f;

                // TO WALK
                if (!sprintInput && dirInput != Vector2.zero)
                {
                    TransitionToState(PlayerState.WALK);
                }

                // TO IDLE
                if (dirInput == Vector2.zero)
                {
                    TransitionToState(PlayerState.IDLE);
                }

                // TO ATTACK
                if (Input.GetButtonDown("Attack"))
                {

                    TransitionToState(PlayerState.ATTACK);
                }

                // TO JUMP
                if (Input.GetButtonDown("Jump"))
                {
                    TransitionToState(PlayerState.JUMP);
                }

                break;
            case PlayerState.JUMP:

                rb2D.velocity = dirInput.normalized * walkSpeed * 5f;

                if (jumpTimer < jumpDuration)
                {
                    jumpTimer += Time.deltaTime;

                    float y = jumpCurve.Evaluate(jumpTimer / jumpDuration);

                    graphics.transform.localPosition = new Vector3(0 , y * jumpHeight, 0);
                }
                else
                {
                    jumpTimer = 0f;
                    TransitionToState(PlayerState.IDLE);
                }

                break;
            case PlayerState.ATTACK:
                break;
            case PlayerState.DEATH:
            default:
                break;
        }
    }

    void OnStateExit()
    {
        switch (currentState)
        {
            case PlayerState.IDLE:
                break;
            case PlayerState.WALK:
                break;
            case PlayerState.SPRINT:
                break;
            case PlayerState.JUMP:
                break;
            case PlayerState.ATTACK:
                break;
            case PlayerState.DEATH:
            default:
                break;
        }
    }

    void TransitionToState(PlayerState nextState)
    {
        OnStateExit();
        currentState = nextState;
        OnStateEnter();
    }



}






