using Slash.Unity.DataBind.Core.Presentation;

public class TimeFormatter : DataProvider
{
    public DataBinding Argument;

    public override object Value
    {
        get
        {
            var argument = Argument.GetValue<int>();
            var minutes = argument / 60;
            var seconds = argument % 60;
            return $"{minutes:00}:{seconds:00}";
        }
    }

    public override void Init()
    {
        AddBinding(Argument);
    }

    protected override void UpdateValue()
    {
        OnValueChanged();
    }
}
