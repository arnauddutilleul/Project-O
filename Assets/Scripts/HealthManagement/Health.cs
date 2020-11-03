using UnityEngine;
using UnityEngine.Events;

namespace HealthManagement
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private int currentHealth;
        [SerializeField] private int maxHealth;
        [SerializeField] private UnityEvent onDeath;
        [SerializeField] private UnityEvent damageSound;

        private void Start()
        {
            HealthManager.Instance.InitializationHealthBar();
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            HealthManager.Instance.ModifyHealth(currentHealth);
            damageSound.Invoke();
            if (currentHealth <= 0)
            {
                //Death Animation
                //Block input of movement
                //Wait for the end of animation
                onDeath?.Invoke();
            }
        }

        public bool Heal()
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += 1;
                HealthManager.Instance.ModifyHealth(currentHealth);
                return true;
            }
            
            Debug.Log("Max Life !");
            return false;
        }

    }
}