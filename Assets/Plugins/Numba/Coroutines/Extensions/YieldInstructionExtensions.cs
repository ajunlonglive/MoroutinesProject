using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Coroutines.Extensions
{
    public static class YieldInstructionExtensions
    {
        public static CustomYieldInstruction AsCustomYieldInstruction(this YieldInstruction instruction)
        {
            var coroutine = Coroutine.Run(Routines.Wait(instruction));
            return new WaitUntil(() => coroutine.IsCompleted);
        }
    }
}