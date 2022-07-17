using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Redcode.Moroutines.Extensions
{
    public static class IEnumerableExtensions
    {
        public static MoroutinesGroup ToMoroutinesGroup(this IEnumerable<Moroutine> moroutines) => new(moroutines);
    }
}