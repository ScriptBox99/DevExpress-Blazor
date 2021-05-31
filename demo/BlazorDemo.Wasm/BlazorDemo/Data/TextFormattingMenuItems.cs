using System.Collections.Generic;

namespace BlazorDemo.Data {
    abstract class TextFormattingMenuItem {
        protected TextFormattingMenuItem(TextFormatting textFormatting, string text) {
            TextFormatting = textFormatting;
            Text = text;
        }

        public TextFormatting TextFormatting { get; }
        public string Text { get; }
        public virtual string IconUrl { get { return null; } }
        public virtual bool Checked { get; protected set; }
        public List<TextFormattingMenuItem> Children { get; set; }
        public bool BeginGroup { get; set; }
        public string IconCss { get; set; }
        public bool SplitMenuButton { get; set; }
        public string Category { get; set; }
        public virtual void Click() { }
    }

    class TextFormattingParentMenuItem : TextFormattingMenuItem {
        public TextFormattingParentMenuItem(TextFormatting textFormatting, string text, List<TextFormattingMenuItem> children)
            : base(textFormatting, text) {
            Children = children;
        }
    }

    class FontFamilyMenuItem : TextFormattingMenuItem {
        public FontFamilyMenuItem(TextFormatting textFormatting, string text, string fontFamily)
            : base(textFormatting, text) {
            FontFamily = fontFamily;
        }

        string FontFamily { get; }
        public override void Click() {
            TextFormatting.FontFamily = FontFamily;
        }
    }

    class FontSizeMenuItem : TextFormattingMenuItem {
        public FontSizeMenuItem(TextFormatting textFormatting, string text, int fontSize)
            : base(textFormatting, text) {
            FontSize = fontSize;
        }

        int FontSize { get; }
        public override void Click() {
            TextFormatting.FontSize = FontSize;
        }
    }

    class TextDecorationMenuItem : TextFormattingMenuItem {
        public TextDecorationMenuItem(TextFormatting textFormatting, string text, string attributeName)
            : base(textFormatting, text) {
            AttributeName = attributeName;
        }

        string AttributeName { get; }
        public override bool Checked {
            get { return TextFormatting.Decoration[AttributeName]; }
            protected set { TextFormatting.Decoration[AttributeName] = value; }
        }

        public override string IconUrl { get { return Checked ? StaticAssetUtils.GetImagePath("check.svg") : null; } }

        public override void Click() {
            Checked = !Checked;
        }
    }

    class ChangeCaseMenuItem : TextFormattingMenuItem {
        public ChangeCaseMenuItem(TextFormatting textFormatting, string text, string textCase)
            : base(textFormatting, text) {
            TextCase = textCase;
        }

        string TextCase { get; }
        public override void Click() {
            TextFormatting.TextCase = TextCase;
        }
    }

    class ClearFormattingMenuItem : TextFormattingMenuItem {
        public ClearFormattingMenuItem(TextFormatting textFormatting)
            : base(textFormatting, "Clear Formatting") {
        }

        public override void Click() {
            TextFormatting.ClearFormatting();
        }
    }
}
