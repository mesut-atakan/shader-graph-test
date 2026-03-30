using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Aventra.Game
{
    public sealed class InputListener : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActionsAssets;

        private InputActionMap _playerActionMap;
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _jumpAction;
        private InputAction _interactAction;

        public Vector2 MoveValue { get; private set; }
        public Vector2 LookValue { get; private set; }
        public Action OnJump { get; private set; }
        public Action OnInteract { get; private set; }

        void Awake()
        {
            _playerActionMap = inputActionsAssets.FindActionMap(nameOrId: "Player");
            _moveAction = _playerActionMap.FindAction("Move");
            _lookAction = _playerActionMap.FindAction("Look");
            _jumpAction = _playerActionMap.FindAction("Jump");
            _interactAction = _playerActionMap.FindAction("Interact");

            _moveAction.performed += OnMovePerformed;
            _lookAction.performed += OnLookPerformed;
            _jumpAction.performed += OnJumpPerformed;
            _interactAction.performed += OnInteractPerformed;

            _moveAction.canceled += OnMoveCanceled;
            _lookAction.canceled += OnLookCanceled;
        }

        void OnEnable()
        {
            _moveAction.Enable();
            _lookAction.Enable();
            _jumpAction.Enable();
            _interactAction.Enable();
        }

        void OnDisable()
        {
            _moveAction.Disable();
            _lookAction.Disable();
            _jumpAction.Disable();
            _interactAction.Disable();
        }

        void OnDestroy()
        {
            _moveAction.performed -= OnMovePerformed;
            _lookAction.performed -= OnLookPerformed;
            _jumpAction.performed -= OnJumpPerformed;
            _interactAction.performed -= OnInteractPerformed;

            _moveAction.canceled -= OnMoveCanceled;
            _lookAction.canceled -= OnLookCanceled;
        }

        private void OnInteractPerformed(InputAction.CallbackContext context)
        {
            Debug.Log($"Intearct");
            OnInteract?.Invoke();
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            Debug.Log("Jump");
            OnJump?.Invoke();
        }

        private void OnLookPerformed(InputAction.CallbackContext context)
        {
            LookValue = context.ReadValue<Vector2>();
            Debug.Log($"Look Value {LookValue}");
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            MoveValue = context.ReadValue<Vector2>().normalized;
            Debug.Log($"Move Value {MoveValue}");
        }

        private void OnLookCanceled(InputAction.CallbackContext context)
        {
            LookValue = Vector2.zero;
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            MoveValue = Vector2.zero;
        }
    }
}