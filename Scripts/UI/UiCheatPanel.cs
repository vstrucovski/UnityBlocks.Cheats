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
        [SerializeField] private RectTransform content;
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
            UiCheatListItem lastButton;
            int count = 1;
            foreach (var category in groupedCommands)
            {
                count += 1;
                DrawHeader(category);
                foreach (var command in category)
                {
                    count += 1;
                    lastButton = DrawButton(command);
                }
            }

            var size = content.sizeDelta;
            size.y = listPrefab.Height * count + 60f;
            content.sizeDelta = size;
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

        private UiCheatListItem DrawButton(
            KeyValuePair<string, (string category, Action<object> action, object parameter)> command)
        {
            var obj = Instantiate(listPrefab, buttonsPanel.transform);
            obj.SetTitle("\t" + FormatCamelCase(command.Key));
            obj.SetValue(command.Value.parameter?.ToString() ?? "null");
            obj.SetCallback(() => command.Value.action.Invoke(command.Value.parameter));
            obj.gameObject.SetActive(true);
            return obj;
        }

        private void OnToggleClicked()
        {
            content.gameObject.SetActive(!content.gameObject.activeSelf);
        }

        private static string FormatCamelCase(string input)
        {
            return Regex.Replace(input, "([a-z])([A-Z])", "$1 $2");
        }
    }
}