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
        var del = new Func<IEnumerator>(TestEnumerator);
        print(del.Method.Name);
        StartCoroutine(TestEnumerator());

        Moroutine.Run(TestEnumerator());
        Moroutine.Run(this, TestEnumerator());
        Moroutine.Run(gameObject, TestEnumerator());
        
        yield return null;
    }

    private IEnumerator TestEnumerator()
    {
        print("Test 1 Started");
        yield return new WaitForSeconds(2f);
        print($"Test 1 Finished {Time.time}");
    }
}