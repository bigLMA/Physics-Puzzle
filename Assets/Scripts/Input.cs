using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;

    private void Start()
    {
        // Configure actions
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    /// <summary>
    /// Reads WASD for movement
    /// </summary>
    /// <returns>2D vector representing movement on ground</returns>
    public Vector2 GetMove()
    {
        return moveAction.ReadValue<Vector2>();
    }

    /// <summary>
    /// Reads mouse X and Y movement
    /// </summary>
    /// <returns>2D vector representing looking around</returns>
    public Vector2 GetLook()
    {
        return lookAction.ReadValue<Vector2>();
    }

    /// <summary>
    /// Return if space pressed
    /// </summary>
    /// <returns>Boolean representing that jump pressed</returns>
    public bool GetJump()
    {
        return jumpAction.IsPressed();
    }
}
