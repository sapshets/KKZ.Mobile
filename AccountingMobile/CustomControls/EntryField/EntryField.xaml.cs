using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Shapes;

namespace AccountingMobile.CustomControls.EntryField;

public partial class EntryField : ContentView
    {
        public EntryField()
        {
            InitializeComponent();
            this.Dispatcher.Dispatch((() =>
            {
                InputField.TextChanged += HandleEntryTextChanged;
            }));
        }

        // Text property
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(
                nameof(Text),
                typeof(string),
                typeof(EntryField),
                string.Empty,
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    if (bindable is EntryField e)
                    {
                        if (!string.IsNullOrWhiteSpace(newValue?.ToString()))
                        {
                            e.InputFieldFrame.Dispatcher.Dispatch(() => { e.UpdateFocusState(true, true); });
                        }
                    }
                });

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        // Placeholder property
        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(
                nameof(Placeholder),
                typeof(string),
                typeof(EntryField),
                string.Empty);

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        // PlaceholderColor property
        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create(
                nameof(PlaceholderColor),
                typeof(Color),
                typeof(EntryField),
                Colors.Gray);

        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        // TextColor property
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(
                nameof(TextColor),
                typeof(Color),
                typeof(EntryField),
                Colors.Black);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly BindableProperty HintColorProperty =
            BindableProperty.Create(
                nameof(HintColor),
                typeof(Color),
                typeof(EntryField),
                Colors.Gray);

        public Color HintColor
        {
            get => (Color)GetValue(HintColorProperty);
            set => SetValue(HintColorProperty, value);
        }

        // FontSize property
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(
                nameof(FontSize),
                typeof(double),
                typeof(EntryField),
                14.0);

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        // BackgroundColor property
        public static new readonly BindableProperty BackgroundColorProperty =
            BindableProperty.Create(
                nameof(BackgroundColor),
                typeof(Color),
                typeof(EntryField),
                Colors.White);

        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        // BorderColor property
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(
                nameof(BorderColor),
                typeof(Color),
                typeof(EntryField),
                Colors.Gray);

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public static readonly BindableProperty FocusedBorderColorProperty =
            BindableProperty.Create(
                nameof(FocusedBorderColor),
                typeof(Color),
                typeof(EntryField),
                Colors.DimGray);

        public Color FocusedBorderColor
        {
            get => (Color)GetValue(FocusedBorderColorProperty);
            set => SetValue(FocusedBorderColorProperty, value);
        }

        // CornerRadius property
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(
                nameof(CornerRadius),
                typeof(CornerRadius),
                typeof(EntryField),
                new CornerRadius(0),
                propertyChanged: OnCornerRadiusChanged);

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty StrokeThicknessProperty =
            BindableProperty.Create(
                nameof(StrokeThickness),
                typeof(double),
                typeof(EntryField),
                1.0);

        public double StrokeThickness
        {
            get => (double)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        public static readonly BindableProperty FocusedStrokeThicknessProperty =
            BindableProperty.Create(
                nameof(FocusedStrokeThickness),
                typeof(double),
                typeof(EntryField),
                1.0);

        public double FocusedStrokeThickness
        {
            get => (double)GetValue(FocusedStrokeThicknessProperty);
            set => SetValue(FocusedStrokeThicknessProperty, value);
        }

        public static readonly BindableProperty InputTypeProperty =
            BindableProperty.Create(
                nameof(InputType),
                typeof(InputDataType),
                typeof(EntryField),
                InputDataType.String,
                propertyChanged: OnInputTypeChanged);

        public InputDataType InputType
        {
            get => (InputDataType)GetValue(InputTypeProperty);
            set => SetValue(InputTypeProperty, value);
        }

        public static readonly BindableProperty IsErrorProperty =
            BindableProperty.Create(
                nameof(IsError),
                typeof(bool),
                typeof(EntryField),
                false,
                propertyChanged: OnIsErrorChanged);

        public bool IsError
        {
            get => (bool)GetValue(IsErrorProperty);
            set => SetValue(IsErrorProperty, value);
        }

        public static readonly BindableProperty ErrorColorProperty =
            BindableProperty.Create(
                nameof(ErrorColor),
                typeof(Color),
                typeof(EntryField),
                Colors.Red);

        public Color ErrorColor
        {
            get => (Color)GetValue(ErrorColorProperty);
            set => SetValue(ErrorColorProperty, value);
        }

        public static readonly BindableProperty MaxSymbolsProperty =
            BindableProperty.Create(
                propertyName: nameof(MaxSymbols),
                returnType: typeof(int),
                declaringType: typeof(EntryField),
                defaultValue: int.MaxValue
            );
        
        public int MaxSymbols
        {
            get => (int)GetValue(MaxSymbolsProperty);
            set => SetValue(MaxSymbolsProperty, value);
        }
        
        public static readonly BindableProperty IsFocusedProperty =
            BindableProperty.Create(
                propertyName: nameof(IsFocused),
                returnType: typeof(bool),
                declaringType: typeof(EntryField),
                defaultValue: false,
                propertyChanged: ((bindable, value, newValue) =>
                {
                    if (bindable is EntryField ef)
                    {
                        ef.Dispatcher.Dispatch(() =>
                        {
                            if ((bool)newValue)
                            {
                                ef.InputField.Focus();
                            }
                            else
                            {
                                ef.InputField.Unfocus();
                            }
                        });
                    }
                }));
        
        public bool IsFocused
        {
            get => (bool)GetValue(IsFocusedProperty);
            set => SetValue(IsFocusedProperty, value);
        }
        private static void OnCornerRadiusChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is EntryField e)
            {
                e.Dispatcher.Dispatch(() =>
                {
                    e.InputFieldFrame.StrokeShape = new RoundRectangle
                    {
                        CornerRadius = (CornerRadius)newValue
                    };
                });
            }
        }

        private static void OnInputTypeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is EntryField entryField && newValue is InputDataType newInputType)
            {
                switch (newInputType)
                {
                    case InputDataType.String:
                        entryField.InputField.Keyboard = Keyboard.Default;
                        entryField.InputField.TextChanged -= ValidateNumericInput;
                        entryField.InputField.TextChanged -= ValidateDateTimeInput;
                        break;

                    case InputDataType.Digit:
                        entryField.InputField.Keyboard = Keyboard.Numeric;
                        entryField.InputField.TextChanged -= ValidateDateTimeInput;
                        entryField.InputField.TextChanged += ValidateNumericInput;
                        break;

                    case InputDataType.DateTime:
                        entryField.InputField.Keyboard = Keyboard.Default;
                        entryField.InputField.TextChanged -= ValidateNumericInput;
                        entryField.InputField.TextChanged += ValidateDateTimeInput;
                        break;
                }
            }
        }

        private static void OnIsErrorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is EntryField ef)
            {
                ef.Dispatcher.Dispatch(() =>
                {
                    ef.InputFieldFrame.Stroke = ef.IsError ? ef.ErrorColor 
                        : string.IsNullOrWhiteSpace(ef.Text) ? ef.BorderColor : ef.FocusedBorderColor;
                    ef.Hint.TextColor = ef.IsError ? ef.ErrorColor 
                        : string.IsNullOrWhiteSpace(ef.Text) ? ef.TextColor : ef.FocusedBorderColor;
                });
            }
        }

    }