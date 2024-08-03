using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SIUE.ControllerGames.Input
{
    public class InputReader : GameActions.IGameplayActions, IDisposable
    {
        private InputAction moveInputAction;
        private InputAction rotateInputAction;
        private InputAction shootInputAction;
        public event Action<Vector2> moveAction;
        public event Action<Vector2> rotateAction;
        public event Action<float> shootAction;
        public InputReader(PlayerInput playerInput)
        {
            moveInputAction = playerInput.actions["Move"];
            rotateInputAction = playerInput.actions["Rotate"];
            shootInputAction = playerInput.actions["Shoot"];
            moveInputAction.performed += OnMove;
            moveInputAction.canceled += OnMoveCancel;
            rotateInputAction.canceled += OnRotateCancel;
            rotateInputAction.performed += OnRotate;
            shootInputAction.canceled += OnShootCancel;
            shootInputAction.performed += OnShoot;
        }

        private void OnRotateCancel(InputAction.CallbackContext context)
        {
        }

        private void OnShootCancel(InputAction.CallbackContext context)
        {
        }

        private void OnMoveCancel(InputAction.CallbackContext context)
        {
           moveAction?.Invoke(Vector2.zero);
        }

        public void Dispose()
        {
            moveInputAction.performed -= OnMove;
            rotateInputAction.performed -= OnRotate;
            shootInputAction.performed -= OnShoot;
              moveInputAction.canceled -= OnMoveCancel;
            rotateInputAction.canceled -= OnRotateCancel;
            shootInputAction.canceled -= OnShootCancel;
        }

        public void OnMove(InputAction.CallbackContext context) =>
            moveAction?.Invoke(context.ReadValue<Vector2>());

        public void OnRotate(InputAction.CallbackContext context) =>
            rotateAction?.Invoke(context.ReadValue<Vector2>());

        public void OnShoot(InputAction.CallbackContext context) =>
            shootAction?.Invoke(context.ReadValue<float>());

    }
}