using UnityEngine;

namespace Aventra.Game
{
    public interface IHoldable
    {
        Transform UseObject { get; }
        Transform HoldObject { get; set; }
        float Mass { get; }
        void OnHold();
        void OnRelease();
    }
}