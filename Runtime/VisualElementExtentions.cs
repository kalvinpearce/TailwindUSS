using UnityEngine.UIElements;

namespace TailwindUSS
{
    public static class VisualElementExtentions
    {
        public static string TwEnumToClassName(string name)
        {
            return name
                .Replace("_", "-")
                .Replace("dot", ".")
                .Replace("slsh", "/");
        }

        public static void AddClasses(this VisualElement self, params Tw[] tws)
        {
            foreach (var tw in tws)
            {
                self.AddToClassList(TwEnumToClassName(tw.ToString()));
            }
        }

        public static void AddClasses(this VisualElement self, params string[] classes)
        {
            foreach (var c in classes)
            {
                self.AddToClassList(c);
            }
        }

        public static void AddClasses(this VisualElement self, string classes)
        {
            var split = classes.Split(" ");
            foreach (var c in split)
            {
                self.AddToClassList(c);
            }
        }
    }
}
