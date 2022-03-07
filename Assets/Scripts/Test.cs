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

        yield return mor.WaitForComplete(); // ���� ��������.

        print(mor.LastResult); // ������� �� ��������� ���������.
    }

    private IEnumerable GenerateSomeResultEnumerable()
    {
        yield return new WaitForSeconds(3f); // ���������� ����� �������..

        yield return "Hello from moroutine!"; // � ��� ����� ��������� ����������� ��������.
    }
}
