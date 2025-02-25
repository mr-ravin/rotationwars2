using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
public class player1move : MonoBehaviour
{
    private float delta_speed;    
    private float move_speed = 4.5f, keyboard_move_speed = 4.5f, controller_move_speed = 4.5f;
    private bool isLeftPressed, isRightPressed, isUpPressed, isDownPressed;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isLeftPressed = isRightPressed = isUpPressed = isDownPressed = false;
    }

    void FixedUpdate()
    {
        delta_speed = move_speed * Time.deltaTime;
        #if UNITY_ANDROID
            android_movement();
        #else
            linux_and_webgl_movement();
        #endif
    }

    public void linux_and_webgl_movement()
    {   
        Vector3 moveDirection = Vector3.zero;
        #if ENABLE_INPUT_SYSTEM
            if (Keyboard.current.aKey.isPressed || isLeftPressed)
            {
                moveDirection += Vector3.left;
            }
            if (Keyboard.current.dKey.isPressed || isRightPressed)
            {
                moveDirection += Vector3.right;
            }
            if (Keyboard.current.wKey.isPressed || isUpPressed)
            {
                moveDirection += Vector3.forward;
            }
            if (Keyboard.current.sKey.isPressed || isDownPressed)
            {
                moveDirection += Vector3.back;
            }
        #else
            if (Input.GetKey(KeyCode.A) || isLeftPressed)
            {
                moveDirection += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D) || isRightPressed)
            {
                moveDirection += Vector3.right;
            }
            if (Input.GetKey(KeyCode.W) || isUpPressed)
            {
                moveDirection += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S) || isDownPressed)
            {
                moveDirection += Vector3.back;
            }
        #endif
        moveDirection.Normalize(); 
        rb.MovePosition(rb.position + moveDirection * delta_speed);
    }

    public void android_movement()
    {
        Vector3 moveDirection = Vector3.zero;
        if(isLeftPressed)
        {
            moveDirection += Vector3.left;
        }
        if(isRightPressed)
        {
            moveDirection += Vector3.right;
        }
        if(isUpPressed)
        {
            moveDirection += Vector3.forward;
        }
        if(isDownPressed)
        {
            moveDirection += Vector3.back;
        }
        moveDirection.Normalize();
        rb.MovePosition(rb.position + moveDirection * delta_speed);
    }

    public void MoveLeft()
    {
        isLeftPressed = true;
        move_speed = controller_move_speed;
    }
    
    public void MoveRight()
    {
        isRightPressed = true;
        move_speed = controller_move_speed;
    }

    public void MoveUp()
    {
        isUpPressed = true;
        move_speed = controller_move_speed;
    }

    public void MoveDown()
    {
        isDownPressed = true;
        move_speed = controller_move_speed;
    }

    public void StopMovement()
    {
        isLeftPressed = isRightPressed = isUpPressed = isDownPressed = false;
        move_speed = keyboard_move_speed;
    }
}