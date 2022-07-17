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
    private GameObject _test1Owner;

    [SerializeField]
    private GameObject _test2Owner;

    private IEnumerator Start()
    {
        var mor1 = Moroutine.Run(Test1Enumerator());
        var mor2 = Moroutine.Run(Test2Enumerable());
        var group = new MoroutinesGroup(mor1, mor2).SetAutoDestroy(true);

        Moroutine.Run(WaitForEnumerable(group));

        yield return null;
    }

    private IEnumerable Test1Enumerator()
    {
        print("Test 1 Started");
        yield return new WaitForSeconds(2f);
        print($"Test 1 Finished {Time.time}");
    }

    private IEnumerable Test2Enumerable()
    {
        print("Test 2 Started");
        yield return new WaitForSeconds(3f);
        print("Test 2 Finished");
    }

    private IEnumerable WaitForEnumerable(MoroutinesGroup group)
    {
        yield return group.WaitForDestroy();

        print($"Destroyed {Time.time}");
    }
}