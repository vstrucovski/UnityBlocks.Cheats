using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Debug = UnityEngine.Debug;

namespace UnityBlocks.Cheats
{
    public class CheatsService
    {
        private readonly IEnumerable<ICheatCommands> _cheatCommands;

        private readonly Dictionary<string, (string category, Action<object> action, object parameter)> _commands =
            new();

        public Dictionary<string, (string category, Action<object> action, object parameter)> Commands => _commands;

        public CheatsService(IEnumerable<ICheatCommands> cheatCommands)
        {
            _cheatCommands = cheatCommands;
            RegisterCheatCommandsFromAttributes();
        }

        private void RegisterCheatCommandsFromAttributes()
        {
            var stopwatch = Stopwatch.StartNew();
            Debug.Log("Starting command registration...");

            foreach (var instance in _cheatCommands)
            {
                var type = instance.GetType();
                var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(m => m.GetCustomAttributes(typeof(CheatCommandAttribute), false).Any());

                foreach (var method in methods)
                {
                    var attribute =
                        (CheatCommandAttribute) method.GetCustomAttributes(typeof(CheatCommandAttribute), false)
                            .First();
                    _commands[method.Name] = (attribute.Category,
                        _ => method.Invoke(instance, new[] {attribute.Parameter}), attribute.Parameter);
                }
            }

            stopwatch.Stop();
            Debug.Log($"Command registration completed in {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}