using UnityEngine;

namespace UnityDevConsole.Controllers.Input
{
    public class UnityInput : IInput
    {
        public bool GetKey (KeyCode key) => UnityEngine.Input.GetKey(key);
        public bool GetKeyDown (KeyCode key) => UnityEngine.Input.GetKeyDown(key);
        public bool GetKeyUp (KeyCode key) => UnityEngine.Input.GetKeyUp(key);
        public bool AnyKey => UnityEngine.Input.anyKey;
    }
}
