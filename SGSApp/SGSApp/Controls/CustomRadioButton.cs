using System;
using SGSApp.Extensions;
using Xamarin.Forms;
using XLabs;

namespace SGSApp.Controls
{
    public class CustomRadioButton : View
    {
        public static readonly BindableProperty CheckedProperty =
            BindableProperty.Create<CustomRadioButton, bool>(
                p => p.Checked, false);

        /// <summary>
        ///     The default text property.
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create<CustomRadioButton, string>(
                p => p.Text, string.Empty);


        /// <summary>
        ///     Identifies the TextColor bindable property.
        /// </summary>
        /// <remarks />
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create<CustomRadioButton, Color>(
                p => p.TextColor, Color.Black);

        /// <summary>
        ///     The checked changed event.
        /// </summary>
        public EventHandler<EventArgs<bool>> CheckedChanged;

        /// <summary>
        ///     Gets or sets a value indicating whether the control is checked.
        /// </summary>
        /// <value>The checked state.</value>
        public bool Checked
        {
            get => this.GetValue<bool>(CheckedProperty);

            set
            {
                SetValue(CheckedProperty, value);
                var eventHandler = CheckedChanged;
                if (eventHandler != null) eventHandler.Invoke(this, value);
            }
        }

        public string Text
        {
            get => this.GetValue<string>(TextProperty);

            set => SetValue(TextProperty, value);
        }

        public Color TextColor
        {
            get => this.GetValue<Color>(TextColorProperty);

            set => SetValue(TextColorProperty, value);
        }

        public int Id { get; set; }
    }
}