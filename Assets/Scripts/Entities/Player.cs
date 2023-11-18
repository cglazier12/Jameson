using Controllers;
using UnityEngine;
using Controllers.Characters;

namespace Entities
{
    public class Player : Character
    {
        [SerializeField] private float jumpForce = 7f;
        private bool isGrounded;
        [SerializeField] private bool shouldJump; // Flag to indicate the jump command was given
        [SerializeField] private float horizontal;
        
        
        protected virtual void Awake()
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
            // Perform jump in FixedUpdate if the flag is set
            if (shouldJump)
            {
                Jump();
                shouldJump = false; // Reset the jump command after executing it
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


        // The Jump method is now only responsible for applying the force
        private void Jump()
        {
            // Add a vertical force to the rigidbody to make the character jump
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
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
            // Animation handling code...
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Ground check to enable jumping again
            if (collision.gameObject.CompareTag("Ground")) // Make sure this matches the exact spelling of the tag you created
            {
                isGrounded = true;
            }
        }
    }
}
