using System.Collections;
using UnityEngine;
using Redcode.Moroutines;
using Redcode.Moroutines.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
    [SerializeField]
    private GameObject _owner;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);

        var w1 = new WaitForSecondsRealtime(1f);
        var w2 = new WaitForSecondsRealtime(2f);
        var w3 = new WaitForSecondsRealtime(3f);

        //yield return w1;
        //yield return w2;
        //yield return w3;
        //while (w1.MoveNext() || w2.MoveNext() || w3.MoveNext())
        //    yield return null;

        yield return new WaitForAll(w1, w2, w3);
        print($"Awaited {Time.time}");
    }

    private IEnumerable TestEnumerator(float delay)
    {
        //print("Test 1 Started");
        yield return new WaitForSeconds(delay);
        //print($"Test 1 Finished {Time.time}");
        //print("Finished");
        yield return UnityEngine.Random.value;
    }
}