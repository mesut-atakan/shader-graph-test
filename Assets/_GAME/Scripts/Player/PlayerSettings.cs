using UnityEngine;
using Aventra.Game.Core;

namespace Aventra.Game
{
    [CreateAssetMenu(fileName = FILE_NAME, menuName = MENU_NAME)]
    public sealed class PlayerSettings : ScriptableObject
    {
        public const string FILE_NAME = nameof(PlayerSettings);
        public const string MENU_NAME = Constants.COMPANY_NAME + "/" + Constants.GAME_NAME + "/" + FILE_NAME;

        [Header("Player Settings")]
        [field: SerializeField] public float MouseSensitivity { get; private set; } = 5.0f;
        [field: SerializeField] public bool InverMouseY { get; private set; } = false;
        [field: SerializeField] public bool InverMouseX { get; private set; } = false;
        
        [Header("Character Settings")]
        [field: SerializeField] public float CharcterMoveSpeed { get; private set; } = 4.0f;
        [field: SerializeField] public float CharacterSprintSpeed { get; private set; } = 7.0f;
        [field: SerializeField] public float MinMoveSpeed { get; private set; } = 1.0f;
        [field: SerializeField] public float MinSprintSpeed { get; private set; } = 3.0f;
        [field: SerializeField] public float CharacterJumpForce { get; private set; } = 4.0f;
        [field: SerializeField] public float HorizontalDeceleration { get; private set; } = 5.0f;
        [field: SerializeField] public float VerticalDeceleration { get; private set; } = 5.0f;

        [Header("FPS Camera Controller")]
        [field: SerializeField] public float CameraUpAxisClamp { get; private set; } = 90.0f;
        [field: SerializeField] public float CameraDownAxisClamp { get; private set; } = -90.0f;
        [field: SerializeField] public float CameraRotationSmoothTime { get; private set; } = 0.1f;

        [Header("Character Interact Controller")]
        [field: SerializeField] public float InteractRange { get; private set; } = 3.0f;
        [field: SerializeField] public float HoldObjectDistance { get; private set; } = 2.0f;
        [field: SerializeField] public float HoldObjectTargetDelta { get; private set; } = 0.25f;
    }
}