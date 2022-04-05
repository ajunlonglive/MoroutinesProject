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
        Moroutine.Run(TimerEnumerable(), Routines.Delay(0.5f, TimerEnumerable()));
        var mors = Moroutine.GetUnownedMoroutines();

        yield return new WaitForSeconds(3f);
        mors.ForEach(m => m.Stop());
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