using System;
using Pooling;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    public class Takeable : MonoBehaviour
    {

        private GameObject _takeMeGameObject;
        public UnityEvent takeMeEvent;
        public TMP_Text textTakeMe;

        private bool _inside;

        private void Start()
        {
            _takeMeGameObject = textTakeMe.transform.parent.gameObject;
            _takeMeGameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            textTakeMe.text = "Take " + gameObject.name;
            _takeMeGameObject.SetActive(true);
            _inside = true;
        }

        private void OnTriggerExit(Collider other)
        {
            _takeMeGameObject.SetActive(false);
            textTakeMe.text = "";
            _inside = false;
        }

        private void Update()
        {
            if (_inside && Input.GetKeyDown(KeyCode.E))
            {
                takeMeEvent?.Invoke();
                _takeMeGameObject.SetActive(false);
                gameObject.TryRelease();
            }
        }
    }
}
