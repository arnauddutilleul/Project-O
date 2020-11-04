using TMPro;
using UnityEngine;

namespace PickableObjects
{
    [RequireComponent(typeof(TMP_Text))]
    public class RedPotionsText : MonoBehaviour
    {
        private TMP_Text text;
        private int _numberPotions;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        public void ChangeNumberPotions(int value)
        {
            _numberPotions += value;
            text.text = _numberPotions.ToString();
        }
    }
}
