using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction throwAction;
    private InputAction rotateGrabbedAction;

    private void Start()
    {
        // Configure actions
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        jumpAction = InputSystem.actions.FindAction("Jump");
        throwAction = InputSystem.actions.FindAction("Throw");
        rotateGrabbedAction = InputSystem.actions.FindAction("Rotate Grabbed Object");
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

    /// <summary>
    /// Return if interact button (LMB) is pressed down
    /// </summary>
    /// <returns>Boolean representing that interact button is pressed</returns>
    public bool GetInteractDown()
    {
        return UnityEngine.Input.GetButtonDown("Fire1");
    }

    /// <summary>
    /// Return if interact button (LMB) is pressed up
    /// </summary>
    /// <returns>Boolean representing that interact button is pressed up</returns>
    public bool GetInteractUp()
    {
        return UnityEngine.Input.GetButtonUp("Fire1");
    }

    public bool GetThrow()
    {
        return throwAction.IsPressed();
    }

    public Vector2 GetRotateGrabbed()
    {
        return rotateGrabbedAction.ReadValue<Vector2>();
    }
}
