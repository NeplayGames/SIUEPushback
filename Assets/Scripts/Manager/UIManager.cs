using System;
using System.Collections;
using System.Collections.Generic;
using SIUE.ControllerGames.Input;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SIUE.ControllerGames.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI PlayerLostMessageText;
        [SerializeField] private GameObject UI;
        [SerializeField] private GameObject InGameUI;
        [SerializeField] private GameObject RestartUI;
        [SerializeField] private GameObject startImage;
        [SerializeField] private GameObject moveImage;
        [SerializeField] private List<GameObject> playerInfo = new List<GameObject>();

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
            if (totalPlayer > 1)
            {
                uIInputReader.SelectPressed -= StartGame;
                StartGameAction?.Invoke();
                this.UI.SetActive(false);
                InGameUI.SetActive(true);
            }
        }

        public void TotalPlayer(int totalPlayer)
        {
            playerInfo[this.totalPlayer].SetActive(true);
            this.totalPlayer = totalPlayer;
            moveImage.SetActive(true);
            if (totalPlayer > 1)
                startImage.SetActive(true);
        }

        public void GameInfoMessage(string message)
        {
            PlayerLostMessageText.text = message;
            StartCoroutine(StopShowingMessage());
        }

        IEnumerator StopShowingMessage()
        {
            yield return new WaitForSeconds(8);
            PlayerLostMessageText.text = string.Empty;
        }
        public void EndGame(string message)
        {
            GameInfoMessage(message);
            RestartUI.SetActive(true);
            InGameUI.SetActive(false);
            uIInputReader.SelectPressed += RestartGame;
        }

        private void RestartGame()
        {
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
            uIInputReader.SelectPressed -= RestartGame;
        }
    }

}
