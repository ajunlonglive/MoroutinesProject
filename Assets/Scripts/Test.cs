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
        for (int i = 0; i < 20; i++)
        {
            Moroutine.Run(_owner, TestEnumerator(1)).WaitForComplete();
            yield return new WaitForSeconds(0.2f);
        }

        //print("Awaited");
    }

    private IEnumerator TestEnumerator(int delay)
    {
        //print("Test 1 Started");
        yield return new WaitForSeconds(5f);
        //print($"Test 1 Finished {Time.time}");
        yield return null;
    }
}