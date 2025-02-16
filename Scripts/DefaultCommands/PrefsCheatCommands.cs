using UnityEngine;

namespace UnityBlocks.Cheats.DefaultCommands
{
    [AddComponentMenu("Cheats/Player Prefs Cheats", 99)]
    public class PrefsCheatCommands : MonoBehaviour, ICheatCommands
    {
        [CheatCommand("All")]
        private void ResetSaves(object _)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("All player prefs are cleared. Please restart the application");
        }
    }
}