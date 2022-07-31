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
        var group = Moroutine.Create(TickEnumerator(1), TickEnumerator(3)).ToMoroutinesGroup();
        Moroutine.Run(WaitEnumerator(group));

        yield return new WaitForSeconds(2f);

        group.Run();
    }

    private IEnumerable TickEnumerator(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(1f);
            print(i);
        }
    }

    private IEnumerable WaitEnumerator(MoroutinesGroup group)
    {
        yield return group.WaitForRun();
        print($"Run awaited! (Time.time = {Time.time})");
    }
}