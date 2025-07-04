namespace AccountingMobile.CustomControls.EntryField;

public partial class EntryField
    {
        public event EventHandler<TextChangedEventArgs> OnTextChanged;
        private void InputField_Focused(object sender, FocusEventArgs e)
        {
            UpdateFocusState(true);
        }

        private void InputField_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(InputField.Text))
            {
                UpdateFocusState(false);
            }
        }

        public async Task UpdateFocusState(bool isFocused, bool isFilledOnStart = false)
        {
            Dispatcher.Dispatch(() =>
            {
                ChangePosition(isFocused, isFilledOnStart);
                ChangeScale(isFocused, isFilledOnStart);
                ChangeBorderState(isFocused);
            });
        }

        public async Task ChangePosition(bool isFocused, bool isFilledOnStart = false)
        {
            var hintTranslationY = InputFieldFrame.Height / 2 * -1;
            if (isFilledOnStart)
            {
                Hint.TranslationY = hintTranslationY;
                return;
            }

            double targetTranslationY = isFocused ? hintTranslationY : 0;
            double step = isFocused ? -1 : 1;

            while (isFocused 
                       ? Hint.TranslationY > targetTranslationY 
                       : Hint.TranslationY < targetTranslationY)
            {
                Hint.TranslationY += step;
#if IOS
                await Task.Delay(2);
#endif
            }

            Hint.TranslationY = targetTranslationY;
        }

        public async Task ChangeScale(bool isFocused, bool isFilledOnStart = false)
        {
            if (isFilledOnStart)
            {
                Hint.Scale = 0.8;
                return;
            }
            double targetScale = isFocused ? 0.8 : 1;
            double step = isFocused ? -0.01 : 0.01;

            while (isFocused 
                       ? Hint.Scale > targetScale 
                       : Hint.Scale < targetScale)
            {
                Hint.Scale += step;
#if IOS
                await Task.Delay(1);
#endif
            }
            
            Hint.Scale = targetScale;
        }

        public async Task ChangeBorderState(bool isFocused)
        {
            InputFieldFrame.Stroke = isFocused ? FocusedBorderColor : BorderColor;
            InputFieldFrame.StrokeThickness = isFocused ? FocusedStrokeThickness : StrokeThickness;
            Hint.TextColor = isFocused ? FocusedBorderColor : HintColor;
        }
        
        private void HandleEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            OnTextChanged?.Invoke(this, e);
            if (e.NewTextValue.Length > MaxSymbols)
            {
                InputField.Text = e.OldTextValue;
            }
        }
    }