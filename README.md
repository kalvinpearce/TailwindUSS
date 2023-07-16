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

This package supplies a `TailwindUSS.uss` file that can be added to any `.uxml`
or linked in any `.cs` script.

A few helpers have been supplied to assist with adding styles in bulk.
To use them, simply put `using TailwindUSS;` at the top of the script.

### `.AddClasses` strings

```csharp
using TailwindUSS;
// ...
VisualElement label = new Label("Hello World! From TailwindUSS");
label.AddClasses("text-2xl", "text-red-500", "m-4");

// Also supports a single string seperated by spaces
label.AddClasses("text-2xl text-red-500 m-4");
```

### `.AddClasses` enum

For better code editor support, the package generates an enum with every
supported tailwind class name.

```csharp
using TailwindUSS;
// ...
VisualElement label = new Label("Hello World! From TailwindUSS");
label.AddClasses(Tw.text_2xl, Tw.text_red_500, Tw.m_4);
```

### `.AddTailwindUSSStylesheet`

A quick helper to add the stylesheet via script.

```csharp
using TailwindUSS;
// ...
VisualElement root = new VisualElement();
root.AddTailwindUSSStylesheet();
```
