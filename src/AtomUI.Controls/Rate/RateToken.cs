using AtomUI.Theme.TokenSystem;

namespace AtomUI.Controls;

[ControlDesignToken]
internal class RateToken : AbstractControlDesignToken
{
    public const string ID = "Rate";
    
    /// <summary>
    /// RateItem间距
    /// </summary>
    public double Spacing { get; set; }
    
    /// <summary>
    /// RateItem间距
    /// </summary>
    public double ItemWidth { get; set; }
    
    /// <summary>
    /// RateItem间距
    /// </summary>
    public double ItemHeight { get; set; }
    
    public RateToken()
        : base(ID)
    {
    }

    protected internal override void CalculateFromAlias()
    {
        base.CalculateFromAlias();
        Spacing = 5;
        ItemWidth = 60;
        ItemHeight = 60;
    }
}