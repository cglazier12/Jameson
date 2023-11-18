using UnityEngine;

namespace Controllers
{
    public class BasicMovement : MonoBehaviour
    {
        [SerializeField] private float walkSpeed = 5f;
        [SerializeField] private float jumpForce = 200;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float groundCheckDistance = 0.1f;

        private Rigidbody2D body;
        private bool isGrounded;
        private bool isJumping;
        private float xAxisInput;
        
        
        public bool IsMoving => xAxisInput != 0;
        public bool IsJumping => isJumping;

        protected virtual void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        protected virtual void Update()
        {
            xAxisInput = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                isJumping = true;
            }
        }

        protected virtual void FixedUpdate()
        {
            CheckGroundStatus();
            ApplyMovement();
            ApplyJump();
        }

        private void CheckGroundStatus()
        {
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundMask);
            if (isGrounded) 
            {
                isJumping = false;
            }
        }

        private void ApplyMovement()
        {
            Vector2 vel = new Vector2(xAxisInput * walkSpeed, body.velocity.y);
            body.velocity = vel;
        }

        private void ApplyJump()
        {
            if (isJumping)
            {
                body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = false;
            }
        }
    }
}