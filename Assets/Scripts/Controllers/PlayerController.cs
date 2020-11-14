using UnityEngine;
using UnityEngine.Events;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Movement movement;
        private float _pressTime;
        private const float PressTimeTollerance = 0.5f;
        [SerializeField] private UnityEvent decreaseRedPotions;
        [SerializeField] private GameObject exitMenu;


        // Update is called once per frame
        void Update()
        {
            //déplacement droite-gauche
            var h = Input.GetAxisRaw("Horizontal");
            movement.Move(h, 0);
             
            //jump
            if (Input.GetButtonDown("Jump") || (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0))
                movement.Jump();

            //Sit
            if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.DownArrow))
            {
                if(_pressTime < PressTimeTollerance)
                {
                    _pressTime += Time.deltaTime;
                }
                else
                {
                    movement.Sit();
                }
            }
            else if(Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                if(_pressTime < PressTimeTollerance)
                {
                    movement.Sit();
                }
 
                _pressTime = 0f;
            }
            
            //Potions E
            if (Input.GetKeyDown(KeyCode.E))
            {
                decreaseRedPotions?.Invoke();
            }

            //Menu Eschap
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                exitMenu.SetActive(true);
            }
        }
    }
}