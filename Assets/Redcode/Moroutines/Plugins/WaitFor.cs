using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Redcode.Moroutines
{
    /// <summary>
    /// Base class for awaiting multiple objects with <see langword="yiled"/> instruction.
    /// </summary>
    internal abstract class WaitFor : CustomYieldInstruction
    {
        protected readonly IEnumerable<IEnumerator> _instructions;

        /// <summary>
        /// Create object which will waiting moroutines.
        /// </summary>
        /// <param name="moroutines">Target moroutines.</param>
        public WaitFor(params Moroutine[] moroutines) : this(moroutines.Select(m => m.WaitForComplete())) { }

        /// <summary>
        /// Create object which will waiting moroutines.
        /// </summary>
        /// <param name="moroutines">Target moroutines.</param>
        public WaitFor(IList<Moroutine> moroutines) : this(moroutines.Select(m => m.WaitForComplete())) { }

        /// <summary>
        /// Create object which will waiting <see cref="IEnumerator"/> instructions.
        /// </summary>
        /// <param name="instructions">Target instructions.</param>
        public WaitFor(params IEnumerator[] instructions) : this((IEnumerable<IEnumerator>)instructions) { }

        /// <summary>
        /// Create object which will waiting <see cref="IEnumerable"/><![CDATA[<]]><see cref="IEnumerator"/><![CDATA[>]]> instructions.
        /// </summary>
        /// <param name="instructions">Target instructions.</param>
        public WaitFor(IEnumerable<IEnumerator> instructions) => _instructions = instructions;
    }
}