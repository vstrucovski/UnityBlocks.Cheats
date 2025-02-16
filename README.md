# 🛠️ Unity Cheats Panel

This module allows you to easily add custom methods to a panel, 
primarily for creating in-game cheats and debugging tools without 
needing to write custom scripts or inspector buttons. 
It is designed to be highly extendable, fast, and easy to integrate into any Unity project.

## 🚀 Features
- **Quick Integration**: Simply drag and drop the prefab to your scene.
- **Custom Commands**: Add custom cheat buttons with ease using simple attributes.
- **Modular & Extendable**: Add new cheat buttons without modifying existing code.
- **Developer Friendly**: Quickly test and debug without modifying the inspector.

![cheats_1.png](Documentation%2Fcheats_1.png)

---

## 📦 How to install
1. Require Git for installing package.
2. Open Unity Package Manager (Window > Package Manager).
3. Add package via git URL
```
https://github.com/vstrucovski/UnityBlocks.Cheats.git
```
OR add directly to manifest
```
"unityblocks.cheats": "https://github.com/vstrucovski/UnityBlocks.Cheats.git"
```

## 🕹️ How to Use
#### 1. Add Cheats Panel to Scene
Simply drag the Cheats prefab from the package into your game scene.
The panel will automatically populate with buttons from any ICheatCommands components in the scene.

#### 2. Create Custom Cheat Commands
To add new buttons to the cheat panel, create a new MonoBehaviour and implement the ICheatCommands interface.
```csharp
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
```
_Example: Resetting Player Preferences_

#### 📝 Important Notes:
Cheat Commands must be decorated with the [CheatCommand] attribute.

The attribute accepts two parameters:
- Command Value (required)
- Category: Groups commands into categories (optional)

#### 3. Register Commands: Ensure all command components are added to the CheatSetup object in the scene.
![cheats_2.png](Documentation%2Fcheats_2.png)

## #TODO
- auto hide panel for production builds
- add shortcuts and option to toggle panel without ui button