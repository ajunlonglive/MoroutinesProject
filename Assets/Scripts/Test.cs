using System.Collections;
using UnityEngine;
using Redcode.Moroutines;
using Redcode.Moroutines.Extensions;

public class Test : MonoBehaviour
{
    [SerializeField]
    private GameObject _owner;

    private IEnumerator Start()
    {
        var mor = Moroutine.Run(_owner, GenerateSomeResultEnumerable());

        yield return mor.WaitForComplete(); // ждем морутину.

        print(mor.LastResult); // выводим ее последний результат.
    }

    private IEnumerable GenerateSomeResultEnumerable()
    {
        yield return new WaitForSeconds(3f); // симулируем некий процесс..

        yield return "Hello from moroutine!"; // а это будет последним результатом морутины.
    }
}
