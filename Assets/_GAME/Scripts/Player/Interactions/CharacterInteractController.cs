using System;
using NUnit.Framework;
using UnityEngine;

namespace Aventra.Game
{
    public sealed class CharacterInteractController : MonoBehaviour
    {
        [SerializeField] private PlayerSettings playerSettings;
        [SerializeField] private InputListener InputListener;

        private Camera _camera;
        private Vector2 _screenCenter => new Vector2(Screen.width / 2f, Screen.height / 2f);

        public IHoldable LookingHoldable { get; private set; }
        public IHoldable HoldingHoldable { get; private set; }


        void Awake()
        {
            _camera = Camera.main;
        }

        void OnEnable()
        {
            InputListener.OnInteract += Interact;
            InputListener.OnRelease += Release;
        }

        void OnDisable()
        {
            InputListener.OnInteract -= Interact;
            InputListener.OnRelease -= Release;
        }

        void Update()
        {
            HoldItemVisual();
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
                    if (HoldingHoldable != null && HoldingHoldable == LookingHoldable)
                        return;

                    LookingHoldable = holdable;
                }
                else
                {
                    LookingHoldable = null;
                }
            }
            else if (LookingHoldable != null)
            {
                LookingHoldable = null;
            }
        }

        private void Interact()
        {
            if (LookingHoldable == null)
                return;

            if (HoldingHoldable != null)
            {
                HoldingHoldable.OnRelease();
                HoldingHoldable = null;
            }



            HoldingHoldable = LookingHoldable;
            HoldingHoldable.HoldObject = _camera.transform;
            HoldingHoldable.OnHold();
            LookingHoldable = null;
            HoldItemVisual();
        }

        private void Release()
        {
            if (HoldingHoldable == null)
                return;

            HoldingHoldable.OnRelease();
            HoldingHoldable = null;
        }

        private void HoldItemVisual()
        {
            if (HoldingHoldable == null)
                return;

            HoldingHoldable.UseObject.position = Vector3.Lerp(
                 HoldingHoldable.UseObject.position,
                _camera.transform.position + _camera.transform.forward * playerSettings.HoldObjectDistance,
                 playerSettings.HoldObjectTargetDelta * Time.deltaTime
              );
        }

#if UNITY_EDITOR

        [ExecuteInEditMode]
        void OnDrawGizmos()
        {
            if (_camera == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(_camera.ScreenPointToRay(_screenCenter).origin, _camera.ScreenPointToRay(_screenCenter).direction * playerSettings.InteractRange);
        }

#endif
    }
}