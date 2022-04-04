using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Redcode.Moroutines
{
    public sealed class MoroutinesController : MonoBehaviour
    {
        private List<Moroutine> _moroutines = new List<Moroutine>();

        public ReadOnlyCollection<Moroutine> Moroutines => _moroutines.AsReadOnly();

        private void OnDisable()
        {
            foreach (var moroutine in _moroutines)
                moroutine.OnControllerDeactivated();
        }

        internal void Add(Moroutine moroutine) => _moroutines.Add(moroutine);

        internal void Remove(Moroutine moroutine)
        {
            _moroutines.Remove(moroutine);

            if (_moroutines.Count == 0)
                Destroy(this);
        }
    }
}