using System.Collections;
using UnityEngine;
using Coroutines;
using Coroutines.Extensions;
using Coroutine = Coroutines.Coroutine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        var delayCoroutine = Coroutine.Run(Routines.Delay(1f, () => print("Converting")));  // ������� ��������

        var yieldInstruction = delayCoroutine.WaitForComplete();                            // �������� YieldInstruction ������
        var customYieldInstruction = yieldInstruction.AsCustomYieldInstruction();           // YieldInstruction �������������� � CustomYieldInstruction
        var coroutine = customYieldInstruction.AsCoroutine();                               // CustomYieldInstruction �������������� � ����� ������ Coroutine
    }

    private IEnumerable WaitAndPrintEnumerator()
    {
        yield return new WaitForSeconds(1f);
        print("Hello!");
    }
}
