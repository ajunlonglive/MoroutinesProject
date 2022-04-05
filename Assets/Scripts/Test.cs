using System.Collections;
using UnityEngine;
using Redcode.Moroutines;
using Redcode.Moroutines.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
    private IEnumerator Start()
    {
        var tickMor1 = Moroutine.Run(TickEnumerable("mor1", 1));
        var tickMor2 = Moroutine.Run(TickEnumerable("mor2", 2));

        yield return new WaitForAll(tickMor1, tickMor2);
        print("All awaited!");
    }

    private IEnumerable TickEnumerable(string prefix, int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(1f);
            print($"{prefix}: Tick!");
        }
    }
}