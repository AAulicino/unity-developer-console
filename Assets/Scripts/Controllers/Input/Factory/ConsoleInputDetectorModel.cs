namespace UnityDevConsole.Controllers.Input
{
    public static class ConsoleInputDetectorModelFactory
    {
        public static ConsoleInputDetectorModel Create (ICoroutineRunner runner)
        {
            return new ConsoleInputDetectorModel(runner, new UnityInput());
        }
    }
}
