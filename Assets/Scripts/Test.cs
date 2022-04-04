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
        yield return mor.WaitForDestroy();

        print("OK");
        //mor.Reset();
        //mor.Run();
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