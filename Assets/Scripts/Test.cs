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
        var mors = Moroutine.Run(this, DelayEnumerable(3f), DelayEnumerable(4f));
        yield return new WaitForAll(mors);
        mors.ForEach(m => m.Destroy());
    }

    private IEnumerable DelayEnumerable(float delay)
    {
        yield return new WaitForSeconds(delay);
        print($"Delay {delay} awaited. Time {Time.time}");
    }
}