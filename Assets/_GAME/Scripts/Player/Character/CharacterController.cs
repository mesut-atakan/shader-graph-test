using UnityEngine;

namespace Aventra.Game
{
    [DisallowMultipleComponent]
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private InputListener inputListener;
        [SerializeField] private PlayerSettings playerSettings;
        [SerializeField] private Transform characterBody;

        private Camera _camera;
        private float _xAxisClamp = 0.0f;

        private float GetSpeed
        {
            get
            {
                if (inputListener.MoveValue == Vector2.zero)
                    return 0f;

                if (inputListener.IsSprint)
                    return playerSettings.CharacterSprintSpeed;
                return playerSettings.CharcterMoveSpeed;
            }
        }

        void Awake()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            Move();
        }

        void LateUpdate()
        {
            Look();
        }

        private void Move()
        {
            Vector3 moveDirection = new Vector3(
                inputListener.MoveValue.x,
                0.0f,
                inputListener.MoveValue.y
            );

            moveDirection = moveDirection * Time.deltaTime * GetSpeed;
            transform.position += moveDirection;
        }

        private void Look()
        {
            float mouseMultiply = Time.deltaTime * playerSettings.MouseSensitivity;
            float mouseX = inputListener.LookValue.x * mouseMultiply;
            float mouseY = inputListener.LookValue.y * mouseMultiply;

            _xAxisClamp = Mathf.Clamp(
                _xAxisClamp - mouseY,
                playerSettings.CameraDownAxisClamp,
                playerSettings.CameraUpAxisClamp
            );

            _camera.transform.localRotation = Quaternion.Euler(_xAxisClamp, 0.0f, 0.0f);
            characterBody.Rotate(Vector3.up * mouseX);
        }
    }
}