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
        yield return new WaitForSeconds(3f);

        Moroutine.Create(_owner, TestEnumerator(3f));
        Moroutine.Run(_owner, TestEnumerator(3f)).Stop();
        
        for (int i = 0; i < 20; i++)
        {
            Moroutine.Run(_owner, TestEnumerator(5f)).SetName("TestEnumerator");
            yield return new WaitForSeconds(0.2f);
        }

        //print("Awaited");
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