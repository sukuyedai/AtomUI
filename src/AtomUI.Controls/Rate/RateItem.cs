using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using ShapePath = Avalonia.Controls.Shapes.Path;

namespace AtomUI.Controls;

public class RateItem : ContentControl
{

    public static readonly StyledProperty<double> FillRatioProperty =
        AvaloniaProperty.Register<RateItem, double>(nameof(FillRatio));
    
    public double FillRatio
    {
        get => GetValue(FillRatioProperty);
        set => SetValue(FillRatioProperty, value);
    }
    
    private ShapePath? _foregroundPath;
    private RectangleGeometry? _clip;
    
    static RateItem()
    {
    }

    public RateItem()
    {
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _foregroundPath = e.NameScope.Find<ShapePath>("PART_ForegroundStar");
        if (_foregroundPath != null)
        {
            _clip                = new RectangleGeometry();
            _foregroundPath.Clip = _clip;
            _transFillRatioToClip();
        } 
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == FillRatioProperty)
        {
            _transFillRatioToClip();
        }
    }
    
    private void _transFillRatioToClip()
    {
        if (_clip == null)
        {
            return;
        }
        var size = 60;
        var width  = Math.Max(0, Math.Min(size, size * FillRatio));
        var height = size;
        _clip.Rect = new Rect(0, 0, width, height);
        if (_foregroundPath != null)
        {
            _foregroundPath.Clip = _clip;
            _foregroundPath.InvalidateVisual();
        }
    }
    
    public void transFillRatioToClip()
    {
        if (_clip == null)
        {
            return;
        }
        var size = 60;
        var width  = Math.Max(0, Math.Min(size, size * FillRatio));
        var height = size;
        _clip.Rect = new Rect(0, 0, width, height);
        if (_foregroundPath != null)
        {
            _foregroundPath.Clip = _clip;
        }
    }
}