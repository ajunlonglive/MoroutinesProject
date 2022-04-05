using System.Collections;
using UnityEngine;
using Redcode.Moroutines;
using Redcode.Moroutines.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
    //using Redcode.Moroutines.Extensions;

    // ...

    private IEnumerator Start()
    {
        Moroutine.Run(this, TickEnumerable(1), TickEnumerable(2));
        
        var mors = gameObject.GetMoroutines();
        yield return new WaitForAll(mors);

        print("All awaited!");
    }

    private IEnumerable TickEnumerable(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(1f);
            print("Tick!");
        }
    }
}