using LucHeart.CoreOSC;
using System.Net;
using System.Runtime.CompilerServices;
using VRCFT.Extension;

namespace VRCFT.App.Service;

public class OscManager
{
    #region Send

    private int _usedSenderPort;

    public OscSender Sender
    {
        get
        {
            if (field == null || _usedSenderPort != ConfigManager.Config.OscSendingPort)
            {
                if (field != null)
                    field.Dispose();

                var endPoint = new IPEndPoint(IPAddress.Loopback, ConfigManager.Config.OscSendingPort);
                field = new OscSender(endPoint);

                _usedSenderPort = ConfigManager.Config.OscSendingPort;
            }

            return field;
        }
    }

    private const string _BasePrefix = "/avatar/parameters/";

    /// <summary>
    /// Sends an OSC message with the parameter name derived from the caller member name.
    /// </summary>
    public void SendMessage(object? value, [CallerMemberName] string? parameterName = null)
        => SendMessage(parameterName!, value);

    /// <summary>
    /// Sends an OSC message with the specified parameter name.
    /// </summary>
    public void SendMessage(string parameterName, object? value)
    {
        if (string.IsNullOrEmpty(parameterName) || value == null)
            return;

        string fullParameterPath = _BasePrefix;

        if (!string.IsNullOrEmpty(ConfigManager.Config.OscParamterPrefix))
            fullParameterPath += ConfigManager.Config.OscParamterPrefix + "/";

        fullParameterPath += "v2/" + parameterName;

        value = value switch
        {
            float floatValue => floatValue.LimitDecimal(afterDecimal: 4),
            double doubleValue => doubleValue.LimitDecimal(afterDecimal: 4),
            _ => value
        };

        var message = new OscMessage(fullParameterPath, [value]);
        Sender.SendAsync(message.GetBytes());
    }

    #endregion

    #region Listen

    private int _usedListeningPort;

    public OscListener Listener
    {
        get
        {
            if (field == null || _usedListeningPort != ConfigManager.Config.OscListeningPort)
            {
                if (field != null)
                    field.Dispose();

                var endPoint = new IPEndPoint(IPAddress.Loopback, ConfigManager.Config.OscListeningPort);
                field = new OscListener(endPoint);

                _usedListeningPort = ConfigManager.Config.OscListeningPort;
            }

            return field;
        }
    }

    #endregion
}