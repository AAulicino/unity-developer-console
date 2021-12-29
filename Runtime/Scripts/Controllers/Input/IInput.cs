using UnityEngine;

namespace UnityDevConsole.Controllers.Input
{
    public interface IInput
    {
        bool AnyKey { get; }

        bool GetKey (KeyCode key);
        bool GetKeyDown (KeyCode key);
        bool GetKeyUp (KeyCode key);
    }
}
