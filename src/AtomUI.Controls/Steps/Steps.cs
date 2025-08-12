using AtomUI.Theme;
using AtomUI.Theme.Utils;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace AtomUI.Controls;

public class Steps : TemplatedControl, IMotionAwareControl, IControlSharedTokenResourcesHost
{
    
    #region 公共属性定义
    
    public static readonly StyledProperty<bool> IsMotionEnabledProperty =
        MotionAwareControlProperty.IsMotionEnabledProperty.AddOwner<Steps>();
    
    public bool IsMotionEnabled
    {
        get => GetValue(IsMotionEnabledProperty);
        set => SetValue(IsMotionEnabledProperty, value);
    }
    
    #endregion
    
    #region 内部属性定义
    #endregion
    
    Control IMotionAwareControl.PropertyBindTarget => this;
    Control IControlSharedTokenResourcesHost.HostControl => this;
    string IControlSharedTokenResourcesHost.TokenId => StepsToken.ID;
    
    static Steps()
    {
        //AffectsMeasure<Avatar>(SizeTypeProperty, TextProperty);
        //AffectsRender<Avatar>(ShapeProperty, IconProperty, SrcProperty, GapProperty);
    }
    
    public Steps()
    {
        this.RegisterResources();
    }
    
}