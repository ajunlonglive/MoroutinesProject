using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Redcode.Moroutines.Extensions
{
    public static class GameObjectExtensions
    {
        public static List<Moroutine> GetMoroutines(this GameObject gameObject) => GetMoroutines(gameObject, (Moroutine.State)byte.MaxValue);

        public static List<Moroutine> GetMoroutines(this GameObject gameObject, Moroutine.State mask)
        {
            return new List<Moroutine>(gameObject.GetComponent<MoroutinesController>().Moroutines.Where(m => (m.CurrentState & mask) != 0));
        }
    }
}