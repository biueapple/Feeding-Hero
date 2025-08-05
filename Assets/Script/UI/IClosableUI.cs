
public interface IClosableUI
{
    public void Open();
    public void Close();
    bool IsOpen { get; }
}
