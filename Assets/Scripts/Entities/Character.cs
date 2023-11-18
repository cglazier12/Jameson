using UnityEngine;
using Controllers;

namespace Entities
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] protected float health = 100f;
        protected MovementController movementController;
        protected Animator animator; // Ensure this is Unity's Animator
        protected DamageController damageController;

        protected virtual void Awake()
        {
            // Initialize controllers
            movementController = GetComponent<MovementController>();
            animator = GetComponent<Animator>();
            damageController = GetComponent<DamageController>();
        }

        protected virtual void Move(Vector2 direction)
        {
            movementController?.HandleMovement(direction);
            // Additional move logic or animation triggers
        }

        protected abstract void TakeDamage(float amount);

        public virtual void ApplyDamage(float damage)
        {
            TakeDamage(damage);
            // Check for health, death, etc.
            if (health <= 0) HandleDeath();
        }

        protected virtual void HandleDeath()
        {
            // Death logic
        }

        // Other common character methods...
    }
}