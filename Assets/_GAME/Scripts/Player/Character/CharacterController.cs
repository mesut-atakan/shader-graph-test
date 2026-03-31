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
        private float _targetXAxisClamp = 0.0f;
        private float _targetBodyYAngle = 0.0f;
        private float _xRotationVelocity;
        private float _yRotationVelocity;
        private float _currentHorizontalSpeed;
        private float _currentVerticalSpeed;

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
            Vector3 forward = _camera.transform.forward;
            Vector3 right = _camera.transform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            float targetSpeed = inputListener.IsSprint
                ? playerSettings.CharacterSprintSpeed
                : playerSettings.CharcterMoveSpeed;

            float targetH = inputListener.MoveValue.x * targetSpeed;
            float targetV = inputListener.MoveValue.y * targetSpeed;

            _currentHorizontalSpeed = Mathf.Lerp(_currentHorizontalSpeed, targetH, Time.deltaTime * playerSettings.HorizontalDeceleration);
            _currentVerticalSpeed   = Mathf.Lerp(_currentVerticalSpeed,   targetV, Time.deltaTime * playerSettings.VerticalDeceleration);

            if (Mathf.Abs(_currentHorizontalSpeed) < 0.001f) _currentHorizontalSpeed = 0f;
            if (Mathf.Abs(_currentVerticalSpeed)   < 0.001f) _currentVerticalSpeed   = 0f;

            Vector3 velocity = forward * _currentVerticalSpeed + right * _currentHorizontalSpeed;
            transform.position += velocity * Time.deltaTime;
        }

        private void Look()
        {
            float mouseMultiply = Time.deltaTime * playerSettings.MouseSensitivity;
            float mouseX = inputListener.LookValue.x * mouseMultiply;
            float mouseY = inputListener.LookValue.y * mouseMultiply;

            _targetXAxisClamp = Mathf.Clamp(
                _targetXAxisClamp - mouseY,
                playerSettings.CameraDownAxisClamp,
                playerSettings.CameraUpAxisClamp
            );
            _targetBodyYAngle += mouseX;

            _xAxisClamp = Mathf.SmoothDamp(_xAxisClamp, _targetXAxisClamp, ref _xRotationVelocity, playerSettings.CameraRotationSmoothTime);
            float smoothedBodyY = Mathf.SmoothDampAngle(characterBody.eulerAngles.y, _targetBodyYAngle, ref _yRotationVelocity, playerSettings.CameraRotationSmoothTime);

            _camera.transform.localRotation = Quaternion.Euler(_xAxisClamp, 0.0f, 0.0f);
            characterBody.rotation = Quaternion.Euler(0f, smoothedBodyY, 0f);
        }
    }
}