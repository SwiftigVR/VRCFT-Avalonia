namespace VRCFT.Extension.MessageBox;

//[Flags] // 0 1 2 4 8 16 ...
public enum MessageBoxButtons
{
    OK = 0,
    OkCancel = 1,
    YesNo = 2,
}

public enum MessageBoxIcon
{
    None,
    Check,
    Question,
    Warning,
    Error,
}

public enum MessageBoxResult
{
    No,
    Cancel,
    Yes,
    Ok,
}