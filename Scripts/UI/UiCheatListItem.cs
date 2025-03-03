using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnityBlocks.Cheats.UI
{
    public class UiCheatListItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI buttonValue;
        [SerializeField] private Button button;
        public event Action OnClicked;
        public float Height => GetComponent<RectTransform>().sizeDelta.y;

        private void Start()
        {
            button.onClick.AddListener(() => OnClicked?.Invoke());
        }

        public void SetTitle(string value)
        {
            title.SetText(value);
        }

        public void SetValue(string value)
        {
            buttonValue.SetText(value);
        }

        public void SetCallback(Action callback)
        {
            OnClicked = callback;
        }

        public void ToHeader()
        {
            button.gameObject.SetActive(false);
            title.fontSize *= 1.4f;
        }
    }
}