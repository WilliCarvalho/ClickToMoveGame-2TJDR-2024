using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
   private PlayerControls playerControls;

   public event Action OnPlayerMove;

    public Vector2 MoveDirection =>
        playerControls.Gameplay.MovementControl.ReadValue<Vector2>();

    public InputManager()
   {
      playerControls = new PlayerControls();
      playerControls.Gameplay.Enable();

      playerControls.Gameplay.Movement.performed += OnMovementPerformed;
   }

   private void OnMovementPerformed(InputAction.CallbackContext obj)
   {
      OnPlayerMove?.Invoke();
   }
}
