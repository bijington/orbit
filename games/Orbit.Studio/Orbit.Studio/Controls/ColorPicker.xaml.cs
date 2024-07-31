namespace Orbit.Studio.Controls;

public partial class ColorPicker : Grid
{
    public ColorPicker()
    {
        InitializeComponent();
    }
    
    private void OnColorSliderValueChanged(object? sender, ValueChangedEventArgs e)
    {
        SelectedColor = Color.FromRgba(
            Red.Value / 255d,
            Blue.Value / 255d,
            Green.Value / 255d,
            Alpha.Value / 255d);
    }
    
    public static readonly BindableProperty SelectedColorProperty = 
        BindableProperty.Create(
            nameof(SelectedColor),
            typeof(Color),
            typeof(ColorPicker),
            Colors.Black, 
            propertyChanged: OnSelectedColorPropertyChanged);

    private static void OnSelectedColorPropertyChanged(BindableObject sender, object oldValue, object newValue)
    {
        ((ColorPicker)sender).UpdatePreviewColor();
    }

    private void UpdatePreviewColor()
    {
        ColorPreview.BackgroundColor = SelectedColor;
    }

    public Color SelectedColor
    {
        get => (Color)GetValue(SelectedColorProperty);
        set => SetValue(SelectedColorProperty, value);
    }
}