using UnityEngine;

public class PlayerSM : MonoBehaviour
{
    [SerializeField] GameObject graphics;
    [SerializeField] GameObject dash;
    [SerializeField] GameObject jump;
    [SerializeField] GameObject shock;
    [SerializeField] GameObject generalOne;
    [SerializeField] GameObject generalTwo;
    [SerializeField] GameObject hit;
    [SerializeField] GameObject smoke;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject dustLand;
    [SerializeField] GameObject groundPound;
    [SerializeField] GameObject EnemyHealth;
    [SerializeField] PlayerState currentState;
    [SerializeField] Animator animator;
    [SerializeField] RuntimeAnimatorController animatorTwo;
    Rigidbody2D rb2D;
    [SerializeField] float jumpHeight = 10f;
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float sprintSpeed = 5f;
    [SerializeField] AnimationCurve jumpCurve;
    [SerializeField] float jumpDuration = 2f;
    float jumpTimer;
    CapsuleCollider2D cc2D;
    float currentSpeed;
    float attackTime = 3f;
    bool right = true;
    

    public enum PlayerState
    {
        IDLE,
        WALK,
        JUMP,
        ATTACK,
        SPRINT,
        CAN,
        DEATH

    }
    bool jumpInput;
    Vector2 dirInput;
    bool sprintInput;
    bool canInput;

    bool peuxRamasserCannette;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CanUP")
        {
            peuxRamasserCannette = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CanUP")
        {
            peuxRamasserCannette = false;
        }
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


        if (dirInput.x != 0)
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
        canInput = Input.GetButtonDown("Can");
        

        if(peuxRamasserCannette && canInput) 
        {
            TransitionToState(PlayerState.CAN);
        }

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
                dash.gameObject.SetActive(true);
                break;
            case PlayerState.JUMP:
                animator.SetTrigger("JUMP");
                jump.gameObject.SetActive(true);
                break;
            case PlayerState.CAN:
                rb2D.velocity = Vector2.zero;
                animator.SetTrigger("CAN");
                break;
            case PlayerState.ATTACK:
                rb2D.velocity = Vector2.zero;
                animator.SetTrigger("ATTACK");
                attackTime = 1f;
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
                    jump.gameObject.SetActive(true);
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
                    dash.gameObject.SetActive(true);
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
                    jump.gameObject.SetActive(true);
                }

                break;
            case PlayerState.SPRINT:

                rb2D.velocity = dirInput.normalized * sprintSpeed * 5f;
                dash.gameObject.SetActive(true);


                // TO WALK
                if (!sprintInput && dirInput != Vector2.zero)
                {
                    TransitionToState(PlayerState.WALK);
                    dash.gameObject.SetActive(false);
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
                    jump.gameObject.SetActive(true);
                    
                }

                break;
            case PlayerState.JUMP:

                rb2D.velocity = dirInput.normalized * walkSpeed * 5f;
                jump.gameObject.SetActive(true);
                if (jumpTimer < jumpDuration)
                {

                    // SAUT
                    jumpTimer += Time.deltaTime;

                    float y = jumpCurve.Evaluate(jumpTimer / jumpDuration);

                    graphics.transform.localPosition = new Vector3(0, y * jumpHeight, 0);

                    if (Input.GetButtonDown("Attack"))
                    {
                        //TransitionToState(PlayerState.ATTACK);
                        animator.SetTrigger("ATTACK");
                    }

                }
                else
                {
                    // FIN DE SAUT
                    jumpTimer = 0f;
                    jump.gameObject.SetActive(false);
                    TransitionToState(PlayerState.IDLE);
                }
                break;
            case PlayerState.CAN:
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
                    jump.gameObject.SetActive(true);
                }
                break;
            case PlayerState.ATTACK:

                attackTime -= Time.deltaTime;
                if (attackTime <= 0)
                {


                }

                // TO ATTACK
                if (Input.GetButtonDown("Attack"))
                {
                    TransitionToState(PlayerState.ATTACK);
                }

                // TO IDLE
                if (dirInput == Vector2.zero)
                {
                    TransitionToState(PlayerState.IDLE);
                }
                // TO WALK
                if (!sprintInput && dirInput != Vector2.zero)
                {
                    TransitionToState(PlayerState.WALK);
                }
                // TO SPRINT
                if (sprintInput)
                {
                    TransitionToState(PlayerState.SPRINT);
                    dash.gameObject.SetActive(true);
                    shock.gameObject.SetActive(true);
                }
                // TO JUMP
                if (Input.GetButtonDown("Jump"))
                {
                    TransitionToState(PlayerState.JUMP);
                    jump.gameObject.SetActive(true);
                }

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
                dash.gameObject.SetActive(false);
                //shock.gameObject.SetActive(false);
                break;
            case PlayerState.JUMP:
                jump.gameObject.SetActive(false);
                break;
            case PlayerState.CAN:
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

    public void PlayerDead()
    {
        TransitionToState(PlayerState.DEATH);
    }


}






