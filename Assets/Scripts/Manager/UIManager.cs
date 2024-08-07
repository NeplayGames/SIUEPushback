using System;
using System.Collections;
using System.Collections.Generic;
using SIUE.ControllerGames.Player;
using SIUE.ControllerGames.UIP;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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
            totalPlayerText.text = $"The total number of player connected is {totalPlayer}";
        }

        public void GameInfoMessage(string ePlayer)
        {
            PlayerLostMessageText.text = ePlayer;
        }

        public void EndGame(string ePlayer)
        {
            UI.SetActive(true);
            totalPlayerText.text = ePlayer;
        }
    }

}
