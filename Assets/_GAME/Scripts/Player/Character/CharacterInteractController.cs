using NUnit.Framework;
using UnityEngine;

namespace Aventra.Game
{
    public sealed class CharacterInteractController : MonoBehaviour
    {
        [SerializeField] private PlayerSettings playerSettings;
        [SerializeField] private InputListener InputListener;

        private Camera _camera;
        private IHoldable _lookingHoldable;
        private IHoldable _holdingHoldable;
        private Vector2 _screenCenter => new Vector2(Screen.width / 2f, Screen.height / 2f);


        void Awake()
        {
            _camera = Camera.main;
        }

        void OnEnable()
        {
            InputListener.OnInteract += Interact;
            InputListener.OnInteract += Release;
        }

        void OnDisable()
        {
            InputListener.OnInteract -= Interact;
            InputListener.OnInteract -= Release;
        }

        void FixedUpdate()
        {
            UpdateInteractableObject();
        }

        private void UpdateInteractableObject()
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(_screenCenter), out RaycastHit hit, playerSettings.InteractRange))
            {
                Debug.Log("Hit: " + hit.collider.name);
                if (hit.collider.TryGetComponent(out IHoldable holdable))
                {
                    if (_holdingHoldable != null && _holdingHoldable == _lookingHoldable)
                        return;

                    _lookingHoldable = holdable;
                }
                else
                {
                    _lookingHoldable = null;
                }
            }
        }

        private void Interact()
        {
            if (_lookingHoldable == null)
                return;

            _holdingHoldable.OnHold();
            _holdingHoldable = _lookingHoldable;
            _lookingHoldable = null;
        }

        private void Release()
        {
            if (_holdingHoldable == null)
                return;

            _holdingHoldable.OnRelease();
            _holdingHoldable = null;
        }

#if UNITY_EDITOR

        [ExecuteInEditMode]
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_camera.ScreenPointToRay(_screenCenter).origin, _camera.ScreenPointToRay(_screenCenter).direction * playerSettings.InteractRange);
        }

#endif
    }
}