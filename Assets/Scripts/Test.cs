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
        var mor = Moroutine.Run(TimerEnumerable());
        mor.OnDestroyed(m => print("OK"));
        yield return new WaitForSeconds(3.5f);

        //mor.Reset();
        mor.Run();
    }

    private IEnumerator TimerEnumerable()
    {
        var seconds = 0;

        //while (true)
        //{
            yield return new WaitForSeconds(1f);
            print(++seconds);
        //}
    }
}