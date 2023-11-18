using Controllers;
using Controllers.Characters;
using UnityEngine;

namespace Controllers
{
    public class DamageController : MonoBehaviour
    {
        public enum DamageType { Physical, Magical, Fire /* etc. can be removed or replaced with actual types */ }

        // Delegate and event for damage application
        public delegate void DamageApplied(Character character, float damageAmount, DamageType type);
        public event DamageApplied OnDamageApplied;

        public void ApplyDamage(Character character, float baseDamageAmount, DamageType type = DamageType.Physical)
        {
            float finalDamage = CalculateFinalDamage(baseDamageAmount, type);
            character.ReceiveDamage(finalDamage); // This method needs to be public or internally callable

            // Trigger the event
            OnDamageApplied?.Invoke(character, finalDamage, type);
        }

        private float CalculateFinalDamage(float baseDamage, DamageType type)
        {
            // Implementation to use 'type'
            // For now, let's just return the base damage as a placeholder
            return baseDamage;
        }

        // Other methods related to damage handling...
    }
}
