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

    IEnumerator TestEnumerator()
    {
        yield return new WaitForSeconds(2);
        print("Enumerator completed!");
    }
    IEnumerator Start()
    {
        yield return Moroutine.Run(TestEnumerator()).WaitForComplete(); 
        print("Finish!");
    }
}