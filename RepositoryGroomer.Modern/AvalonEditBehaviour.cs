using System;
using System.Windows;
using System.Windows.Interactivity;
using ICSharpCode.AvalonEdit;
using RepositoryGroomer.Core;

namespace RepositoryGroomer.Modern
{
    public sealed class AvalonEditBehaviour : Behavior<TextEditor>
    {
        public static readonly DependencyProperty TextContainProperty =
            DependencyProperty.Register(nameof(TextContain), typeof(string), typeof(AvalonEditBehaviour),
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, TextContainPropertyChangedCallback));

        public static readonly DependencyProperty ReferenceContainProperty = DependencyProperty.Register(nameof(ReferenceContain), typeof(Reference), typeof(AvalonEditBehaviour),
            new FrameworkPropertyMetadata(default(Reference), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ReferenceContainPropertyChangedCallback));

        public static readonly DependencyProperty LinkedFileProperty = DependencyProperty.Register(nameof(LinkedFile),
            typeof(LinkedFileInfo), typeof(AvalonEditBehaviour),
            new FrameworkPropertyMetadata(default(LinkedFileInfo), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                LinkedFilePropertyChangedCallback));

        public string TextContain
        {
            get { return (string)GetValue(TextContainProperty); }
            set { SetValue(TextContainProperty, value); }
        }

        public Reference ReferenceContain
        {
            get { return (Reference)GetValue(ReferenceContainProperty); }
            set { SetValue(ReferenceContainProperty, value); }
        }
        public LinkedFileInfo LinkedFile
        {
            get { return (LinkedFileInfo)GetValue(LinkedFileProperty); }
            set { SetValue(LinkedFileProperty, value); }
        }
        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject != null)
                AssociatedObject.TextChanged += AssociatedObjectOnTextChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject != null)
                AssociatedObject.TextChanged -= AssociatedObjectOnTextChanged;
        }

        private void AssociatedObjectOnTextChanged(object sender, EventArgs eventArgs)
        {
            var textEditor = sender as TextEditor;
            if (textEditor?.Document != null)
                TextContain = textEditor.Document.Text;
        }

        private static void TextContainPropertyChangedCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var behavior = dependencyObject as AvalonEditBehaviour;
            var editor = behavior?.AssociatedObject;
            if (editor?.Document != null)
            {
                editor.Document.Text = dependencyPropertyChangedEventArgs.NewValue.ToString();
            }
        }

        private static void ReferenceContainPropertyChangedCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var behavior = dependencyObject as AvalonEditBehaviour;
            var editor = behavior?.AssociatedObject;
            if (editor?.Document != null)
            {
                var reference = (Reference)dependencyPropertyChangedEventArgs.NewValue;
                var indexof = editor.Document.Text.IndexOf(reference.Include) - 20;
                if (indexof > -1)
                    editor.Select(indexof, reference.OriginalXml.Length + 16);

                editor.ScrollToLine(editor.TextArea.TextView.HighlightedLine);
            }
        }

        private static void LinkedFilePropertyChangedCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var behavior = dependencyObject as AvalonEditBehaviour;
            var editor = behavior?.AssociatedObject;
            if (editor?.Document != null)
            {
                var linkedFile = (LinkedFileInfo) dependencyPropertyChangedEventArgs.NewValue;
                var indexofStart = editor.Document.Text.IndexOf(linkedFile.LinkedFileRelativePath) - 20;
                if (indexofStart > -1)
                    editor.Select(indexofStart, linkedFile.OriginalXml.Length + 16);

                editor.ScrollToLine(editor.TextArea.TextView.HighlightedLine);
            }
        }
    }
}
