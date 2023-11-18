using UnityEngine;

namespace Controllers
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        private Rigidbody2D rb;
        private bool isGrounded;
        public int move;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void HandleMovement(Vector2 direction)
        {
            move++; // Assuming you want to keep track of how many times the player moves
            transform.position += new Vector3(direction.x, 0, 0) * (speed * Time.deltaTime);
        }

        public void Jump(float jumpForce)
        {
            if (isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Simple ground check
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }
    }
}
