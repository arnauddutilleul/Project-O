using CameraScripts;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Movement movement;
        [SerializeField] private CameraInput cameraInput;

        // Update is called once per frame
        void Update()
        {
            //deplacement player
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            movement.Move(x, z);

            //deplacement camera
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            cameraInput.CameraRotation(mouseX, mouseY);
            
            //palayer jump
            if (Input.GetButtonDown("Jump"))
                movement.Jump();
            
            //player sprint
            if (Input.GetKeyDown(KeyCode.LeftShift))
                movement.Run(true);
            if (Input.GetKeyUp(KeyCode.LeftShift))
                movement.Run(false);
        }
    }
}