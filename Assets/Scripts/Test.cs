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
        var group = new MoroutinesGroup(Moroutine.Run(_test1Owner, Test1Enumerator(), Test2Enumerator()));

        yield return group.WaitForComplete();

        print("Awaited");
    }

    private IEnumerator Test1Enumerator()
    {
        yield return new WaitForSeconds(1f);
        print("Test 1");
    }

    private IEnumerator Test2Enumerator()
    {
        yield return new WaitForSeconds(2f);
        print("Test 2");
    }
}