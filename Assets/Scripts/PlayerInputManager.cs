using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [HideInInspector]
    public Vector2 moveDirection;

    [HideInInspector]
    public bool dashPressed;
    
    void Update(){
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
        moveDirection.Normalize();
        if (Input.GetKeyDown(KeyCode.Space)) dashPressed = true;
        else dashPressed = false;
    }
}
