using System;
using UnityEngine;
using UnityEngine.Events;

namespace HealthManagement
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private int currentHealth;
        [SerializeField] private int maxHealth;
        [SerializeField] private UnityEvent onDeath;

        private void Start()
        {
            HealthManager.Instance.InitializationHealthBar();
        }

        // Start is called before the first frame update
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            HealthManager.Instance.ModifyHealth(currentHealth);
            if (currentHealth <= 0)
            {
                //Death Animation
                //Block input of movement
                //Wait for the end of animation
                onDeath?.Invoke();
            }
        }
    }
}