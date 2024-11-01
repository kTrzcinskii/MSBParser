namespace MSBParser;

public class ApplyHighlight
{
    private Action Higlight { get; }

    public ApplyHighlight(Action higlight)
    {
        Higlight = higlight;
    }

    public void Run()
    {
        Higlight.Invoke();
    }
}