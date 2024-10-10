using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    public event Action onInteractPressed;
    public event Action<Vector2> onMovePressed;

    public bool isInteractPressed { get; private set; }
    public bool isMovePressed => currentMoveInput.x == 0 && currentMoveInput.y == 0;
    public Vector2 currentMoveInput { get; private set; }

    private void OnInteract(InputValue value)
    {
        isInteractPressed = value.isPressed;
        if (isInteractPressed) {
            onInteractPressed?.Invoke();
        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        if (input == currentMoveInput) return;
        currentMoveInput = input;
        onMovePressed?.Invoke(currentMoveInput);
    }
}