using UnityEngine;

namespace CameraScripts
{
    public class CameraAnimation : MonoBehaviour
    {
        public CharacterController playerController; 
        public Animation anim;
        private bool isMoving;
    
        private bool left; 
        private bool right;
    
        void CameraAnimations(){
            
            if(playerController.isGrounded){
                if(isMoving){
                    if(left){
                        if(!anim.isPlaying){
                            anim.Play("walkLeft");
                            left = false;
                            right = true;
                        }
                    }
                    if(right){
                        if(!anim.isPlaying){
                            anim.Play("walkRight");
                            right = false;
                            left = true;
                        }
                    }
                }
                else
                {
                    if (!anim.isPlaying)
                    {
                        anim.Play("idle");
                        right = false;
                        left = true;
                    }
                }         
            }
        }
    
 
        void Start () { 
            left = true;
            right = false;
        }


        void Update()
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");

            if (inputX != 0 || inputY != 0)
            {
                isMoving = true;
            }
            else if (inputX == 0 && inputY == 0)
            {
                isMoving = false;
            }

            CameraAnimations();
        }
    }
}
