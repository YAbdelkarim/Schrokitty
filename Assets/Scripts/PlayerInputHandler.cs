using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandler : MonoBehaviour
{
    private InputAction _moveAction, _jumpAction;
    private CharacterController2D _characterController;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _jumpAction = InputSystem.actions.FindAction("Jump");

        _jumpAction.performed += Jump;

        _characterController = GetComponent<CharacterController2D>();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        _characterController.Jump();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector = _moveAction.ReadValue<Vector2>();
        _characterController.Move(moveVector);
    }
}
