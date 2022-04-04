using System.Collections;
using UnityEngine;
using Redcode.Moroutines;
using Redcode.Moroutines.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
    private void Start()
    {
        var mor = Moroutine.Run(TimerEnumerable());
        yield return new WaitForSeconds(3f);
        mor.Reset();
        mor.Run();
    }

    private IEnumerable TimerEnumerable()
    {
        var seconds = 0;

        while (true)
        {
            yield return new WaitForSeconds(1f);
            print(++seconds);
        }
    }
}