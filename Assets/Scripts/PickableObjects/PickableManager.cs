using System.Collections.Generic;
using HealthManagement;
using UnityEngine;

namespace PickableObjects
{
    public class PickableManager : MonoBehaviour, IPickable
    {
        [SerializeField] private int maxAmountObjects;
        private readonly Dictionary<GameObject, int> _inventory = new Dictionary<GameObject, int>();
        [SerializeField] private Health health;
        [SerializeField] private RedPotionsText redPotionsUI;


        public void TakeObject(GameObject obj)
        {
            if (!_inventory.ContainsKey(obj)) {
                _inventory.Add(obj, 1);
                redPotionsUI.ChangeNumberPotions(+1);
            } else {
                if (_inventory[obj] < maxAmountObjects)
                {
                    if (obj.name == "RedPotions")
                        redPotionsUI.ChangeNumberPotions(+1);
                    
                    _inventory[obj] += 1;
                }
            }
            
        }

        public void ConsumeObject(GameObject obj)
        {
            if (_inventory.ContainsKey(obj)) {
                if (health.Heal())
                {
                    _inventory[obj] -= 1;
                    redPotionsUI.ChangeNumberPotions(-1);
                }
                if (_inventory[obj] == 0)
                    _inventory.Remove(obj);
            }
        }
    }
}