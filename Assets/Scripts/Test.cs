using System.Collections;
using UnityEngine;
using Moroutines;
using Moroutines.Extensions;

public class Test : MonoBehaviour
{
    [SerializeField]
    private GameObject _owner;

    private IEnumerator Start()
    {
        var cor = Moroutine.Run(_owner, WaitAndPrintEnumerator());

        yield return new WaitForSeconds(3.5f);

        _owner.SetActive(false);

        yield return new WaitForSeconds(3f);

        _owner.SetActive(true);
        cor.Run();
    }

    private IEnumerable WaitAndPrintEnumerator()
    {
        var counter = 0;

        while (true)
        {
            print(counter++);
            yield return new WaitForSeconds(1f);
        }
    }
}
