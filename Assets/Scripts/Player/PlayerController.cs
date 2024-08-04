using System;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private bool isControllable;
        private Vector3 playerMovement;
        private InputReader inputReader;

        private Vector3 direction;
        //Temp
        private int rotationSpeed = 720;
        public float throwPower = 10f; // The power of the throw
        public float throwDuration = 1f; // Duration of the throw in seconds
        private Vector3 startPosition;
        private Vector3 targetPosition;
        private float throwTimer;
        private ThrowableItems pickedThrowableItem;
        private bool isThrowing;
        public void GotHit(Vector3 direction, float distance)
        {
            startPosition = transform.position;
            targetPosition = startPosition + direction.normalized * distance;
            throwTimer = 0f;
            isThrowing = true;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if(collision.gameObject.TryGetComponent(out ThrowableItems throwableItems))
            {
                if(throwableItems.shoot){
                    GotHit(throwableItems.Direction, throwableItems.distance);
                }
                if(pickedThrowableItem != null) return;
                this.pickedThrowableItem = throwableItems;
                throwableItems.GotPicked(this.transform);
            }
        }
        public void SetInputReader(InputReader inputReader)
        {
            this.inputReader = inputReader;
            this.inputReader.moveAction += MovePlayer;
            this.inputReader.shootAction += Shoot;
        }

        private void Shoot(float obj)
        {
            if(pickedThrowableItem == null) return;
            pickedThrowableItem.OnThrow(direction, transform.position);
            pickedThrowableItem = null;
        }

        private void MovePlayer(Vector2 vector)
        {
            playerMovement = new Vector3(vector.x, 0, vector.y);
            playerMovement *= Time.deltaTime;
        }

        void OnDestroy()
        {
            //ToDo
            //Remove
            if (!isControllable) return;
            this.inputReader.moveAction -= MovePlayer;
            this.inputReader.shootAction -= Shoot;
        }

        void Update()
        {
            ThrowItem();
            if (playerMovement.sqrMagnitude == 0 || !isControllable || isThrowing)
                return;
            this.characterController.Move(playerMovement * 40);
            RotatePlayerTowardsInputDirection();
        }

        private void ThrowItem()
        {
            if (isThrowing)
            {
                throwTimer += Time.deltaTime;
                float progress = throwTimer / throwDuration;
                transform.position = Vector3.Lerp(startPosition, targetPosition, Mathf.SmoothStep(0f, 1f, progress));
                if (progress >= 1f)
                    isThrowing = false;
            }
        }

        private void RotatePlayerTowardsInputDirection()
        {
            direction = playerMovement.normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed
            );
        }
        void OnValidate()
        {
            Assert.IsNotNull(this.characterController, "The player component Required Character Controller");
        }
    }
}
