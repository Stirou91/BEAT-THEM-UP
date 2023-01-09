using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] float jumpHeight = 10f;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 8f;
    [SerializeField] PlayerState currentState;

    CapsuleCollider2D cc2D;
    float currentSpeed;
    float attackTime;

    // // FIX GRAVITY
    //dirToMove = new Vector2(dirInputX* speed, rb2d.velocity.y);

    // MOVE
    //rb2d.velocity = dirToMove;


    public enum PlayerState
    {
        IDLE,
        WALK,
        JUMP,
        ATTACK,
        SPRINT,
        DEATH
        
    }


    Vector2 dirInput;
    bool sprintInput;

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
       // .SetActive();
        GetInput();

        OnStateUpdate();
    }


    private void GetInput()
    {
        dirInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (dirInput != Vector2.zero)
        {
            animator.SetFloat("DirX", dirInput.x);
            animator.SetFloat("DirY", dirInput.y);
        }

        animator.SetFloat("DirMagnitude", dirInput.magnitude);
        sprintInput = Input.GetButton("Sprint");
        animator.SetBool("SPRINT", sprintInput);
    }

    void OnStateEnter()
    {
        switch (currentState)
        {
            case PlayerState.IDLE:
                currentSpeed = walkSpeed;
                rb2D.velocity = Vector2.zero;
                break;
            case PlayerState.WALK:
                currentSpeed = walkSpeed;
                break;
            case PlayerState.SPRINT:
                currentSpeed = sprintSpeed;
                break;
            case PlayerState.JUMP:
                currentSpeed = jumpHeight;
                animator.SetTrigger("JUMP");
                break;
            case    PlayerState.ATTACK:
                animator.SetTrigger("ATTACK");
                break;
            case PlayerState.DEATH:
                    animator.SetTrigger("DEATH");
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

                if (dirInput.magnitude > 0 && !sprintInput)
                {
                    TransitionToState(PlayerState.WALK);
                }
                if (dirInput.magnitude > 0 && sprintInput)
                {
                    TransitionToState(PlayerState.SPRINT);
                }
                if (true)
                {
                    TransitionToState(PlayerState.WALK);
                }
                if (Input.GetButtonDown("Attack 1"))
                {
                    TransitionToState(PlayerState.ATTACK);
                }
                break;
            case PlayerState.WALK:

                rb2D.velocity = dirInput.normalized * walkSpeed;

                if (dirInput.magnitude == 0)
                {
                    TransitionToState(PlayerState.IDLE);
                }
                if (sprintInput)
                {
                    TransitionToState(PlayerState.SPRINT);
                }
                if (Input.GetButtonDown("Jump"))
                {
                    TransitionToState(PlayerState.JUMP);
                }
                
                break;
            case PlayerState.SPRINT:

                rb2D.velocity = dirInput.normalized * sprintSpeed;
                if (dirInput.magnitude == 0)
                {
                    TransitionToState(PlayerState.IDLE);
                }
                if (!sprintInput)
                {
                    TransitionToState(PlayerState.WALK);
                }
                if (Input.GetButtonDown("Jump"))
                {
                    TransitionToState(PlayerState.SPRINT);
                }
                if (!sprintInput)
                {
                    TransitionToState(PlayerState.ATTACK);
                }
                if (true)
                {
                    TransitionToState(PlayerState.DEATH);
                }
                break;
            case PlayerState.ATTACK:
                rb2D.velocity = new Vector2(animator.GetFloat("DirX"), animator.GetFloat("DirY")).normalized * currentSpeed;
                attackTime -= Time.deltaTime;
                if (attackTime <= 0)
                {
                    TransitionToState(PlayerState.IDLE);
                }
                break;
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

