using Entities;
using Sirenix.Utilities;
using UnityEngine;

namespace Controllers
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        private float horizontalMovement;
        private Rigidbody2D rb;
        [SerializeField] private float originalLinearDrag;
        [SerializeField] public float airLinearDrag = 2.0f;
        
        public int move;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            originalLinearDrag = rb.drag;
        }

        public void HandleMovement(Vector2 direction)
        {

            horizontalMovement = direction.x;
            move++; // Assuming you want to keep track of how many times the player moves
            transform.position += new Vector3(horizontalMovement, 0, 0) * (speed * Time.deltaTime);
            
            if (horizontalMovement > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // Face right
            }
            else if (horizontalMovement < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Face left
            }
        }

        public void Jump(float jumpForce)
        {

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
