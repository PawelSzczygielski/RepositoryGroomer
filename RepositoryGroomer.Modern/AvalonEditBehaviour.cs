using System;
using System.Windows;
using System.Windows.Interactivity;
using ICSharpCode.AvalonEdit;

namespace RepositoryGroomer.Modern
{
    public sealed class AvalonEditBehaviour : Behavior<TextEditor>
    {
        public static readonly DependencyProperty TextContainProperty =
            DependencyProperty.Register(nameof(TextContain), typeof(string), typeof(AvalonEditBehaviour),
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, TextContainPropertyChangedCallback));

        public static readonly DependencyProperty ReferenceContainProperty = DependencyProperty.Register(nameof(ReferenceContain), typeof(string), typeof(AvalonEditBehaviour),
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ReferenceContainPropertyChangedCallback));

        public string TextContain
        {
            get { return (string)GetValue(TextContainProperty); }
            set { SetValue(TextContainProperty, value); }
        }

        public string ReferenceContain
        {
            get { return (string)GetValue(ReferenceContainProperty); }
            set { SetValue(ReferenceContainProperty, value); }
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
                var caretOffset = editor.CaretOffset;
                editor.Document.Text = dependencyPropertyChangedEventArgs.NewValue.ToString();
                editor.CaretOffset = caretOffset;
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
                var newStringValue = (string)dependencyPropertyChangedEventArgs.NewValue;
                var indexof = editor.Document.Text.IndexOf(newStringValue);
                if (indexof != -1)
                    editor.Select(indexof, newStringValue.Length);
            }
        }
    }
}
