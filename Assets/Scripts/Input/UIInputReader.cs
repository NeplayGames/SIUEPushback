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
        public event Action RestartPressed;
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

        public void OnStart(InputAction.CallbackContext context)
        {
            Debug.Log("Click");
            SelectPressed?.Invoke();
        }

        public void OnRestart(InputAction.CallbackContext context)
        {
            Debug.Log("Click");
            RestartPressed?.Invoke();
        }

        public void OnQuit(InputAction.CallbackContext context)
        {
            Application.Quit();
        }
    }

}
