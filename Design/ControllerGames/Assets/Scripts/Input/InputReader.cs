using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SIUE.ControllerGames.Input
{
    public class InputReader : GameActions.IGameplayActions, IDisposable
    {
        private GameActions gameActions;

        public event Action<Vector2> moveAction;
        public InputReader()
        {
            gameActions = new GameActions();
            gameActions.Gameplay.AddCallbacks(this);
            gameActions.Gameplay.Enable();
        }

        public void Dispose()
        {
             gameActions.Gameplay.RemoveCallbacks(this);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            moveAction?.Invoke(context.ReadValue<Vector2>());
        }   
    }   
}