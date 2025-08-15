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
    
    private ShapePath? _fgPath;
    private RectangleGeometry? _clip;
    
    static RateItem()
    {
        //AffectsRender<SegmentedItem>(BackgroundProperty);
    }

    public RateItem()
    {
        //Console.WriteLine("new RateItem");
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _fgPath = e.NameScope.Find<ShapePath>("PART_ForegroundStar");

        if (_fgPath != null)
        {
            _clip        = new RectangleGeometry();
            _fgPath.Clip = _clip;
            _transFillRatioToClip();
        } 
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == FillRatioProperty)
        {
        }
    }

    private void _transFillRatioToClip()
    {
        if (_clip == null)
        {
            return;
        }

        var size = 20;
        // 顶层裁剪宽度 = Fill * StarSize
        var width  = Math.Max(0, Math.Min(size, size * FillRatio));
        var height = size;
        _clip.Rect = new Rect(0, 0, width, height);
    }
}