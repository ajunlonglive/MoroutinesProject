using UnityEngine;

namespace Moroutines.Extensions
{
    public static class YieldInstructionExtensions
    {
        public static Moroutine AsCoroutine(this YieldInstruction instruction) => Moroutine.Run(Routines.Wait(instruction));

        public static CustomYieldInstruction AsCustomYieldInstruction(this YieldInstruction instruction)
        {
            var coroutine = instruction.AsCoroutine();
            return new WaitUntil(() => coroutine.IsCompleted);
        }
    }
}