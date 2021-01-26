﻿using Avalonia.Layout;
using Avalonia.Threading;
using Material.Dialog.Commands;
using Material.Dialog.ViewModels.TextField;
using Material.Dialog.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Material.Dialog.ViewModels
{
    public class TextFieldDialogViewModel : DialogWindowViewModel
    {
        private TextFieldDialog _window;

        private TextFieldViewModel[] m_TextFields;
        public TextFieldViewModel[] TextFields { get => m_TextFields; internal set => m_TextFields = value; }

        private DialogResultButton[] m_DialogButtons;
        public DialogResultButton[] DialogButtons { get => m_DialogButtons; internal set => m_DialogButtons = value; }

        private DialogResultButton m_PositiveButton;
        public DialogResultButton PositiveButton { get => m_PositiveButton; internal set => m_PositiveButton = value; }

        private DialogResultButton m_NegativeButton;
        public DialogResultButton NegativeButton { get => m_NegativeButton; internal set => m_NegativeButton = value; }

        private Orientation m_ButtonsStackOrientation;
        public Orientation ButtonsStackOrientation { get => m_ButtonsStackOrientation; internal set => m_ButtonsStackOrientation = value; }

        public TextFieldDialogViewModel(TextFieldDialog dialog)
        {
            _window = dialog;
            ButtonClick = new RelayCommand(OnPressButton, CanPressButton);
        }

        public void BindValidater()
        {
            foreach (var item in TextFields)
                item.OnValidateRequired += Field_OnValidateRequired;
        }

        private void Field_OnValidateRequired(object sender, bool e)
        {
            ButtonClick.RaiseCanExecute();
        }

        public void UnbindValidater()
        {
            foreach (var item in TextFields)
                item.OnValidateRequired -= Field_OnValidateRequired;
        }

        public bool ValidateFields()
        {
            foreach (var field in TextFields)
            {
                if (!field.IsValid)
                    return false;
            }
            return true;
        }

        public bool CanPressButton(object args)
        {
            if(args == PositiveButton)
            {
                return ValidateFields();
            }
            else if(args == NegativeButton)
            {
                return true;
            }
            return false;
        }
        public async void OnPressButton(object args)
        {
            var button = args as DialogResultButton;
            if (button is null)
                return; 

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                var result = new TextFieldDialogResult() { result = button.Result };
                var fields = new List<TextFieldResult>();
                foreach (var item in TextFields)
                    fields.Add(new TextFieldResult { Text = item.Text });
                result.fieldsResult = fields.ToArray();
                _window.Result = result;
                _window.Close();
                UnbindValidater();
            });
        }

        public RelayCommand ButtonClick { get; private set; }
    }
}
