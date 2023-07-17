using UnityEngine;
using UnityEngine.UIElements;

namespace TailwindUSS
{
    public static class VisualElementExtentions
    {
        private const string DEBUG_PREFIX = "<color=#3b82f6><b>TailwindUSS</b></color>";

        private static void Log(string message)
        {
            Debug.Log($"{DEBUG_PREFIX} {message}");
        }

        private static void LogWarning(string message)
        {
            Debug.LogWarning($"{DEBUG_PREFIX} {message}");
        }

        private static void LogError(string message)
        {
            Debug.LogError($"{DEBUG_PREFIX} {message}");
        }

#if UNITY_EDITOR
        private const string STYLESHEET_PATH = "Packages/com.kalvinpearce.tailwinduss/Runtime";
        public static void AddTailwindUSSStylesheet(this VisualElement self)
        {
            var path = $"{STYLESHEET_PATH}/TailwindUSS.Normal.uss";
            var stylesheet = UnityEditor.AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
            if (!self.styleSheets.Contains(stylesheet))
            {
                self.styleSheets.Add(stylesheet);
            }
        }

        public static void AddTailwindUSSStylesheetHover(this VisualElement self)
        {
            var path = $"{STYLESHEET_PATH}/TailwindUSS.Hover.uss";
            var stylesheet = UnityEditor.AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
            if (!self.styleSheets.Contains(stylesheet))
            {
                self.styleSheets.Add(stylesheet);
            }
        }

        public static void AddTailwindUSSStylesheetActive(this VisualElement self)
        {
            var path = $"{STYLESHEET_PATH}/TailwindUSS.Active.uss";
            var stylesheet = UnityEditor.AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
            if (!self.styleSheets.Contains(stylesheet))
            {
                self.styleSheets.Add(stylesheet);
            }
        }

        public static void AddTailwindUSSStylesheetFocus(this VisualElement self)
        {
            var path = $"{STYLESHEET_PATH}/TailwindUSS.Focus.uss";
            var stylesheet = UnityEditor.AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
            if (!self.styleSheets.Contains(stylesheet))
            {
                self.styleSheets.Add(stylesheet);
            }
        }

        public static void AddTailwindUSSStylesheetDisabled(this VisualElement self)
        {
            var path = $"{STYLESHEET_PATH}/TailwindUSS.Disabled.uss";
            var stylesheet = UnityEditor.AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
            if (!self.styleSheets.Contains(stylesheet))
            {
                self.styleSheets.Add(stylesheet);
            }
        }

        public static void AddTailwindUSSStylesheetEnabled(this VisualElement self)
        {
            var path = $"{STYLESHEET_PATH}/TailwindUSS.Enabled.uss";
            var stylesheet = UnityEditor.AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
            if (!self.styleSheets.Contains(stylesheet))
            {
                self.styleSheets.Add(stylesheet);
            }
        }

        public static void AddTailwindUSSStylesheetChecked(this VisualElement self)
        {
            var path = $"{STYLESHEET_PATH}/TailwindUSS.Checked.uss";
            var stylesheet = UnityEditor.AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
            if (!self.styleSheets.Contains(stylesheet))
            {
                self.styleSheets.Add(stylesheet);
            }
        }
#endif

        public static void Tailwind(this VisualElement self, string classes)
        {
            var splitClasses = classes.Split(' ');
            foreach (var tailwindClass in splitClasses)
            {
                var selector = PseudoSelector.None;
                string className = tailwindClass;

                // Parse pseudo
                if (tailwindClass.Contains(":"))
                {
                    var split = tailwindClass.Split(":");
                    selector = ParseTailwindPseudoSelector(split[0]);
                    className = split[1];

                    if (selector == PseudoSelector.Unsupported)
                    {
                        continue;
                    }
                }

#if UNITY_EDITOR
                // Ensure stylesheet is applied
                switch (selector)
                {
                    case PseudoSelector.None:
                        self.AddTailwindUSSStylesheet();
                        break;
                    case PseudoSelector.Hover:
                        self.AddTailwindUSSStylesheetHover();
                        break;
                    case PseudoSelector.Active:
                        self.AddTailwindUSSStylesheetActive();
                        break;
                    case PseudoSelector.Focus:
                        self.AddTailwindUSSStylesheetFocus();
                        break;
                    case PseudoSelector.Disabled:
                        self.AddTailwindUSSStylesheetDisabled();
                        break;
                    case PseudoSelector.Enabled:
                        self.AddTailwindUSSStylesheetEnabled();
                        break;
                    case PseudoSelector.Checked:
                        self.AddTailwindUSSStylesheetChecked();
                        break;
                }
#endif

                // Add class
                switch (selector)
                {
                    case PseudoSelector.None:
                        self.AddToClassList(className);
                        break;
                    case PseudoSelector.Hover:
                        self.AddToClassList("hover_" + className);
                        break;
                    case PseudoSelector.Active:
                        self.AddToClassList("active_" + className);
                        break;
                    case PseudoSelector.Focus:
                        self.AddToClassList("focus_" + className);
                        break;
                    case PseudoSelector.Disabled:
                        self.AddToClassList("disabled_" + className);
                        break;
                    case PseudoSelector.Enabled:
                        self.AddToClassList("enabled_" + className);
                        break;
                    case PseudoSelector.Checked:
                        self.AddToClassList("checked_" + className);
                        break;
                }
            }
        }

        private enum PseudoSelector
        {
            None,
            Unsupported,
            Hover,
            Active,
            Focus,
            Disabled,
            Enabled,
            Checked
        }

        private static PseudoSelector ParseTailwindPseudoSelector(string selector)
        {
            PseudoSelector pseudo = selector switch
            {
                "hover" => PseudoSelector.Hover,
                "active" => PseudoSelector.Active,
                "focus" => PseudoSelector.Focus,
                "disabled" => PseudoSelector.Disabled,
                "enabled" => PseudoSelector.Enabled,
                "checked" => PseudoSelector.Checked,
                _ => PseudoSelector.Unsupported
            };

            if (pseudo == PseudoSelector.Unsupported)
            {
                LogWarning($"Selector \"{selector}\" is not supported.");
            }

            return pseudo;
        }

    }
}
