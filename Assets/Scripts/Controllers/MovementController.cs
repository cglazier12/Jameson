using UnityEngine;

namespace Controllers
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        // Update is called once per frame
        public void HandleMovement(Vector2 direction)
        {
            // Simple move logic for demonstration purposes
            transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
        }
    }    
}
