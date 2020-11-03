using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Movement movement;
        private float _pressTime;
        private const float _pressTimeTollerance = 0.5f;

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
                if(_pressTime < _pressTimeTollerance)
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
                if(_pressTime < _pressTimeTollerance)
                {
                    movement.Sit();
                }
 
                _pressTime = 0f;
            }
             
        }
    }
}