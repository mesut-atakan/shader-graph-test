using Aventra.Game.Core;
using UnityEngine;

namespace Aventra.Game
{
    [CreateAssetMenu(fileName = FILE_NAME, menuName = MENU_NAME)]
    public sealed class CargoboxSettings : ScriptableObject
    {
        private const string FILE_NAME = nameof(CargoboxSettings);
        private const string MENU_NAME = Constants.COMPANY_NAME + "/" + Constants.GAME_NAME + "/" + FILE_NAME;

        [field:SerializeField] public float DefaultForceValue { get; private set; } = 5.0f;
    }
}