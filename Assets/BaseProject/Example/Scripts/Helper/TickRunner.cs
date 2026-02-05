using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BaseProject.Example.Scripts.Helper
{
    public class TickRunner : MonoBehaviour
    {
        private readonly List<ITickable> _tickables = new();

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Register(ITickable tickable)
        {
            if (_tickables.Contains(tickable))
                return;
            _tickables.Add(tickable);
        }

        public void Unregister(ITickable tickable)
        {
            _tickables.Remove(tickable);
        }

        private void Update()
        {
            for (int i = 0; i < _tickables.Count; i++)
            {
                _tickables[i].Tick();
            }
        }
    }
}