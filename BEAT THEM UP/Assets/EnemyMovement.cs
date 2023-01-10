using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] EnemeState currentState;

    CapsuleCollider2D cc2D;
    float currentSpeed;

    // Start is called before the first frame update

    public enum EnemeState
    {
        IDLE,
        WALK,
        ATTACK,
        DEATH

    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        cc2D = GetComponent<CapsuleCollider2D>();
        currentState = EnemeState.IDLE;
        OnStateEnter(); 
    }

    // Update is called once per frame
    void Update()
    {
     

        OnStateUpdate();
    }

    void OnStateEnter()
    {
        switch (currentState)
        {
            case EnemeState.IDLE:
                break;
            case EnemeState.WALK:
                animator.SetBool("WALK", true);
                break;
            case EnemeState.ATTACK:
                animator.SetTrigger("ATTACK");
                break;
            case EnemeState.DEATH:
                break;
            default:
                break;
        }

    }


    void OnStateUpdate()
    {
        switch (currentState)
        {
            case EnemeState.IDLE:

                if(_PlayerDetected)
                {
                    TransitionToState(EnemyState.WALK).
                }

                break;
            case EnemeState.WALK:

                if (_PlayerDetected)
                {
                    TransitionToState(EnemyState.IDLE);
                }


                break;
            case EnemeState.ATTACK:
                break;
            case EnemeState.DEATH:
                break;
            default:
                break;
        }

    }

    void OnStateExist()
    {
        switch (currentState)
        {
            case EnemeState.IDLE:
                break;
            case EnemeState.WALK:
                animator.SetBool("WALK", false);
                break;
            case EnemeState.ATTACK:
                animator.SetTrigger("ATTACK");
                break;
            case EnemeState.DEATH:
                break;
            default:
                break;
        }


    }

    void TransitionToState(EnemeState nextState)
    {
        OnStateExist();
        currentState = nextState;
        OnStateEnter();

    }

    void PlayerDetected()

    {
        _PlayerDetected = true;

    }

    void PlayerEscaped()
    {
        _PlayerDetected = false;
    }

    private EnemyState _currentState;
    private bool _PlayerDetected = false;

}
