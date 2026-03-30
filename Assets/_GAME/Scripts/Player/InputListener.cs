using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Aventra.Game
{
    public sealed class InputListener : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActionAsset;

        private InputActionMap _playerActionMap;
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _jumpAction;
        private InputAction _interactAction;
        private InputAction _sprintAction;

        public Vector2 MoveValue { get; private set; }
        public Vector2 LookValue { get; private set; }
        public bool IsSprint { get; set; }
        public Action OnJump { get; set; }
        public Action OnInteract { get; set; }

        void Awake()
        {
            _playerActionMap = inputActionAsset.FindActionMap("Player");
            _moveAction = _playerActionMap.FindAction("Move");
            _lookAction = _playerActionMap.FindAction("Look");
            _jumpAction = _playerActionMap.FindAction("Jump");
            _interactAction = _playerActionMap.FindAction("Interact");
            _sprintAction = _playerActionMap.FindAction("Sprint");

            _moveAction.performed += OnMovePerformed;
            _moveAction.canceled += OnMoveCanceled;

            _lookAction.performed += OnLookPerformed;
            _lookAction.canceled += OnLookCanceled;

            _jumpAction.performed += OnJumpPerformed;

            _interactAction.performed += OnInteractPerformed;

            _sprintAction.performed += OnSprintPerformed;
            _sprintAction.canceled += OnSprintCanceled;
        }

        void OnEnable()
        {
            _moveAction.Enable();
            _lookAction.Enable();
            _jumpAction.Enable();
            _interactAction.Enable();
            _sprintAction.Enable();
        }

        void OnDisable()
        {
            _moveAction.Disable();
            _lookAction.Disable();
            _jumpAction.Disable();
            _interactAction.Disable();
            _sprintAction.Disable();
        }

        void OnDestroy()
        {
            _moveAction.performed -= OnMovePerformed;
            _moveAction.canceled -= OnMoveCanceled;

            _lookAction.performed -= OnLookPerformed;
            _lookAction.canceled -= OnLookCanceled;

            _jumpAction.performed -= OnJumpPerformed;

            _interactAction.performed -= OnInteractPerformed;

            _sprintAction.performed -= OnSprintPerformed;
            _sprintAction.canceled -= OnSprintCanceled;
        }

        private void OnSprintCanceled(InputAction.CallbackContext context)
        {
            IsSprint = false;
        }

        private void OnSprintPerformed(InputAction.CallbackContext context)
        {
            IsSprint = true;
        }

        private void OnInteractPerformed(InputAction.CallbackContext context)
        {
            // Debug.Log("Interact");
            OnInteract?.Invoke();
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            // Debug.Log("Jump");
            OnJump?.Invoke();
        }

        private void OnLookCanceled(InputAction.CallbackContext context)
        {
            LookValue = Vector2.zero;
            // Debug.Log($"Look Value {LookValue}");
        }

        private void OnLookPerformed(InputAction.CallbackContext context)
        {
            LookValue = context.ReadValue<Vector2>();
            // Debug.Log($"Look Value {LookValue}");
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            MoveValue = Vector2.zero;
            // Debug.Log($"Move Value {MoveValue}");
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            MoveValue = context.ReadValue<Vector2>().normalized;
            // Debug.Log($"Move Value {MoveValue}");
        }
    }
}