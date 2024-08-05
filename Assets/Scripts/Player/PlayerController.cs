using System;
using System.Collections;
using System.Collections.Generic;
using SIUE.ControllerGames.Configs;
using SIUE.ControllerGames.Input;
using SIUE.ControllerGames.Throwables;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace SIUE.ControllerGames.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] public bool isControllable;
        private Vector3 playerMovement;
        private InputReader inputReader;
        private PlayerConfig playerConfig;
        private Vector3 direction;
        //Temp
        private int rotationSpeed = 10;
        public float throwPower = 10f; // The power of the throw
        public float throwDuration = 1f; // Duration of the throw in seconds
        private Vector3 startPosition;
        private Vector3 targetPosition;
        private float throwTimer;
        private ThrowableItems pickedThrowableItem;
        private bool isPushedBack;
        public void GotHit(Vector3 direction, float distance)
        {
            startPosition = transform.position;
            targetPosition = startPosition + direction.normalized * distance;
            throwTimer = 0f;
            isPushedBack = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out ThrowableItems throwableItems))
            {
                if (throwableItems.shoot)
                {
                    GotHit(throwableItems.Direction, throwableItems.distance);
                    throwableItems.Remove();
                    return;
                }
                if (pickedThrowableItem != null) return;
                this.pickedThrowableItem = throwableItems;
                throwableItems.GotPicked(this.transform);
            }
        }
        public void SetInputReader(InputReader inputReader, PlayerConfig playerConfig)
        {
            this.playerConfig = playerConfig;
            this.inputReader = inputReader;
            this.inputReader.moveAction += MovePlayer;
            this.inputReader.shootAction += Shoot;
        }

        public bool IsHit
        {
            get
            {
                return isPushedBack;
            }
        }

        private void Shoot(float obj)
        {
            if (pickedThrowableItem == null) return;
            pickedThrowableItem.OnThrow(direction, transform.position + transform.forward * 3);
            pickedThrowableItem = null;
        }

        private void MovePlayer(Vector2 vector)
        {
            playerMovement = new Vector3(vector.x, 0, vector.y);
        }

        void OnDestroy()
        {
            if (!isControllable) return;
            this.inputReader.moveAction -= MovePlayer;
            this.inputReader.shootAction -= Shoot;
        }

        void Update()
        {
            PushedPlayerBack();
            if (playerMovement.sqrMagnitude == 0 || !isControllable || isPushedBack)
                return;
            RotateAndMovePlayer();
        }

        private void PushedPlayerBack()
        {
            if (isPushedBack)
            {
                characterController.enabled = false;
                throwTimer += Time.deltaTime;
                float progress = throwTimer / throwDuration;
                transform.position = Vector3.Lerp(startPosition, targetPosition, Mathf.SmoothStep(0f, 1f, progress));
                if (progress >= 1f)
                {
                    isPushedBack = false;
                    characterController.enabled = true;
                }
            }
        }

        private void RotateAndMovePlayer()
        {
            print(playerMovement.magnitude);
            if(playerMovement.magnitude < .2f) return;
            direction = playerMovement.normalized;      
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed
            );
            this.characterController.Move(playerMovement * Time.deltaTime *  playerConfig.playerSpeed);
        }
        void OnValidate()
        {
            Assert.IsNotNull(this.characterController, "The player component Required Character Controller");
        }
    }
}
