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
        var mors = Moroutine.Run(this, DelayEnumerable(1f), DelayEnumerable(2f), DelayEnumerable(3f));

        yield return new WaitForAll(gameObject.GetMoroutines().Select(m => m.WaitForComplete()));
        print("All moroutines awaited");
    }

    private IEnumerable DelayEnumerable(float delay)
    {
        yield return new WaitForSeconds(delay);
        print($"Delay {delay} awaited. Time {Time.time}");
    }
}