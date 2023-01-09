using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSM : MonoBehaviour
{
    [SerializeField] PlayerState currentState;
    [SerializeField] Animator animator;
    Rigidbody2D rb2D;
    [SerializeField] float jumpHeight = 10f;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 8f;

    CapsuleCollider2D cc2D;
    float currentSpeed;
    float attackTime;


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
       
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }
    private void GetInput()
    {
        dirInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        sprintInput = Input.GetButton("Sprint");
        animator.SetBool("SPRINT", sprintInput && dirInput.magnitude > 0);
    }





}




