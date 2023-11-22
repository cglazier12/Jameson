using Controllers;
using UnityEngine;
using Controllers.Characters;
using Sirenix.OdinInspector;

namespace Entities
{
    public class Player : Character
    {
        [SerializeField] private float jumpForce = 1f;
        [SerializeField] private bool isGrounded;
        [SerializeField] private bool isJumpPressed; // Flag to indicate the jump command was given
        [SerializeField] private bool isAttackPressed; // Flag to indicate the attack command was given
        [SerializeField] private float horizontal;
        
        [InfoBox("Ground Check")]
        [SerializeField] private LayerMask groundLayer; // Set this in the Inspector
        [SerializeField] private Transform groundCheck; // Assign a child GameObject positioned at the feet
        [SerializeField] private float groundCheckRadius = 2; // Radius of the overlap circle to determine if grounded

        [SerializeField] private string currentAnimation;
        private const string IDLE = "Idle";
        private const string RUN = "Run";
        private const string JUMP = "Jump";
        private const string CROUCH = "Crouch";
        
        protected override void Awake()
        {
            // Initialize controllers
            movementController = GetComponent<MovementController>();
            if (movementController == null)
            {
                movementController = gameObject.AddComponent<MovementController>();
            }

            animator = GetComponent<Animator>();
            if (animator == null)
            {
                animator = gameObject.AddComponent<Animator>();
            }

            damageController = GetComponent<DamageController>();
            if (damageController == null)
            {
                damageController = gameObject.AddComponent<DamageController>();
            }
        }
        
        private void Update()
        {
            // Gather input from the player
            HandleInput();
        }
        
        //=====================================================
        // Physics based time step loop
        //=====================================================
        private void FixedUpdate()
        {
            //check if player is on the ground
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            
            //------------------------------------------

            //update horizontal movement based on input
            if (horizontal < 0)
            {
                Move(Vector2.left);
            }
            else if (horizontal > 0)
            {
                Move(Vector2.right);
            }
            
            // Handle animations based on the player's current action
            if (isGrounded && !isJumpPressed)
            {
                ChangeAnimationState(horizontal != 0 ? RUN : IDLE);
            }
    
            // Check if trying to jump
            if (isJumpPressed && isGrounded)
            {
                movementController.Jump(jumpForce);
                ChangeAnimationState(JUMP);
                isJumpPressed = false;
            }
        }

        private void HandleInput()
        {
            // Check for horizontal movement
            horizontal = Input.GetAxisRaw("Horizontal");

            // Check if the jump key was pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumpPressed = true; // Set the jump request flag
            }

            // Check if the attack key was pressed
            isAttackPressed = Input.GetKeyDown(KeyCode.RightControl);
        }
        protected override void TakeDamage(float amount)
        {
            // Player-specific damage handling
            health -= amount;
            // Handle animations and check for death
        }

        // Handle animations based on the player's current action
        void ChangeAnimationState(string newAnimation)
        {
            if (currentAnimation == newAnimation) return;

            animator.Play(newAnimation);
            currentAnimation = newAnimation;
        }

        /*
         *  ===== GIZMOS =====
         */
        
        void OnDrawGizmos()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }
        }
    }
}
