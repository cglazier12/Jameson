using Entities;
using UnityEngine;

namespace Controllers
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
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
            
            move++; // Assuming you want to keep track of how many times the player moves
            transform.position += new Vector3(direction.x, 0, 0) * (speed * Time.deltaTime);
        }

        public void Jump(float jumpForce)
        {

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
