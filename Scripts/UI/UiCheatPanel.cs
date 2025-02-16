using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace UnityBlocks.Cheats.UI
{
    public class UiCheatPanel : MonoBehaviour
    {
        [SerializeField] private Button toggleButton;
        [SerializeField] private GameObject panel;
        [SerializeField] private GameObject buttonsPanel;
        [SerializeField] private UiCheatListItem listPrefab;
        private CheatsService _cheatsService;

        public void Init(CheatsService cheatsService)
        {
            _cheatsService = cheatsService;
            Render();
        }

        private void Start()
        {
            toggleButton.onClick.AddListener(OnToggleClicked);
        }

        private void Render()
        {
            var groupedCommands = _cheatsService.Commands.GroupBy(c => c.Value.category);
            foreach (var category in groupedCommands)
            {
                DrawHeader(category);
                foreach (var command in category)
                {
                    DrawButton(command);
                }
            }

            DisableDefaultsUI();
        }

        private void DisableDefaultsUI()
        {
            listPrefab.gameObject.SetActive(false);
        }

        private void DrawHeader(
            IGrouping<string, KeyValuePair<string, (string category, Action<object> action, object parameter)>> command)
        {
            if (string.IsNullOrEmpty(command.Key)) return;
            var obj = Instantiate(listPrefab, buttonsPanel.transform);
            // obj.SetTitle($"\n■ {command.Key}");
            obj.SetTitle($"● {command.Key}");
            obj.ToHeader();
            obj.gameObject.SetActive(true);
        }

        private void DrawButton(
            KeyValuePair<string, (string category, Action<object> action, object parameter)> command)
        {
            var obj = Instantiate(listPrefab, buttonsPanel.transform);
            obj.SetTitle("\t" + FormatCamelCase(command.Key));
            obj.SetValue(command.Value.parameter?.ToString() ?? "null");
            obj.SetCallback(() => command.Value.action.Invoke(command.Value.parameter));
            obj.gameObject.SetActive(true);
        }

        private void OnToggleClicked()
        {
            panel.SetActive(!panel.gameObject.activeSelf);
        }

        private static string FormatCamelCase(string input)
        {
            return Regex.Replace(input, "([a-z])([A-Z])", "$1 $2");
        }
    }
}