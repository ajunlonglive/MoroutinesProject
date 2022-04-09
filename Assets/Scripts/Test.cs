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
        Moroutine mor = null;
        mor = Moroutine.Run(TickEnumerable(10, () =>
        {
            if (mor.Owner == null)
                print("Unowned: Tick!");
            else
                print($"{mor.Owner.name}: Tick!");
        }));

        yield return new WaitForSeconds(3.5f);
        mor.SetOwner(this);

        yield return new WaitForSeconds(3f);
        mor.SetOwner(_owner);
    }

    private IEnumerable TickEnumerable(int count, Action action)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(1f);
            action();
        }
    }
}