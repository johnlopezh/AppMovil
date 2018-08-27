using Xamarin.Forms;

namespace SGSApp.Controls
{
    public class SelectableItem : BindableObject
    {
        public static readonly BindableProperty DataProperty =
            BindableProperty.Create(nameof(Data), typeof(object), typeof(SelectableItem), null);

        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(SelectableItem), false);

        public SelectableItem(object data)
        {
            Data = data;
        }

        public SelectableItem(object data, bool isSelected)
        {
            Data = data;
            IsSelected = isSelected;
        }

        public object Data
        {
            get => GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        public bool IsSelected
        {
            get => (bool) GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }
    }

    public class SelectableItem<T> : SelectableItem
    {
        public SelectableItem(T data)
            : base(data)
        {
        }

        public SelectableItem(T data, bool isSelected)
            : base(data, isSelected)
        {
        }

        public new T Data
        {
            get => (T) base.Data;
            set => base.Data = value;
        }
    }
}