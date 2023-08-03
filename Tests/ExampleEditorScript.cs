using UnityEditor;
using UnityEngine.UIElements;
using TailwindUSS;

public class ExampleEditorScript : EditorWindow
{
    [MenuItem("Window/TailwindUSS/Example")]
    public static void ShowExample()
    {
        var window = GetWindow<ExampleEditorScript>();
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        root.AddTailwindUSSStylesheet();

        VisualElement label = new Label("Hello World! From TailwindUSS");
        label.Tailwind("text-2xl text-red-500 m-4 transition-all hover:text-blue-500");

        root.Add(label);
    }
}
