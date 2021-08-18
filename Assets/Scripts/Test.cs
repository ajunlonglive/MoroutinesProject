using System.Collections;
using UnityEngine;
using Coroutines;
using Coroutines.Extensions;
using Coroutine = Coroutines.Coroutine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        var delayCoroutine = Coroutine.Run(Routines.Delay(1f, () => print("Converting")));  // Создали корутину

        var yieldInstruction = delayCoroutine.WaitForComplete();                            // Получили YieldInstruction объект
        var customYieldInstruction = yieldInstruction.AsCustomYieldInstruction();           // YieldInstruction конвертировали в CustomYieldInstruction
        var coroutine = customYieldInstruction.AsCoroutine();                               // CustomYieldInstruction конвертировали в новый объект Coroutine
    }

    private IEnumerable WaitAndPrintEnumerator()
    {
        yield return new WaitForSeconds(1f);
        print("Hello!");
    }
}
