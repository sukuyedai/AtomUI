using AtomUI.Theme;
using AtomUI.Theme.Data;
using AtomUI.Theme.Styling;
using AtomUI.Theme.Utils;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;

namespace AtomUI.Controls;

public class Rate : ItemsControl,
                    IMotionAwareControl,
                    IControlSharedTokenResourcesHost
{
    #region 公共属性定义

    public static readonly StyledProperty<int> CountProperty =
        AvaloniaProperty.Register<Rate, int>(nameof(Count), defaultValue: 5);
    
    public static readonly StyledProperty<string[]> TooltipsProperty =
        AvaloniaProperty.Register<Rate, string[]>(nameof(Tooltips));
    
    public static readonly StyledProperty<bool> IsDisabledProperty =
        AvaloniaProperty.Register<Rate, bool>(nameof(IsDisabled));

    public static readonly StyledProperty<double> ValueProperty =
        AvaloniaProperty.Register<Rate, double>(nameof(Value));

    public static readonly StyledProperty<string> CharacterProperty =
        AvaloniaProperty.Register<Rate, string>(nameof(Character));

    public static readonly StyledProperty<bool> IsMotionEnabledProperty
        = MotionAwareControlProperty.IsMotionEnabledProperty.AddOwner<Rate>();

    public int Count
    {
        get => GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }
    
    public string[] Tooltips
    {
        get => GetValue(TooltipsProperty);
        set => SetValue(TooltipsProperty, value);
    }

    public bool IsDisabled
    {
        get => GetValue(IsDisabledProperty);
        set => SetValue(IsDisabledProperty, value);
    }
        
    public double Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
    
    public string Character
    {
        get => GetValue(CharacterProperty);
        set => SetValue(CharacterProperty, value);
    }

    public bool IsMotionEnabled
    {
        get => GetValue(IsMotionEnabledProperty);
        set => SetValue(IsMotionEnabledProperty, value);
    }

    #endregion

    #region 内部属性定义

    Control IMotionAwareControl.PropertyBindTarget => this;
    Control IControlSharedTokenResourcesHost.HostControl => this;
    string IControlSharedTokenResourcesHost.TokenId => RateToken.ID;

    #endregion

    static Rate()
    {
        AffectsMeasure<Rate>();
    }

    public Rate()
    {
        this.PointerMoved  += OnPointerMoved;
        this.PointerExited += OnPointerExited;
        this.RegisterResources();
    }
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        BuildRateItems();
    }
    
    private void BuildRateItems()
    {
        Items.Clear();
        for (int i = 0; i < Count; i++)
        {
            var rateItem = new RateItem();
            Items.Add(rateItem);
        }
        ProcessChildItem();
    }

    private void ProcessChildItem()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            //if (ItemContainerGenerator.ContainerFromIndex(i) is RateItem rateItem)
            if (Items[i] is RateItem rateItem)
            {
                rateItem.FillRatio = CalculateFillRatio(i, Value);
            }
        }
    }

    private double CalculateFillRatio(int index, double value)
    {
        var fill = value - index;
        if (fill < 0) return 0;
        if (fill > 1) return 1;
        return fill;
    }

    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (IsDisabled)
        {
            return;
        }

        var    pointerPosition = e.GetPosition(this);
        var    rateWidth       = this.Bounds.Width;
        var    rateHeight      = this.Bounds.Height;
        double rateItemWidth   = 0.0;
        double spacing         = 0.0;
        if (TokenResourceUtils.FindTokenResource(this, RateTokenKey.ItemWidth) is double _rateItemWidth)
        {
            rateItemWidth = _rateItemWidth;
        }

        if (TokenResourceUtils.FindTokenResource(this, RateTokenKey.Spacing) is double _spacing)
        {
            spacing = _spacing;
        }

        var rateWrapWidth     = (Count - 1) * spacing + Count * rateItemWidth;
        var rateItemWrapWidth = rateItemWidth + spacing;
        //_hoverValue = Math.Clamp(pos.X / _starWidth, 0, TotalStars); 
        if (pointerPosition.X > rateWrapWidth)
        {
            return;
        }

        var hoveredRateItemNumber       = (int)(pointerPosition.X / rateItemWrapWidth);
        var hoveredCurrentRateItemWidth = pointerPosition.X % rateItemWrapWidth;
        var ratio                       = hoveredCurrentRateItemWidth / rateItemWidth;

        for (var i = 0; i < Items.Count; i++)
        {
            if (this.Items[i] is RateItem rateItem)
            {
                if (i == hoveredRateItemNumber)
                {
                    rateItem.FillRatio = ratio;
                }
                else if(i < hoveredRateItemNumber)
                {
                    rateItem.FillRatio = 1.0;
                }
                else
                {
                    rateItem.FillRatio = 0;
                }
            }
        }
        InvalidateVisual();
    }

    private void OnPointerExited(object? sender, PointerEventArgs e)
    {
        if (IsDisabled)
        {
            return;
        }
        InvalidateVisual();
    }
    
    /*
    public static Geometry CreateStarGeometry(Point center, double outerRadius, double innerRadius)
    {
        var points = new Point[10];
        for (int i = 0; i < 10; i++)
        {
            double angle  = Math.PI / 2 + i * Math.PI / 5; // 从顶部开始，每个点间隔36度（弧度制）
            double radius = i % 2 == 0 ? outerRadius : innerRadius;
            points[i] = new Point(
                center.X + radius * Math.Cos(angle),
                center.Y - radius * Math.Sin(angle) // 注意：Avalonia的Y轴向下为正，所以用减号
            );
        }

        var figure = new PathFigure
        {
            StartPoint = points[0],
            IsClosed   = true,
            Segments   = new PathSegments()
        };
        for (int i = 1; i < points.Length; i++)
        {
            figure.Segments.Add(new LineSegment { Point = points[i] });
        }
        var geometry = new PathGeometry();
        geometry.Figures?.Add(figure);
        return geometry;
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);
        Console.WriteLine("Render");
        var starGeometry = CreateStarGeometry(new Point(50, 50), 40, 20);
        context.DrawGeometry(Brushes.Gold, null, starGeometry);
    }
    */
}