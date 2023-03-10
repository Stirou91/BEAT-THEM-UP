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
    [SerializeField] GameObject graphics;
    [SerializeField] public float currentHealth;
    [SerializeField] private float deathTimer = 0.5f;
    [SerializeField] GameObject diskPrefab;


    CapsuleCollider2D cc2D;
    float limit;
    private bool _PlayerDetected = false;
    private Transform moveTarget;
    private float attackTimer;
    float damageTaken;
    Vector2 enemydir;
    bool right = true;
    bool death = true;
    bool isHurt;
    internal GameObject player;

    // Start is called before the first frame update

    public enum EnemeState
    {
        IDLE,
        WALK,
        ATTACK,
        HURT,
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
        Rotation();

        OnStateUpdate();
    }
   
   
    private void Rotation()
    {
        if (moveTarget == null)
        {
            return;

        }


        enemydir = moveTarget.position - transform.position;

        if (enemydir.x < 0)
        {
            right = false;
        }

        if (enemydir.x > 0)
        {
            right = true;
        }

        if (right)
        {
            graphics.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            graphics.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
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
            case EnemeState.HURT:
                animator.SetTrigger("HURT");
                rb2D.velocity = Vector2.zero;
                if (!isHurt)
                {
                    isHurt = true;
                }

                break;
            case EnemeState.DEATH:
                animator.SetTrigger("DEATH");
                rb2D.velocity = Vector2.zero;
                cc2D.enabled = false;
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
                if (_PlayerDetected && IsTargetNearLimit())
                {
                    attackTimer += Time.deltaTime;
                    if (attackTimer >= waitingTimeBeforeAttack)
                    {
                        TransitionToState(EnemeState.ATTACK);
                        
                    }
                }

                break;
            case EnemeState.WALK:


                rb2D.velocity = enemydir.normalized * 1;
                //transform.position = Vector2.MoveTowards(transform.position, moveTarget.position, Time.deltaTime);
                
                if (!_PlayerDetected)
                {
                    TransitionToState(EnemeState.IDLE);
                    return;
                }


                if (IsTargetNearLimit() )
                {
                    TransitionToState(EnemeState.ATTACK);
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
            case EnemeState.HURT:
                
                break;
            case EnemeState.DEATH:
                
                if (death)
                {
                    Destroy(gameObject, deathTimer);
                    GameObject diskPrefabgo = Instantiate(diskPrefab, transform.position, transform.rotation);
                }
                

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
                rb2D.velocity = Vector2.zero;
                animator.SetBool("WALK", false);
                break;
            case EnemeState.ATTACK:
                hitbox.SetActive(false);
                animator.SetTrigger("ATTACK");
                break;
            case EnemeState.HURT:
                animator.SetTrigger("HURT");
                break;
            case EnemeState.DEATH:
                animator.SetTrigger("DEATH");
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


    public void EnemyDead()
    {
        TransitionToState(EnemeState.DEATH);
    }
    public void EnemyHurt()
    {
        TransitionToState(EnemeState.HURT);
    }

    IEnumerator HurtColor()
    {
        float t = 0;
        float duration = .3f;

        //Color startColor = sr.color;

        while (t < duration)
        {
            t += Time.deltaTime;

            //sr.color = Color.Lerp(startColor, Color.red, hurtCurve.Evaluate(t / duration));

            yield return null;
        }

        isHurt = false;

        TransitionToState(EnemeState.IDLE);

    }
}
