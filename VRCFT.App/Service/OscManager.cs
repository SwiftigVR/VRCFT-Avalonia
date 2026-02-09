using LucHeart.CoreOSC;
using System.Net;
using System.Runtime.CompilerServices;

namespace VRCFT.App.Service;

public class OscManager
{
    #region Send

    private const int _SendingPort = 9000;

    public OscSender Sender
    {
        get
        {
            if (field == null)
            {
                var endPoint = new IPEndPoint(IPAddress.Loopback, _SendingPort);
                field = new OscSender(endPoint);
            }

            return field;
        }
    }

    private const string _BasePrefix = "/avatar/parameters/";

    /// <summary>
    /// Sends an OSC message with the parameter name derived from the caller member name.
    /// </summary>
    public void SendMessage(object? value, [CallerMemberName] string? parameterName = null)
    {
        if (string.IsNullOrEmpty(parameterName) || value == null)
            return;

        string fullParameter = _BasePrefix;

        if (!string.IsNullOrEmpty(ConfigManager.Config.OscParamterPrefix))
            fullParameter += ConfigManager.Config.OscParamterPrefix + "/";

        fullParameter += "v2/" + parameterName;

        var message = new OscMessage(fullParameter, [value]);
        Sender.SendAsync(message.GetBytes());
    }

    /// <summary>
    /// Sends an OSC message with the specified parameter name.
    /// </summary>
    public void SendMessage(string parameterName, object? value)
    {
        if (string.IsNullOrEmpty(parameterName) || value == null)
            return;

        string fullParameter = _BasePrefix;

        if (!string.IsNullOrEmpty(ConfigManager.Config.OscParamterPrefix))
            fullParameter += ConfigManager.Config.OscParamterPrefix + "/";

        fullParameter += "v2/" + parameterName;

        var message = new OscMessage(fullParameter, [value]);
        Sender.SendAsync(message.GetBytes());
    }

    #endregion

    #region Listen

    private const int _ListeningPort = 9001;

    public OscListener Listener
    {
        get
        {
            if (field == null)
            {
                var endPoint = new IPEndPoint(IPAddress.Loopback, _ListeningPort);
                field = new OscListener(endPoint);
            }

            return field;
        }
    }

    #endregion
}