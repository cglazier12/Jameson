using Controllers;
using UnityEngine;
using Controllers.Characters;
using Sirenix.OdinInspector;

namespace Entities
{
    public class Player : Character
    {
        [SerializeField] private float jumpForce = 7f;
        [SerializeField] private bool isGrounded;
        [SerializeField] private bool shouldJump; // Flag to indicate the jump command was given
        [SerializeField] private float horizontal;
        
        [InfoBox("Ground Check")]
        [SerializeField] private LayerMask groundLayer; // Set this in the Inspector
        [SerializeField] private Transform groundCheck; // Assign a child GameObject positioned at the feet
        [SerializeField] private float groundCheckRadius = 2; // Radius of the overlap circle to determine if grounded
        
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

            // Animations can be handled after input is processed
            HandleAnimations(horizontal); // Ensure 'horizontal' is a class-level variable
        }
        
        private void FixedUpdate()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            // Perform jump in FixedUpdate if the flag is set
            if (shouldJump && isGrounded)
            {
                movementController.Jump(jumpForce);
                shouldJump = false;
                //animator.ResetTrigger("Jump");
            }

        }

        private void HandleInput()
        {
            // Directly use the class-level field 'horizontal'
            horizontal = Input.GetAxisRaw("Horizontal");
            if (horizontal < 0)
            {
                Move(Vector2.left);
            }
            else if (horizontal > 0)
            {
                Move(Vector2.right);
            }

            // Check for the jump input and set the flag
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                shouldJump = true;
            }
        }
        
        protected override void TakeDamage(float amount)
        {
            // Player-specific damage handling
            health -= amount;
            // Handle animations and check for death
        }

        protected override void HandleDeath()
        {
            // Player-specific death handling
            base.HandleDeath(); // If there's common logic in the base method
            // Additional player death logic
        }

        // Handle animations based on the player's current action
        private void HandleAnimations(float horizontalInput)
        {
            // Handle jump animation
            if (shouldJump)
            {
                animator.SetTrigger("jump");
            }

            // Optional: handle direction facing
            if (horizontalInput > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // Face right
            }
            else if (horizontalInput < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Face left
            }

            // Add more parameters as needed for crouching, etc.
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
