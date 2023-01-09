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
            animator.SetFloat("DirX", dirInput.x);
        }
        animator.SetFloat("DirMagnitude", dirInput.magnitude);
        sprintInput = Input.GetButton("Sprint");
        animator.SetBool("SPRINT", sprintInput && dirInput.magnitude > 0);
        animator.SetBool("WALK", dirInput.magnitude > 0);
        if (Input.GetButtonDown("Jump"))
        { 
            animator.SetTrigger("JUMP");
        }
        if (Input.GetButtonDown("Attack"))
        {
            animator.SetTrigger("ATTACK");
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
                break;
            case PlayerState.JUMP:
                break;
            case PlayerState.ATTACK:
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
                if (dirInput != Vector2.zero)
                {
                    TransitionToState(sprintInput ? PlayerState.SPRINT : PlayerState.WALK);
                    right = dirInput.x > 0;
                    graphics.transform.rotation = right ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
                }
                rb2D.velocity = Vector2.zero;
                //rb2D.velocity = dirInput.normalized * sprintSpeed * 5f;
                break;
            case PlayerState.WALK:
                if (dirInput == Vector2.zero)
                {
                    TransitionToState(PlayerState.IDLE);
                }

                if (sprintInput)
                {
                    TransitionToState(PlayerState.SPRINT);
                }
                rb2D.velocity = dirInput.normalized * sprintSpeed * 5f;
                break;
            case PlayerState.SPRINT:

                if (!sprintInput && dirInput != Vector2.zero)
                {
                    TransitionToState(PlayerState.WALK);
                }
                rb2D.velocity = dirInput.normalized * sprintSpeed * 5f;
                break;
            case PlayerState.JUMP:
                if (jumpInput)
                {
                    TransitionToState(PlayerState.JUMP);
                }
                currentSpeed = jumpHeight;
                if (jumpInput)
                {
                    RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, -transform.up, .1f);
                    Vector2 rayOrigin = new Vector2(cc2D.bounds.center.x, cc2D.bounds.min.y);
                    bool hit = Physics2D.Raycast(rayOrigin, -transform.up);

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






