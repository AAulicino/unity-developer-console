using System.Collections;
using UnityEngine;

namespace UnityDevConsole
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine (IEnumerator routine);
        void StopCoroutine (Coroutine routine);
    }
}
