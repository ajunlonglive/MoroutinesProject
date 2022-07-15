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
        var group = new MoroutinesGroup(Moroutine.Run(Test1Enumerator(), Test2Enumerator()));
        yield return group.Run().WaitForCompleted();
    }

    private IEnumerator Test1Enumerator()
    {
        yield return new WaitForSeconds(3f);
        print("Test 1");
    }

    private IEnumerator Test2Enumerator()
    {
        yield return new WaitForSeconds(5f);
        print("Test 2");
    }
}