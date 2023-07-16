using UnityEngine.UIElements;
using System.Collections.Generic;

namespace TailwindUSS
{
    public static class Utils
    {
        internal const string PROJECT_PATH = "Packages/com.kalvinpearce.tailwinduss";

        private static TailwindColor[] _tailwindColors;
        internal static TailwindColor[] TailwindColors
        {
            get
            {
                if (_tailwindColors == null)
                {
                    _tailwindColors = GetTailwindColors();
                }
                return _tailwindColors;
            }
        }

        internal struct TailwindColor
        {
            public string name;
            public TailwindColorShade[] shades;
        }
        internal struct TailwindColorShade
        {
            public string value;
            public string hex;
        }

        private static TailwindColor[] GetTailwindColors()
        {
            var path = System.IO.Path.Combine(PROJECT_PATH, "Editor/tailwind-colors.json");
            var fullPath = System.IO.Path.GetFullPath(path);
            var json = System.IO.File.ReadAllText(fullPath);
            var colors = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);

            var tailwindColors = new List<TailwindColor>();

            foreach (var color in colors)
            {
                var shades = new List<TailwindColorShade>();

                foreach (var shade in color.Value)
                {
                    shades.Add(new TailwindColorShade
                    {
                        value = shade.Key,
                        hex = shade.Value,
                    });
                }

                var tailwindColor = new TailwindColor
                {
                    name = color.Key,
                    shades = shades.ToArray(),
                };
                tailwindColors.Add(tailwindColor);
            }

            return tailwindColors.ToArray();
        }

        internal static string ClassNameToTwEnum(string name)
        {
            return name
                .Split(' ')[0]
                .Replace("-", "_")
                .Replace(".", "dot")
                .Replace("/", "slsh");
        }

        public static void AddTailwindUSSStylesheet(this VisualElement self)
        {
            var path = System.IO.Path.Combine(PROJECT_PATH, "Runtime/tailwinduss.uss");
            self.styleSheets.Add(UnityEditor.AssetDatabase.LoadAssetAtPath<StyleSheet>(path));
        }
    }
}
