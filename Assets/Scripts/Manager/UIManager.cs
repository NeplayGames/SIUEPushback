using System;
using System.Collections;
using System.Collections.Generic;
using SIUE.ControllerGames.Player;
using SIUE.ControllerGames.UIP;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SIUE.ControllerGames.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI totalPlayerText;
        [SerializeField] private TextMeshProUGUI PlayerLostMessageText;
        [SerializeField] private GameObject UI;
        private UIInputReader uIInputReader;
        private int totalPlayer;
        public event Action StartGameAction;

        const string startText = "Press triangle to start";
        
        void Start()
        {
            uIInputReader = new UIInputReader();
            uIInputReader.SelectPressed += StartGame;
        }

        private void StartGame()
        {
            print(totalPlayer);
            if (totalPlayer > 1)
            {
                uIInputReader.SelectPressed -= StartGame;
                StartGameAction?.Invoke();
                this.UI.SetActive(false);
            }
        }

        public void TotalPlayer(int totalPlayer)
        {
            this.totalPlayer = totalPlayer;
            totalPlayerText.text = $"Press X to join. \n The total number of player connected is {totalPlayer}.";
        }

        public void GameInfoMessage(string message)
        {
            PlayerLostMessageText.text = message;
        }

        public void EndGame(string message)
        {
            UI.SetActive(true);
            totalPlayerText.text = $"{message}\n Press triangle to restart";
            uIInputReader.SelectPressed += RestartGame;
        }

        private void RestartGame()
        {
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
            uIInputReader.SelectPressed -= RestartGame;
        }
    }

}
