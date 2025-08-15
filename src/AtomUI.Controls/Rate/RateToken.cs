using AtomUI.Theme.TokenSystem;

namespace AtomUI.Controls;

[ControlDesignToken]
internal class RateToken : AbstractControlDesignToken
{
    public const string ID = "Rate";
    
    public RateToken()
        : base(ID)
    {
    }

    protected internal override void CalculateFromAlias()
    {
        base.CalculateFromAlias();
    }
}