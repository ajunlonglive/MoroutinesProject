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
        //yield return new WaitForSeconds(1f);
        
        var mor1 = Moroutine.Run(this, DelayEnumerable(1f));
        var mor2 = Moroutine.Run(this, DelayEnumerable(2f));
        var mor3 = Moroutine.Run(this, DelayEnumerable(3f));

        yield return new WaitForAny(mor1, mor2, mor3);
        
        print($"All moroutines awaited: {mor1.IsRunning}, {mor2.IsRunning}. Time {Time.time}");
    }

    private IEnumerable DelayEnumerable(float delay)
    {
        yield return new WaitForSeconds(delay);
        print($"Delay {delay} awaited. Time {Time.time}");
    }
}