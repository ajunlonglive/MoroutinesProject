using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Moroutines;

namespace Moroutines.Demo
{
    public class CreateMoroutine : MonoBehaviour
    {
        private void Start() => Moroutine.Run(TickEnumerable());

        private IEnumerable TickEnumerable()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                print("Tick!");
            }
        }



    }
}