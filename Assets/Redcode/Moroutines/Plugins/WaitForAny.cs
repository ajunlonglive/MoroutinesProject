using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Redcode.Moroutines
{
    /// <summary>
    /// Allows you to wait for completing at least one of multiple objects (<see cref="Moroutine"/>, <see cref="CustomYieldInstruction"/>, <see cref="IEnumerable"/>, <see cref="IEnumerator"/> and other..).<br/>
    /// Don't use with <see cref="WaitForSecondsRealtime"/> object.
    /// </summary>
    public class WaitForAny : WaitFor
    {
        /// <summary>
        /// Is it need to keep waiting for the object?
        /// </summary>
        public override bool keepWaiting => _instructions.All(m => m.MoveNext());

        /// <summary>
        /// <inheritdoc cref="WaitFor(Moroutine[])"/>
        /// </summary>
        /// <param name="moroutines"><inheritdoc cref="WaitFor(Moroutine[])"/></param>
        public WaitForAny(params Moroutine[] moroutines) : this(moroutines.Select(m => m.WaitForComplete())) { }

        /// <summary>
        /// Create object which will waiting moroutines.
        /// </summary>
        /// <param name="moroutines">Target moroutines.</param>
        public WaitForAny(List<Moroutine> moroutines) : this(moroutines.Select(m => m.WaitForComplete())) { }

        /// <summary>
        /// <inheritdoc cref="WaitFor(IEnumerator[])"/>
        /// </summary>
        /// <param name="instructions"><inheritdoc cref="WaitFor(IEnumerator[])"/></param>
        public WaitForAny(params IEnumerator[] instructions) : this((IEnumerable<IEnumerator>)instructions) { }

        /// <summary>
        /// <inheritdoc cref="WaitFor(IEnumerable{IEnumerator})"/>
        /// </summary>
        /// <param name="instructions"><inheritdoc cref="WaitFor(IEnumerable{IEnumerator})"/></param>
        public WaitForAny(IEnumerable<IEnumerator> instructions) : base(instructions) { }
    }
}