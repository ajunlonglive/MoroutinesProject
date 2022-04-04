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
        yield return null;
        var mors = Moroutine.Run(this, DelayEnumerable(1f)/*, DelayEnumerable(2f), DelayEnumerable(3f)*/);
        mors.OnDestroyed(m => print("Destroyed"));

        //yield return new WaitForAll(gameObject.GetMoroutines().Select(m => m.WaitForComplete()));
        //print("All moroutines awaited");
    }

    private IEnumerator DelayEnumerable(float delay)
    {
        yield return new WaitForSeconds(delay);
        print($"Delay {delay} awaited. Time {Time.time}");
    }
}