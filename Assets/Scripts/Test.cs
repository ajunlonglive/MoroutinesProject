using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Coroutines.Extensions;

namespace Coroutines
{
	public class Test : MonoBehaviour
	{
        private Coroutine _cor;

        private void Start() => _cor = Coroutine.Run(PrintRoutine());

        private IEnumerator PrintRoutine()
        {
            var i = 0;
            while (true)
            {
                print(i++);

                if (i == 3)
                    _cor.Stop();

                yield return new WaitForSeconds(1f);
            }
        }
    }
}