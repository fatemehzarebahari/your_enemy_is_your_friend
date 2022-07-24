using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 moveDirection;

    [HideInInspector]
    public bool dashPressed;
    public bool slamPressed;
    
    void Update(){
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
        moveDirection.Normalize();
        if (Input.GetKeyDown(KeyCode.Space)) dashPressed = true;
        else dashPressed = false;

       if (Input.GetKeyDown(KeyCode.LeftShift)) slamPressed = true;
       else slamPressed = false;
    }
}
