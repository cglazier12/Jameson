using UnityEngine;

namespace Controllers
{
    public class DamageController : MonoBehaviour
    {
        public void ApplyDamage(Character character, float damageAmount)
        {
            // The TakeDamage method would be called on the character
            character.TakeDamage(damageAmount);
        }
    }    
}
