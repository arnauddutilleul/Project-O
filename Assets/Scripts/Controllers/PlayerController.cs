using UnityEngine;

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
             if (Input.GetButtonDown("Jump"))
                 movement.Jump();

             if(Input.GetKey(KeyCode.LeftControl))
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
             else if(Input.GetKeyUp(KeyCode.LeftControl))
             {
                 if(_pressTime < _pressTimeTollerance)
                 {
                     movement.Sit();
                 }
 
                 _pressTime = 0f;
             }
             
         }
}