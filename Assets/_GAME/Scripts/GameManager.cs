using UnityEngine;

namespace Aventra.Game
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerSettings playerSettings;

        void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Application.targetFrameRate = -1;
        }
    }
}