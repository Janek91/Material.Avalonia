using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Material.Dialog.Interfaces;
using Material.Dialog.ViewModels;
using System;

namespace Material.Dialog.Views
{
    public class TextFieldDialog : Window, IDialogWindowResult<TextFieldDialogResult>
    {
        public TextFieldDialogResult Result { get; set; }

        public TextFieldDialog()
        {
            Result = new TextFieldDialogResult();

            InitializeComponent();

            this.Closed += TextFieldDialog_Closed;
            this.Opened += TextFieldDialog_Opened;
        }

        private void TextFieldDialog_Closed(object sender, EventArgs e)
        {
            this.Opened -= TextFieldDialog_Opened;
            this.Closed -= TextFieldDialog_Closed;
        }

        private void TextFieldDialog_Opened(object sender, EventArgs e)
        {
            switch (DataContext)
            {
                case TextFieldDialogViewModel vm:
                    vm.ButtonClick.RaiseCanExecute();
                    break;
            }
        }

        public TextFieldDialogResult GetResult() => Result;

        public void SetNegativeResult(DialogResult result) => Result.result = result.GetResult;

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}
