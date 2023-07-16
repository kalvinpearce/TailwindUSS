using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TailwindUSS
{
    public static class TailwindUSSGenerator
    {
        private struct Prop
        {
            public string name;
            public string css;
        }

        [UnityEditor.MenuItem("Assets/TailwindUSS/Generate")]
        public static void Generate()
        {
            var ussFilePath = Path.Combine(Utils.PROJECT_PATH, "Runtime/TailwindUSS.uss");
            var enumPath = Path.Combine(Utils.PROJECT_PATH, "Runtime/TailwindClassesEnum.cs");

            // Ensure directory exists
            var directory = Path.GetDirectoryName(ussFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var allProps = new List<Prop>();

            /* === Layout ===*/
            allProps.AddRange(GetDisplayProps());
            allProps.AddRange(GetOverflowProps());
            allProps.AddRange(GetPositionProps());
            allProps.AddRange(GetInsTRBLProps());
            allProps.AddRange(GetVisibilityProps());

            /* === Spacing ===*/
            allProps.AddRange(GetPaddingProps());
            allProps.AddRange(GetMarginProps());
            allProps.AddRange(GetSpaceProps());

            /* === Backgrounds ===*/
            allProps.AddRange(GetBackgroundProps());

            /* === Transforms ===*/
            allProps.AddRange(GetTransformProps());
            allProps.AddRange(GetScaleProps());
            allProps.AddRange(GetRotateProps());
            allProps.AddRange(GetTranslateProps());
            allProps.AddRange(GetTransformOriginProps());

            /* === Effects ===*/
            allProps.AddRange(GetOpacityProps());

            /* === Flexbox ===*/
            allProps.AddRange(GetFlexDirectionProps());
            allProps.AddRange(GetFlexWrapProps());
            allProps.AddRange(GetFlexProps());
            allProps.AddRange(GetFlexGrowShrinkProps());

            /* === Sizing ===*/
            allProps.AddRange(GetWidthHeightProps());

            /* === Borders ===*/
            allProps.AddRange(GetBorderRadiusProps());
            allProps.AddRange(GetBorderWidthProps());
            allProps.AddRange(GetColors("border", "border-color"));

            /* === Transitions ===*/
            allProps.AddRange(GetTransitionProps());
            allProps.AddRange(GetTransitionDurationProps());
            allProps.AddRange(GetTransitionTimingFnProps());
            allProps.AddRange(GetTransitionDelayProps());

            /* === Interactivity ===*/
            allProps.AddRange(GetCursorProps());

            /* === Box Alignment ===*/
            allProps.AddRange(GetJustifyProps());
            allProps.AddRange(GetAlignProps());

            /* === Typography ===*/
            allProps.AddRange(GetColors("text", "color"));
            allProps.AddRange(GetFontSizeProps());
            allProps.AddRange(GetTextWhiteSpaceProps());

            using StreamWriter ussWriter = new StreamWriter(ussFilePath, false);
            ussWriter.WriteLine("/*");
            ussWriter.WriteLine(" vim: filetype=css");
            ussWriter.WriteLine(" This file is auto-generated. Do not edit it directly.");
            ussWriter.WriteLine("*/");

            using StreamWriter enumWriter = new StreamWriter(enumPath, false);
            enumWriter.WriteLine("// This file is auto-generated. Do not edit it directly.");
            enumWriter.WriteLine("namespace TailwindUSS");
            enumWriter.WriteLine("{");
            enumWriter.WriteLine("    public enum Tw");
            enumWriter.WriteLine("    {");


            // Normal
            foreach (var prop in allProps)
            {
                ussWriter.WriteLine($".{prop.name} {{ {prop.css} }}");
                var enumName = Utils.ClassNameToTwEnum(prop.name);
                enumWriter.WriteLine($"        {enumName},");
            }
            // Hover
            foreach (var prop in allProps)
            {
                ussWriter.WriteLine($".hover-{prop.name}:hover {{ {prop.css} }}");
                var enumName = Utils.ClassNameToTwEnum($"hover-{prop.name}");
                enumWriter.WriteLine($"        {enumName},");
            }
            // Focus
            foreach (var prop in allProps)
            {
                ussWriter.WriteLine($".focus-{prop.name}:focus {{ {prop.css} }}");
                var enumName = Utils.ClassNameToTwEnum($"focus-{prop.name}");
                enumWriter.WriteLine($"        {enumName},");
            }
            // Active
            foreach (var prop in allProps)
            {
                ussWriter.WriteLine($".active-{prop.name}:active {{ {prop.css} }}");
                var enumName = Utils.ClassNameToTwEnum($"active-{prop.name}");
                enumWriter.WriteLine($"        {enumName},");
            }
            //Inactive
            foreach (var prop in allProps)
            {
                ussWriter.WriteLine($".inactive-{prop.name}:inactive {{ {prop.css} }}");
                var enumName = Utils.ClassNameToTwEnum($"inactive-{prop.name}");
                enumWriter.WriteLine($"        {enumName},");
            }
            // Disabled
            foreach (var prop in allProps)
            {
                ussWriter.WriteLine($".disabled-{prop.name}:disabled {{ {prop.css} }}");
                var enumName = Utils.ClassNameToTwEnum($"disabled-{prop.name}");
                enumWriter.WriteLine($"        {enumName},");
            }
            // Checked
            foreach (var prop in allProps)
            {
                ussWriter.WriteLine($".checked-{prop.name}:checked {{ {prop.css} }}");
                var enumName = Utils.ClassNameToTwEnum($"checked-{prop.name}");
                enumWriter.WriteLine($"        {enumName},");
            }

            enumWriter.WriteLine("    }");
            enumWriter.WriteLine("}");
        }


        /* === Layout ===*/
        private static List<Prop> GetDisplayProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "flex", css = "display: flex;" });
            props.Add(new Prop { name = "hidden", css = "display: none;" });
            return props;
        }

        private static List<Prop> GetOverflowProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "overflow-hidden", css = "overflow: hidden;" });
            props.Add(new Prop { name = "overflow-visible", css = "overflow: visible;" });
            props.Add(new Prop { name = "overflow-scroll", css = "overflow: scroll;" });
            return props;
        }

        private static List<Prop> GetPositionProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "absolute", css = "position: absolute;" });
            props.Add(new Prop { name = "relative", css = "position: relative;" });
            return props;
        }

        private static List<Prop> GetInsTRBLProps()
        {
            var props = new List<Prop>();
            props.AddRange(GetSizes("inset", new string[] { "top", "right", "bottom", "left" }, true));
            props.Add(new Prop { name = "inset-auto", css = "top: auto; right: auto; bottom: auto; left: auto;" });
            props.AddRange(GetSizesFrac("inset", new string[] { "top", "right", "bottom", "left" }, true));
            props.AddRange(GetSizes("inset-x", new string[] { "left", "right" }, true));
            props.Add(new Prop { name = "inset-x-auto", css = "left: auto; right: auto;" });
            props.AddRange(GetSizesFrac("inset-x", new string[] { "left", "right" }, true));
            props.AddRange(GetSizes("inset-y", new string[] { "top", "bottom" }, true));
            props.Add(new Prop { name = "inset-y-auto", css = "top: auto; bottom: auto;" });
            props.AddRange(GetSizesFrac("inset-y", new string[] { "top", "bottom" }, true));
            props.AddRange(GetSizes("top", "top", true));
            props.Add(new Prop { name = "top-auto", css = "top: auto;" });
            props.AddRange(GetSizesFrac("top", "top", true));
            props.AddRange(GetSizes("right", "right", true));
            props.Add(new Prop { name = "right-auto", css = "right: auto;" });
            props.AddRange(GetSizesFrac("right", "right", true));
            props.AddRange(GetSizes("bottom", "bottom", true));
            props.Add(new Prop { name = "bottom-auto", css = "bottom: auto;" });
            props.AddRange(GetSizesFrac("bottom", "bottom", true));
            props.AddRange(GetSizes("left", "left", true));
            props.Add(new Prop { name = "left-auto", css = "left: auto;" });
            props.AddRange(GetSizesFrac("left", "left", true));
            return props;
        }

        private static List<Prop> GetVisibilityProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "visible", css = "visibility: visible;" });
            props.Add(new Prop { name = "invisible", css = "visibility: hidden;" });
            return props;
        }


        /* === Spacing ===*/
        private static List<Prop> GetPaddingProps()
        {
            var props = new List<Prop>();
            props.AddRange(GetSizes("p", "padding"));
            props.AddRange(GetSizes("px", new string[] { "padding-left", "padding-right" }));
            props.AddRange(GetSizes("py", new string[] { "padding-top", "padding-bottom" }));
            props.AddRange(GetSizes("pt", "padding-top"));
            props.AddRange(GetSizes("pr", "padding-right"));
            props.AddRange(GetSizes("pb", "padding-bottom"));
            props.AddRange(GetSizes("pl", "padding-left"));
            return props;
        }

        private static List<Prop> GetMarginProps()
        {
            var props = new List<Prop>();
            props.AddRange(GetSizes("m", "margin", true));
            props.Add(new Prop { name = "m-auto", css = "margin: auto;" });
            props.AddRange(GetSizes("mx", new string[] { "margin-left", "margin-right" }, true));
            props.Add(new Prop { name = "mx-auto", css = "margin-left: auto; margin-right: auto;" });
            props.AddRange(GetSizes("my", new string[] { "margin-top", "margin-bottom" }, true));
            props.Add(new Prop { name = "my-auto", css = "margin-top: auto; margin-bottom: auto;" });
            props.AddRange(GetSizes("mt", "margin-top", true));
            props.Add(new Prop { name = "mt-auto", css = "margin-top: auto;" });
            props.AddRange(GetSizes("mr", "margin-right", true));
            props.Add(new Prop { name = "mr-auto", css = "margin-right: auto;" });
            props.AddRange(GetSizes("mb", "margin-bottom", true));
            props.Add(new Prop { name = "mb-auto", css = "margin-bottom: auto;" });
            props.AddRange(GetSizes("ml", "margin-left", true));
            props.Add(new Prop { name = "ml-auto", css = "margin-left: auto;" });
            return props;
        }

        private static List<Prop> GetSpaceProps()
        {
            var props = new List<Prop>();
            props.AddRange(GetSizes("space-x", new string[] { "margin-left", "margin-right" }, true, " > *"));
            props.AddRange(GetSizes("space-y", new string[] { "margin-top", "margin-bottom" }, true, " > *"));
            return props;
        }

        /* === Background ===*/
        private static List<Prop> GetBackgroundProps()
        {
            var props = new List<Prop>();
            props.AddRange(GetColors("bg", "background-color"));
            return props;
        }

        /* === Transforms ===*/
        private static List<Prop> GetScaleProps()
        {
            var props = new List<Prop>();
            props.AddRange(GetScales("scale", new string[] { "--tw-scale-x", "--tw-scale-y" }));
            props.AddRange(GetScales("scale-x", "--tw-scale-x"));
            props.AddRange(GetScales("scale-y", "--tw-scale-y"));
            return props;
        }

        private static List<Prop> GetRotateProps()
        {
            var props = new List<Prop>();
            props.AddRange(GetScales("rotate", "--tw-rotate"));
            return props;
        }

        private static List<Prop> GetTranslateProps()
        {
            var props = new List<Prop>();
            props.AddRange(GetSizes("translate-x", "--tw-translate-x", true));
            props.AddRange(GetSizesFrac("translate-x", "--tw-translate-x", true));
            props.AddRange(GetSizes("translate-y", "--tw-translate-y", true));
            props.AddRange(GetSizesFrac("translate-y", "--tw-translate-y", true));
            return props;
        }

        private static List<Prop> GetTransformOriginProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "origin-center", css = "transform-origin: center;" });
            props.Add(new Prop { name = "origin-top", css = "transform-origin: top;" });
            props.Add(new Prop { name = "origin-top-right", css = "transform-origin: top right;" });
            props.Add(new Prop { name = "origin-right", css = "transform-origin: right;" });
            props.Add(new Prop { name = "origin-bottom-right", css = "transform-origin: bottom right;" });
            props.Add(new Prop { name = "origin-bottom", css = "transform-origin: bottom;" });
            props.Add(new Prop { name = "origin-bottom-left", css = "transform-origin: bottom left;" });
            props.Add(new Prop { name = "origin-left", css = "transform-origin: left;" });
            props.Add(new Prop { name = "origin-top-left", css = "transform-origin: top left;" });
            return props;
        }

        private static List<Prop> GetTransformProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "transform", css = @"
                    --tw-translate-x: 0;
                    --tw-translate-y: 0;
                    --tw-rotate: 0;
                    --tw-scale-x: 1;
                    --tw-scale-y: 1;
                    translate: var(--tw-translate-x) var(--tw-translate-y); 
                    rotate: var(--tw-rotate);
                    scale: var(--tw-scale-x) var(--tw-scale-y);" });
            return props;
        }

        /* === Effects ===*/
        private static List<Prop> GetOpacityProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "opacity-0", css = "opacity: 0;" });
            props.Add(new Prop { name = "opacity-5", css = "opacity: 0.05;" });
            props.Add(new Prop { name = "opacity-10", css = "opacity: 0.1;" });
            props.Add(new Prop { name = "opacity-20", css = "opacity: 0.2;" });
            props.Add(new Prop { name = "opacity-25", css = "opacity: 0.25;" });
            props.Add(new Prop { name = "opacity-30", css = "opacity: 0.3;" });
            props.Add(new Prop { name = "opacity-40", css = "opacity: 0.4;" });
            props.Add(new Prop { name = "opacity-50", css = "opacity: 0.5;" });
            props.Add(new Prop { name = "opacity-60", css = "opacity: 0.6;" });
            props.Add(new Prop { name = "opacity-70", css = "opacity: 0.7;" });
            props.Add(new Prop { name = "opacity-75", css = "opacity: 0.75;" });
            props.Add(new Prop { name = "opacity-80", css = "opacity: 0.8;" });
            props.Add(new Prop { name = "opacity-90", css = "opacity: 0.9;" });
            props.Add(new Prop { name = "opacity-95", css = "opacity: 0.95;" });
            props.Add(new Prop { name = "opacity-100", css = "opacity: 1;" });
            return props;
        }

        /* === Flexbox ===*/
        private static List<Prop> GetFlexDirectionProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "flex-row", css = "flex-direction: row;" });
            props.Add(new Prop { name = "flex-row-reverse", css = "flex-direction: row-reverse;" });
            props.Add(new Prop { name = "flex-col", css = "flex-direction: column;" });
            props.Add(new Prop { name = "flex-col-reverse", css = "flex-direction: column-reverse;" });
            return props;
        }

        private static List<Prop> GetFlexWrapProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "flex-wrap", css = "flex-wrap: wrap;" });
            props.Add(new Prop { name = "flex-wrap-reverse", css = "flex-wrap: wrap-reverse;" });
            props.Add(new Prop { name = "flex-nowrap", css = "flex-wrap: nowrap;" });
            return props;
        }

        private static List<Prop> GetFlexProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "flex-1", css = "flex: 1 1 0%;" });
            props.Add(new Prop { name = "flex-auto", css = "flex: 1 1 auto;" });
            props.Add(new Prop { name = "flex-initial", css = "flex: 0 1 auto;" });
            props.Add(new Prop { name = "flex-none", css = "flex: none;" });
            return props;
        }

        private static List<Prop> GetFlexGrowShrinkProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "flex-grow", css = "flex-grow: 1;" });
            props.Add(new Prop { name = "flex-grow-0", css = "flex-grow: 0;" });
            props.Add(new Prop { name = "flex-shrink", css = "flex-shrink: 1;" });
            props.Add(new Prop { name = "flex-shrink-0", css = "flex-shrink: 0;" });
            return props;
        }

        /* === Sizing ===*/
        private static List<Prop> GetWidthHeightProps()
        {
            var props = new List<Prop>();
            props.AddRange(GetSizes("w", "width"));
            props.Add(new Prop { name = "w-auto", css = "width: auto;" });
            props.AddRange(GetSizesFrac("w", "width"));
            props.AddRange(GetSizesFracExtra("w", "width"));
            props.Add(new Prop { name = "min-w-0", css = "min-width: 0px;" });
            props.Add(new Prop { name = "min-w-full", css = "min-width: 100%;" });
            props.Add(new Prop { name = "max-w-0", css = "max-width: 0px;" });
            props.Add(new Prop { name = "max-w-none", css = "max-width: none;" });
            props.Add(new Prop { name = "max-w-xs", css = "max-width: 320px;" });
            props.Add(new Prop { name = "max-w-sm", css = "max-width: 384px;" });
            props.Add(new Prop { name = "max-w-md", css = "max-width: 448px;" });
            props.Add(new Prop { name = "max-w-lg", css = "max-width: 512px;" });
            props.Add(new Prop { name = "max-w-xl", css = "max-width: 576px;" });
            props.Add(new Prop { name = "max-w-2xl", css = "max-width: 672px;" });
            props.Add(new Prop { name = "max-w-3xl", css = "max-width: 768px;" });
            props.Add(new Prop { name = "max-w-4xl", css = "max-width: 896px;" });
            props.Add(new Prop { name = "max-w-5xl", css = "max-width: 1024px;" });
            props.Add(new Prop { name = "max-w-6xl", css = "max-width: 1152px;" });
            props.Add(new Prop { name = "max-w-7xl", css = "max-width: 1280px;" });
            props.Add(new Prop { name = "max-w-full", css = "max-width: 100%;" });

            props.AddRange(GetSizes("h", "height"));
            props.Add(new Prop { name = "h-auto", css = "height: auto;" });
            props.AddRange(GetSizesFrac("h", "height"));
            props.AddRange(GetSizesFracExtra("h", "height"));
            props.Add(new Prop { name = "min-h-0", css = "min-height: 0px;" });
            props.Add(new Prop { name = "min-h-full", css = "min-height: 100%;" });
            props.Add(new Prop { name = "max-h-0", css = "max-height: 0px;" });
            props.Add(new Prop { name = "max-h-none", css = "max-height: none;" });
            props.Add(new Prop { name = "max-h-xs", css = "max-height: 320px;" });
            props.Add(new Prop { name = "max-h-sm", css = "max-height: 384px;" });
            props.Add(new Prop { name = "max-h-md", css = "max-height: 448px;" });
            props.Add(new Prop { name = "max-h-lg", css = "max-height: 512px;" });
            props.Add(new Prop { name = "max-h-xl", css = "max-height: 576px;" });
            props.Add(new Prop { name = "max-h-2xl", css = "max-height: 672px;" });
            props.Add(new Prop { name = "max-h-3xl", css = "max-height: 768px;" });
            props.Add(new Prop { name = "max-h-4xl", css = "max-height: 896px;" });
            props.Add(new Prop { name = "max-h-5xl", css = "max-height: 1024px;" });
            props.Add(new Prop { name = "max-h-6xl", css = "max-height: 1152px;" });
            props.Add(new Prop { name = "max-h-7xl", css = "max-height: 1280px;" });
            props.Add(new Prop { name = "max-h-full", css = "max-height: 100%;" });
            return props;
        }

        /* === Borders ===*/
        private static List<Prop> GetBorderRadiusProps()
        {
            var props = new List<Prop>();
            props.AddRange(GetBorderSizes("rounded", "border-radius"));
            props.AddRange(GetBorderSizes("rounded-t", new string[] { "border-top-left-radius", "border-top-right-radius" }));
            props.AddRange(GetBorderSizes("rounded-r", new string[] { "border-top-right-radius", "border-bottom-right-radius" }));
            props.AddRange(GetBorderSizes("rounded-b", new string[] { "border-bottom-right-radius", "border-bottom-left-radius" }));
            props.AddRange(GetBorderSizes("rounded-l", new string[] { "border-top-left-radius", "border-bottom-left-radius" }));
            props.AddRange(GetBorderSizes("rounded-tl", "border-top-left-radius"));
            props.AddRange(GetBorderSizes("rounded-tr", "border-top-right-radius"));
            props.AddRange(GetBorderSizes("rounded-br", "border-bottom-right-radius"));
            props.AddRange(GetBorderSizes("rounded-bl", "border-bottom-left-radius"));
            return props;
        }

        private static List<Prop> GetBorderWidthProps()
        {
            var props = new List<Prop>();
            props.AddRange(GetBorderWidths("border", "border-width"));
            props.AddRange(GetBorderWidths("border-t", "border-top-width"));
            props.AddRange(GetBorderWidths("border-r", "border-right-width"));
            props.AddRange(GetBorderWidths("border-b", "border-bottom-width"));
            props.AddRange(GetBorderWidths("border-l", "border-left-width"));
            return props;
        }

        /* === Transitions ===*/
        private static List<Prop> GetTransitionProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "transition", css = "transition-property: background-color, border-color, color, fill, stroke, opacity, box-shadow, transform; transition-timing-function: ease-in-out-cubic; transition-duration: 150ms;" });
            props.Add(new Prop { name = "transition-none", css = "transition-property: none;" });
            props.Add(new Prop { name = "transition-all", css = "transition-property: all; transition-timing-function: ease-in-out-cubic; transition-duration: 150ms;" });
            props.Add(new Prop { name = "transition-colors", css = "transition-property: background-color, border-color, color, fill, stroke; transition-timing-function: ease-in-out-cubic; transition-duration: 150ms;" });
            props.Add(new Prop { name = "transition-opacity", css = "transition-property: opacity; transition-timing-function: ease-in-out-cubic; transition-duration: 150ms;" });
            props.Add(new Prop { name = "transition-shadow", css = "transition-property: box-shadow; transition-timing-function: ease-in-out-cubic; transition-duration: 150ms;" });
            props.Add(new Prop { name = "transition-transform", css = "transition-property: transform; transition-timing-function: ease-in-out-cubic; transition-duration: 150ms;" });
            return props;
        }

        private static List<Prop> GetTransitionDurationProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "duration-75", css = "transition-duration: 75ms;" });
            props.Add(new Prop { name = "duration-100", css = "transition-duration: 100ms;" });
            props.Add(new Prop { name = "duration-150", css = "transition-duration: 150ms;" });
            props.Add(new Prop { name = "duration-200", css = "transition-duration: 200ms;" });
            props.Add(new Prop { name = "duration-300", css = "transition-duration: 300ms;" });
            props.Add(new Prop { name = "duration-500", css = "transition-duration: 500ms;" });
            props.Add(new Prop { name = "duration-700", css = "transition-duration: 700ms;" });
            props.Add(new Prop { name = "duration-1000", css = "transition-duration: 1000ms;" });
            return props;
        }

        private static List<Prop> GetTransitionTimingFnProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "ease-linear", css = "transition-timing-function: linear;" });
            props.Add(new Prop { name = "ease-in", css = "transition-timing-function: ease-in-cubic;" });
            props.Add(new Prop { name = "ease-out", css = "transition-timing-function: ease-out-cubic;" });
            props.Add(new Prop { name = "ease-in-out", css = "transition-timing-function: ease-in-out-cubic;" });
            return props;
        }

        private static List<Prop> GetTransitionDelayProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "delay-75", css = "transition-delay: 75ms;" });
            props.Add(new Prop { name = "delay-100", css = "transition-delay: 100ms;" });
            props.Add(new Prop { name = "delay-150", css = "transition-delay: 150ms;" });
            props.Add(new Prop { name = "delay-200", css = "transition-delay: 200ms;" });
            props.Add(new Prop { name = "delay-300", css = "transition-delay: 300ms;" });
            props.Add(new Prop { name = "delay-500", css = "transition-delay: 500ms;" });
            props.Add(new Prop { name = "delay-700", css = "transition-delay: 700ms;" });
            props.Add(new Prop { name = "delay-1000", css = "transition-delay: 1000ms;" });
            return props;
        }

        /* === Interactivity ===*/
        private static List<Prop> GetCursorProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "cursor-arrow", css = "cursor: arrow;" });
            props.Add(new Prop { name = "cursor-text", css = "cursor: text;" });
            props.Add(new Prop { name = "cursor-resize-vertical", css = "cursor: resize-vertical;" });
            props.Add(new Prop { name = "cursor-resize-horizontal", css = "cursor: resize-horizontal;" });
            props.Add(new Prop { name = "cursor-link", css = "cursor: link;" });
            props.Add(new Prop { name = "cursor-slide-arrow", css = "cursor: slide-arrow;" });
            props.Add(new Prop { name = "cursor-resize-up-right", css = "cursor: resize-up-right;" });
            props.Add(new Prop { name = "cursor-resize-up-left", css = "cursor: resize-up-left;" });
            props.Add(new Prop { name = "cursor-move-arrow", css = "cursor: move-arrow;" });
            props.Add(new Prop { name = "cursor-rotate-arrow", css = "cursor: rotate-arrow;" });
            props.Add(new Prop { name = "cursor-scale-arrow", css = "cursor: scale-arrow;" });
            props.Add(new Prop { name = "cursor-arrow-plus", css = "cursor: arrow-plus;" });
            props.Add(new Prop { name = "cursor-arrow-minus", css = "cursor: arrow-minus;" });
            props.Add(new Prop { name = "cursor-pan", css = "cursor: pan;" });
            props.Add(new Prop { name = "cursor-orbit", css = "cursor: orbit;" });
            props.Add(new Prop { name = "cursor-zoom", css = "cursor: zoom;" });
            props.Add(new Prop { name = "cursor-fps", css = "cursor: fps;" });
            props.Add(new Prop { name = "cursor-split-resize-up-down", css = "cursor: split-resize-up-down;" });
            props.Add(new Prop { name = "cursor-split-resize-left-right", css = "cursor: split-resize-left-right;" });
            return props;
        }

        /* === Box Alignment ===*/
        private static List<Prop> GetJustifyProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "justify-start", css = "justify-content: flex-start;" });
            props.Add(new Prop { name = "justify-end", css = "justify-content: flex-end;" });
            props.Add(new Prop { name = "justify-center", css = "justify-content: center;" });
            props.Add(new Prop { name = "justify-between", css = "justify-content: space-between;" });
            props.Add(new Prop { name = "justify-around", css = "justify-content: space-around;" });
            props.Add(new Prop { name = "justify-evenly", css = "justify-content: space-evenly;" });
            return props;
        }

        private static List<Prop> GetAlignProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "content-start", css = "align-content: flex-start;" });
            props.Add(new Prop { name = "content-center", css = "align-content: center;" });
            props.Add(new Prop { name = "content-end", css = "align-content: flex-end;" });
            props.Add(new Prop { name = "items-stretch", css = "align-items: stretch;" });
            props.Add(new Prop { name = "items-start", css = "align-items: flex-start;" });
            props.Add(new Prop { name = "items-center", css = "align-items: center;" });
            props.Add(new Prop { name = "items-end", css = "align-items: flex-end;" });
            props.Add(new Prop { name = "self-auto", css = "align-self: auto;" });
            props.Add(new Prop { name = "self-start", css = "align-self: flex-start;" });
            props.Add(new Prop { name = "self-end", css = "align-self: flex-end;" });
            props.Add(new Prop { name = "self-center", css = "align-self: center;" });
            props.Add(new Prop { name = "self-stretch", css = "align-self: stretch;" });
            return props;
        }

        /* === Typography ===*/
        private static List<Prop> GetFontSizeProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "text-xs", css = "font-size: 12px;" });
            props.Add(new Prop { name = "text-sm", css = "font-size: 14px;" });
            props.Add(new Prop { name = "text-base", css = "font-size: 16px;" });
            props.Add(new Prop { name = "text-lg", css = "font-size: 18px;" });
            props.Add(new Prop { name = "text-xl", css = "font-size: 20px;" });
            props.Add(new Prop { name = "text-2xl", css = "font-size: 24px;" });
            props.Add(new Prop { name = "text-3xl", css = "font-size: 30px;" });
            props.Add(new Prop { name = "text-4xl", css = "font-size: 36px;" });
            props.Add(new Prop { name = "text-5xl", css = "font-size: 48px;" });
            props.Add(new Prop { name = "text-6xl", css = "font-size: 60px;" });
            props.Add(new Prop { name = "text-7xl", css = "font-size: 72px;" });
            props.Add(new Prop { name = "text-8xl", css = "font-size: 96px;" });
            props.Add(new Prop { name = "text-9xl", css = "font-size: 128px;" });
            return props;
        }

        private static List<Prop> GetFontStyleProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "italic", css = "-unity-font-style: italic;" });
            props.Add(new Prop { name = "not-italic", css = "-unity-font-style: normal;" });
            props.Add(new Prop { name = "font-bold", css = "-unity-font-style: bold;" });
            props.Add(new Prop { name = "font-bold-italic", css = "-unity-font-style: bold-and-italic;" });
            return props;
        }

        private static List<Prop> GetLetterSpacingProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "tracking-tighter", css = "letter-spacing: -5px;" });
            props.Add(new Prop { name = "tracking-tight", css = "letter-spacing: -2.5px;" });
            props.Add(new Prop { name = "tracking-normal", css = "letter-spacing: 0px;" });
            props.Add(new Prop { name = "tracking-wide", css = "letter-spacing: 2.5px;" });
            props.Add(new Prop { name = "tracking-wider", css = "letter-spacing: 5px;" });
            props.Add(new Prop { name = "tracking-widest", css = "letter-spacing: 10px;" });
            return props;
        }

        private static List<Prop> GetTextAlignProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "text-upper-left", css = "-unity-text-align: upper-left;" });
            props.Add(new Prop { name = "text-upper-center", css = "-unity-text-align: upper-center;" });
            props.Add(new Prop { name = "text-upper-right", css = "-unity-text-align: upper-right;" });
            props.Add(new Prop { name = "text-middle-left", css = "-unity-text-align: middle-left;" });
            props.Add(new Prop { name = "text-middle-center", css = "-unity-text-align: middle-center;" });
            props.Add(new Prop { name = "text-middle-right", css = "-unity-text-align: middle-right;" });
            props.Add(new Prop { name = "text-lower-left", css = "-unity-text-align: lower-left;" });
            props.Add(new Prop { name = "text-lower-center", css = "-unity-text-align: lower-center;" });
            props.Add(new Prop { name = "text-lower-right", css = "-unity-text-align: lower-right;" });
            return props;
        }

        private static List<Prop> GetTextOverflowProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "truncate", css = "overflow: hidden; white-space: nowrap; text-overflow: ellipsis;" });
            props.Add(new Prop { name = "overflow-ellipsis", css = "text-overflow: ellipsis;" });
            props.Add(new Prop { name = "overflow-clip", css = "text-overflow: clip;" });
            return props;
        }

        private static List<Prop> GetTextWhiteSpaceProps()
        {
            var props = new List<Prop>();
            props.Add(new Prop { name = "whitespace-normal", css = "white-space: normal;" });
            props.Add(new Prop { name = "whitespace-no-wrap", css = "white-space: nowrap;" });
            return props;
        }


        // Tailwind border sizes
        private static Prop[] borderSizes = new Prop[] {
            new Prop { name = "none", css = "0px" },
            new Prop { name = "sm", css = "2px" },
            new Prop { name = "", css = "4px" },
            new Prop { name = "md", css = "6px" },
            new Prop { name = "lg", css = "8px" },
            new Prop { name = "xl", css = "12px" },
            new Prop { name = "2xl", css = "16px" },
            new Prop { name = "3xl", css = "24px" },
            new Prop { name = "full", css = "9999px" },
        };

        private static Prop[] borderWidths = new Prop[] {
            new Prop { name = "", css = "1px" },
            new Prop { name = "0", css = "0px" },
            new Prop { name = "2", css = "2px" },
            new Prop { name = "4", css = "4px" },
            new Prop { name = "8", css = "8px" },
        };


        // Tailwind scales
        private static Prop[] scales = new Prop[] {
            new Prop { name = "0", css = "0" },
            new Prop { name = "50", css = ".5" },
            new Prop { name = "75", css = ".75" },
            new Prop { name = "90", css = ".9" },
            new Prop { name = "95", css = ".95" },
            new Prop { name = "100", css = "1" },
            new Prop { name = "105", css = "1.05" },
            new Prop { name = "110", css = "1.1" },
            new Prop { name = "125", css = "1.25" },
            new Prop { name = "150", css = "1.5" },
        };

        // Tailwind angles
        private static Prop[] angles = new Prop[] {
            new Prop { name = "0", css = "0deg" },
            new Prop { name = "1", css = "1deg" },
            new Prop { name = "2", css = "2deg" },
            new Prop { name = "3", css = "3deg" },
            new Prop { name = "6", css = "6deg" },
            new Prop { name = "12", css = "12deg" },
            new Prop { name = "45", css = "45deg" },
            new Prop { name = "90", css = "90deg" },
            new Prop { name = "180", css = "180deg" },
            new Prop { name = "-180", css = "-180deg" },
            new Prop { name = "-90", css = "-90deg" },
            new Prop { name = "-45", css = "-45deg" },
            new Prop { name = "-12", css = "-12deg" },
            new Prop { name = "-6", css = "-6deg" },
            new Prop { name = "-3", css = "-3deg" },
            new Prop { name = "-2", css = "-2deg" },
            new Prop { name = "-1", css = "-1deg" },
        };

        // Tailwind sizes
        private static Prop[] baseSizes = new Prop[] {
            new Prop { name = "0", css = "0px" },
            new Prop { name = "0d5", css = "2px" },
            new Prop { name = "1", css = "4px" },
            new Prop { name = "1d5", css = "6px" },
            new Prop { name = "2", css = "8px" },
            new Prop { name = "2d5", css = "10px" },
            new Prop { name = "3", css = "12px" },
            new Prop { name = "3d5", css = "14px" },
            new Prop { name = "4", css = "16px" },
            new Prop { name = "5", css = "20px" },
            new Prop { name = "6", css = "24px" },
            new Prop { name = "7", css = "28px" },
            new Prop { name = "8", css = "32px" },
            new Prop { name = "9", css = "36px" },
            new Prop { name = "10", css = "40px" },
            new Prop { name = "11", css = "44px" },
            new Prop { name = "12", css = "48px" },
            new Prop { name = "14", css = "56px" },
            new Prop { name = "16", css = "64px" },
            new Prop { name = "20", css = "80px" },
            new Prop { name = "24", css = "96px" },
            new Prop { name = "28", css = "112px" },
            new Prop { name = "32", css = "128px" },
            new Prop { name = "36", css = "144px" },
            new Prop { name = "40", css = "160px" },
            new Prop { name = "44", css = "176px" },
            new Prop { name = "48", css = "192px" },
            new Prop { name = "52", css = "208px" },
            new Prop { name = "56", css = "224px" },
            new Prop { name = "60", css = "240px" },
            new Prop { name = "64", css = "256px" },
            new Prop { name = "72", css = "288px" },
            new Prop { name = "80", css = "320px" },
            new Prop { name = "96", css = "384px" },
            new Prop { name = "px", css = "1px" },
        };

        private static Prop[] fractionalSizes = new Prop[] {
            new Prop { name = "1s2", css = "50%" },
            new Prop { name = "1s3", css = "33.333333%" },
            new Prop { name = "2s3", css = "66.666667%" },
            new Prop { name = "1s4", css = "25%" },
            new Prop { name = "2s4", css = "50%" },
            new Prop { name = "3s4", css = "75%" },
            new Prop { name = "full", css = "100%" },
        };

        private static Prop[] fractionalSizesExtra = new Prop[] {
            new Prop { name = "1s5", css = "20%" },
            new Prop { name = "2s5", css = "40%" },
            new Prop { name = "3s5", css = "60%" },
            new Prop { name = "4s5", css = "80%" },
            new Prop { name = "1s6", css = "16.666667%" },
            new Prop { name = "2s6", css = "33.333333%" },
            new Prop { name = "3s6", css = "50%" },
            new Prop { name = "4s6", css = "66.666667%" },
            new Prop { name = "5s6", css = "83.333333%" },
            new Prop { name = "1s12", css = "8.333333%" },
            new Prop { name = "2s12", css = "16.666667%" },
            new Prop { name = "3s12", css = "25%" },
            new Prop { name = "4s12", css = "33.333333%" },
            new Prop { name = "5s12", css = "41.666667%" },
            new Prop { name = "6s12", css = "50%" },
            new Prop { name = "7s12", css = "58.333333%" },
            new Prop { name = "8s12", css = "66.666667%" },
            new Prop { name = "9s12", css = "75%" },
            new Prop { name = "10s12", css = "83.333333%" },
            new Prop { name = "11s12", css = "91.666667%" },
        };

        private static List<Prop> GetScales(string twTag, string cssProp)
        {
            return GetScales(twTag, new string[] { cssProp });
        }
        private static List<Prop> GetScales(string twTag, string[] cssProps)
        {
            var props = new List<Prop>();

            foreach (var scale in scales)
            {
                props.Add(new Prop
                {
                    name = $"{twTag}-{scale.name}",
                    css = cssProps.Select(x => $"{x}: {scale.css};").Aggregate((x, y) => $"{x} {y}")
                });
            }

            return props;
        }

        private static List<Prop> GetAngles(string twTag, string cssProp)
        {
            return GetAngles(twTag, new string[] { cssProp });
        }
        private static List<Prop> GetAngles(string twTag, string[] cssProps)
        {
            var props = new List<Prop>();

            foreach (var angle in angles)
            {
                props.Add(new Prop
                {
                    name = $"{twTag}-{angle.name}",
                    css = cssProps.Select(x => $"{x}: {angle.css};").Aggregate((x, y) => $"{x} {y}")
                });
            }

            return props;
        }

        private static List<Prop> GetSizes(string twTag, string cssProp, bool includeNegative = false)
        {
            return GetSizes(twTag, new string[] { cssProp }, includeNegative);
        }
        private static List<Prop> GetSizes(string twTag, string[] cssProps, bool includeNegative = false, string nameSuffix = "")
        {
            var sizes = new List<Prop>();

            foreach (var size in baseSizes)
            {
                sizes.Add(new Prop
                {
                    name = $"{twTag}-{size.name}{nameSuffix}",
                    css = cssProps.Select(x => $"{x}: {size.css};").Aggregate((x, y) => $"{x} {y}")
                });
            }

            if (includeNegative)
            {
                foreach (var size in baseSizes)
                {
                    sizes.Add(new Prop
                    {
                        name = $"-{twTag}-{size.name}",
                        css = cssProps.Select(x => $"{x}: -{size.css};").Aggregate((x, y) => $"{x} {y}")
                    });
                }
            }

            return sizes;
        }

        private static List<Prop> GetSizesFrac(string twTag, string cssProp, bool includeNegative = false)
        {
            return GetSizesFrac(twTag, new string[] { cssProp }, includeNegative);
        }
        private static List<Prop> GetSizesFrac(string twTag, string[] cssProps, bool includeNegative = false)
        {
            var sizes = new List<Prop>();

            foreach (var size in fractionalSizes)
            {
                sizes.Add(new Prop
                {
                    name = $"{twTag}-{size.name}",
                    css = cssProps.Select(x => $"{x}: {size.css};").Aggregate((x, y) => $"{x} {y}")
                });
            }

            if (includeNegative)
            {
                foreach (var size in fractionalSizes)
                {
                    sizes.Add(new Prop
                    {
                        name = $"-{twTag}-{size.name}",
                        css = cssProps.Select(x => $"{x}: -{size.css};").Aggregate((x, y) => $"{x} {y}")
                    });
                }
            }

            return sizes;
        }
        private static List<Prop> GetSizesFracExtra(string twTag, string cssProp)
        {
            var sizes = new List<Prop>();

            foreach (var size in fractionalSizesExtra)
            {
                sizes.Add(new Prop
                {
                    name = $"{twTag}-{size.name}",
                    css = $"{cssProp}: {size.css};"
                });
            }

            return sizes;
        }

        private static List<Prop> GetBorderSizes(string twTag, string cssProp)
        {
            return GetBorderSizes(twTag, new string[] { cssProp });
        }
        private static List<Prop> GetBorderSizes(string twTag, string[] cssProps)
        {
            var sizes = new List<Prop>();
            foreach (var size in borderSizes)
            {
                // Hack to remove the - when the name is empty
                var sep = string.IsNullOrEmpty(size.name) ? "" : "-";
                sizes.Add(new Prop
                {
                    name = $"{twTag}{sep}{size.name}",
                    css = cssProps.Select(x => $"{x}: {size.css};").Aggregate((x, y) => $"{x} {y}")
                });
            }

            return sizes;
        }

        private static List<Prop> GetBorderWidths(string twTag, string cssProp)
        {
            var sizes = new List<Prop>();

            foreach (var size in borderWidths)
            {
                // Hack to remove the - when the name is empty
                var sep = string.IsNullOrEmpty(size.name) ? "" : "-";
                sizes.Add(new Prop
                {
                    name = $"{twTag}{sep}{size.name}",
                    css = $"{cssProp}: {size.css};"
                });
            }

            return sizes;
        }


        private static List<Prop> GetColors(string twTag, string cssProp)
        {
            var colors = new List<Prop>();

            var tailwindColors = Utils.TailwindColors;
            foreach (var color in tailwindColors)
            {
                foreach (var shade in color.shades)
                {
                    colors.Add(new Prop
                    {
                        name = $"{twTag}-{color.name}-{shade.value}",
                        css = $"{cssProp}: {shade.hex};"
                    });
                }
            }
            colors.Add(new Prop
            {
                name = $"{twTag}-transparent",
                css = $"{cssProp}: transparent;"
            });
            colors.Add(new Prop
            {
                name = $"{twTag}-white",
                css = $"{cssProp}: white;"
            });
            colors.Add(new Prop
            {
                name = $"{twTag}-black",
                css = $"{cssProp}: black;"
            });

            return colors;
        }
    }
}
