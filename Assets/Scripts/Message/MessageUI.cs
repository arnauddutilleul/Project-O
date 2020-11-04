using TMPro;
using UnityEngine;

namespace Message
{
    public class MessageUI : MonoBehaviour
    {
        private TMP_Text text;
        
        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        public void ChangeMessageToPlayer(string message)
        {
            text.text = message;
            Invoke(nameof(ClearMessage), 5);
        }

        private void ClearMessage()
        {
            text.text = "";
        }
    }
}
