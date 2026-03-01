using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerInput.MainActions input;

     public KeyCode switchKey = KeyCode.Q;



    CharacterController controller;
    Animator animator;
    AudioSource audioSource;

    [Header("Controller")]

    public DisableArmsEnableGun arms;
    public float moveSpeed = 5;
    public float gravity = -9.8f;
    public float jumpHeight = 1.2f;
    [Header("Attacking")]
    public swordInfo sword;
    public axeInfo axe;
    public warHammerInfo hammer;
    public spearInfo spear;
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public float knockbackForce = 5f; 
    public LayerMask attackLayer;


    

    Vector3 _PlayerVelocity;

    bool isGrounded;

    [Header("Camera")]
    public Camera cam;
    public float sensitivity;

    float xRotation = 0f;

    void Awake()
    { 
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        playerInput = new PlayerInput();
        input = playerInput.Main;
        AssignInputs();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (Input.GetKeyDown(switchKey) && arms.showArms == true)
        {
            if (arms.weapon == "hammer")
            {
                attackDistance = hammer.attackDistance;
                attackDamage = hammer.attackDamage;
                attackDelay = hammer.attackDelay;
                attackSpeed = hammer.attackSpeed;
            }
            else if (arms.weapon == "sword")
            {
                attackDistance = sword.attackDistance;
                attackDamage = sword.attackDamage;
                attackDelay = sword.attackDelay;
                attackSpeed = sword.attackSpeed;
            }
            else if (arms.weapon == "axe")
            {
                attackDistance = axe.attackDistance;
                attackDamage = axe.attackDamage;
                attackDelay = axe.attackDelay;
                attackSpeed = axe.attackSpeed;
            }
            else if (arms.weapon == "spear")
            {
                attackDistance = spear.attackDistance;
                attackDamage = spear.attackDamage;
                attackDelay = spear.attackDelay;
                attackSpeed = spear.attackSpeed;
            }
        }

        // Repeat Inputs
        if(input.Attack.IsPressed())
        { Attack(); }

        SetAnimations();
    }

    void FixedUpdate() 
    { MoveInput(input.Movement.ReadValue<Vector2>()); }

    void LateUpdate() 
    { LookInput(input.Look.ReadValue<Vector2>()); }

    void MoveInput(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        controller.Move(transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
        _PlayerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && _PlayerVelocity.y < 0)
            _PlayerVelocity.y = -2f;
        controller.Move(_PlayerVelocity * Time.deltaTime);
    }

    void LookInput(Vector3 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime * sensitivity);
        xRotation = Mathf.Clamp(xRotation, -80, 80);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime * sensitivity));
    }

    void OnEnable() 
    { input.Enable(); }

    void OnDisable()
    { input.Disable(); }

    void Jump()
    {
       
        if (isGrounded)
            _PlayerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    }

    void AssignInputs()
    {
        input.Jump.performed += ctx => Jump();
        input.Attack.started += ctx => Attack();
    }

    

    public const string IDLE = "Idle";
    public const string WALK = "Walk";
    public const string ATTACK1 = "Attack 1";
    public const string ATTACK2 = "Attack 2";

    string currentAnimationState;

    public void ChangeAnimationState(string newState) 
    {
       
        if (currentAnimationState == newState) return;

       
        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }

    void SetAnimations()
    {
        
        if(!attacking)
        {
            if(_PlayerVelocity.x == 0 &&_PlayerVelocity.z == 0)
            { ChangeAnimationState(IDLE); }
            else
            { ChangeAnimationState(WALK); }
        }
    }

    

    
    public GameObject hitEffect;
    public AudioClip swordSwing;
    public AudioClip hitSound;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;

    

    public void Attack()
    {
        
        if(!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(swordSwing);

        if(attackCount == 0)
        {
            ChangeAnimationState(ATTACK1);
            attackCount++;
        }
        else
        {
            ChangeAnimationState(ATTACK2);
            attackCount = 0;
        }
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRaycast()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer) && arms.showArms == true)
        { 
            HitTarget(hit.point);

            if(hit.transform.TryGetComponent<Actor>(out Actor T))
            { T.TakeDamage(attackDamage); }

            ApplyKnockback(hit);
        } 
    }

    void HitTarget(Vector3 pos)
    {
        audioSource.pitch = 1;
        audioSource.PlayOneShot(hitSound);

        GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20);
    }
    void ApplyKnockback(RaycastHit hit)
{
    if (arms.weapon != "hammer") return; // Only apply knockback for hammer

    Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
    if (rb != null)
    {
        Vector3 knockDirection = hit.transform.position - transform.position;
        knockDirection.y = 0; 
        knockDirection.Normalize();

        
        rb.AddForce(knockDirection * knockbackForce, ForceMode.Impulse);
    }
}
}