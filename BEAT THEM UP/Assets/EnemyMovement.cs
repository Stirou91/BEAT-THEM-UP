using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] EnemeState currentState;
    [SerializeField] private float limitNearTarget = 0.5f;
    [SerializeField] private float waitingTimeBeforeAttack = 1f;
    [SerializeField] private float attackDuration = 0.2f;
    [SerializeField] private GameObject hitbox;



    CapsuleCollider2D cc2D;
    float limit;
    private bool _PlayerDetected = false;
    private Transform moveTarget;
    private float attackTimer;

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
                attackTimer = 0f;
                break;
            case EnemeState.WALK:
                animator.SetBool("WALK", true);
                break;
            case EnemeState.ATTACK:
                attackTimer = 0f;
                hitbox.SetActive(true);
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

                // J'AI DETECTE LE PLAYER ET JE SUIS SUR LUI
                if (_PlayerDetected && !IsTargetNearLimit())
                {
                    TransitionToState(EnemeState.WALK);
                }
                // JE SUIS EN IDLE PRET DU JOUEUR
                if (IsTargetNearLimit())
                {
                    attackTimer += Time.deltaTime;
                    if (attackTimer >= waitingTimeBeforeAttack)
                    {
                        TransitionToState(EnemeState.ATTACK);
                        
                    }
                }

                break;
            case EnemeState.WALK:

                transform.position = Vector2.MoveTowards(transform.position, moveTarget.position, Time.deltaTime);

                if (IsTargetNearLimit() )
                {
                    TransitionToState(EnemeState.IDLE);
                }

                if (!_PlayerDetected)
                {
                    TransitionToState(EnemeState.IDLE);
                }


                break;
            case EnemeState.ATTACK:
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackDuration)
                {
                    // J'AI FINI D'ATTAQUER
                    TransitionToState(EnemeState.IDLE);
                }
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
                hitbox.SetActive(false);
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

    void PlayerDetected(GameObject playerDetected)

    {
        moveTarget = playerDetected.transform;
        _PlayerDetected = true;

    }

    void PlayerEscaped()
    {

        moveTarget = null;
        _PlayerDetected = false;

    }
     bool IsTargetNearLimit()
    {

        return Vector2.Distance(transform.position, moveTarget.position) < limitNearTarget;
    }



}
