using System;

namespace Aventra.Game.Common
{
    public class Event
    {
        private event Action _listeners;

        public bool HasListeners => _listeners is not null;

        public Event AddListener(Action action)
        {
            _listeners += action;
            return this;
        }

        public Event RemoveListener(Action action)
        {
            _listeners -= action;
            return this;
        }

        public Event RemoveAllListeners()
        {
            _listeners = null;
            return this;
        }

        public void Invoke()
        {
            _listeners?.Invoke();
        }
    }
}