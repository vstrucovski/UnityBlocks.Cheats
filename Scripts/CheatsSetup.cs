using System.Collections.Generic;
using System.Linq;
using UnityBlocks.Cheats.UI;
using UnityEngine;

namespace UnityBlocks.Cheats
{
    public class CheatsSetup : MonoBehaviour
    {
        [SerializeField] private UiCheatPanel cheatPanel;
        [SerializeField] private List<MonoBehaviour> cheatCommands = new();

        private void Start()
        {
            if (cheatCommands.Count == 0) return;
            var manuallyAddedCommands = cheatCommands
                .OfType<ICheatCommands>()
                .ToList();
            var cheatsService = new CheatsService(manuallyAddedCommands);
            cheatPanel.Init(cheatsService);
        }
    }
}