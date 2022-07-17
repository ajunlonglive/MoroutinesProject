using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Settings", menuName = "Moroutines/Settings", order = 1)]
internal class Settings : ScriptableObject
{
    [SerializeField]
    private bool _hideMoroutinesExecuterFromScene;

    public bool HideMoroutinesExecuterFromScene => _hideMoroutinesExecuterFromScene;
}
