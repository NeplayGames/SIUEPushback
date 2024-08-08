using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SIUE.ControllerGames.Input
{
    public class UIInputReader : GameActions.IUIActions, IDisposable
    {
        GameActions.UIActions inputAction;
        public event Action SelectPressed;
        public UIInputReader()
        {
            GameActions gameAction = new GameActions();
            inputAction = new GameActions.UIActions(gameAction);
            inputAction.AddCallbacks(this);
            inputAction.Enable();
        }

        public void Dispose()
        {
            inputAction.RemoveCallbacks(this);
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            Debug.Log("Click");
            SelectPressed?.Invoke();
        }

        public void OnSelect(InputAction.CallbackContext context)
        {
            Debug.Log("Click");
        }
    }

}
