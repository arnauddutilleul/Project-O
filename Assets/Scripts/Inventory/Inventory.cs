using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {
        private IList<GameObject> _inventoryList;
        public GameObject flashlight;

        public IList<GameObject> InventoryList
        {
            get { return _inventoryList; }
        }
        void Start()
        {
            _inventoryList = new List<GameObject>(); 
        }

        public void TakeObject(GameObject obj)
        {
            InventoryList.Add(obj);

            if (obj.name == "Flashlight")
            {
                flashlight.SetActive(true);
            }
        }
    }
}
