<p align="center">
  <img width="180" height="180" src="Documentation~/TailwindUSS.png">
</p>

# TailwindUSS

Tailwind for Unity UI Toolkit.

## Install

Install using Unitys built-in package manager, UPM.

`Unity Package Manager` -> `Install Package from git URL...`

`https://github.com/kalvinpearce/TailwindUSS.git`

## Usage

This package supplies a `TailwindUSS.<pseudo>.uss` file that can be added to
any `.uxml` or linked in any `.cs` script.

A few helpers have been supplied to assist with adding styles in bulk.
To use them, simply put `using TailwindUSS;` at the top of the script.

### `.Tailwind`

It takes a string of tailwind classes, separated by spaces. It will automatically
apply the relevant stylesheets when used in editor scripts, otherwise they need
to be added manually in the uxml.

```csharp
using TailwindUSS;
// ...
VisualElement label = new Label("Hello World! From TailwindUSS");
label.Tailwind("text-2xl text-red-500 m-4 transition-all hover:text-blue-500");
```

### Editor Integration

For a better editor experience, please use the official Tailwind plugin and
follow the instructions below.

#### Vscode

1. Install the [official extension](https://marketplace.visualstudio.com/items?itemName=bradlc.vscode-tailwindcss)
2. Navigate to the tailwind settings
3. In the tailwindCSS.includeLanguages setting add a new item for csharp
    - Search `tailwindCSS.includeLanguages`
    - Click `Add Item`
    - Key: `csharp` & Value: `html`
4. Add class regex for `Tailwind("...")`
    - Search `tailwindCSS.experimental.classRegex`
    - Click `Edit in settings.json`
    - Paste `"Tailwind\\(\"([^\"]*)"`
5. Ensure a tailwind.config.js exists in the project root
    - Run `npx tailwindcss init` in a terminal in the project root
    - Add `"Assets/**/*.cs"` to the content array in `tailwind.config.js`
