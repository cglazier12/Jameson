using UnityEngine;

namespace Controllers.Characters
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] protected float health = 100f;
        public MovementController movementController;
        protected Animator animator; // This should be Unity's Animator component
        protected DamageController damageController;

        public int move;
        
        protected virtual void Awake()
        {
            // Initialize controllers
            movementController = GetComponent<MovementController>();
            animator = GetComponent<Animator>();
            damageController = GetComponent<DamageController>();
        }

        protected virtual void Move(Vector2 direction)
        {
            
            movementController.HandleMovement(direction);
            // Additional move logic or animation triggers
        }

        // This method is protected and abstract, to be implemented by subclasses
        protected abstract void TakeDamage(float amount);

        // This is the public method that the DamageController will call
        public void ReceiveDamage(float damage)
        {
            TakeDamage(damage);
            // Check for health, death, etc.
            if (health <= 0) HandleDeath();
        }

        protected virtual void HandleDeath()
        {
            // Death logic, such as playing an animation, disabling the character, etc.
        }

        // Other common character methods...
    }
}