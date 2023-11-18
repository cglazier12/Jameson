
namespace Entities
{
    public abstract class Character
    {
        protected float health;
        protected MovementController movementController;
        protected Animator animator;
        protected DamageController damageController;

        protected virtual void Move() { /* Common movement logic */ }
        protected abstract void TakeDamage(float amount);
        // Other common character methods...
    }
}
