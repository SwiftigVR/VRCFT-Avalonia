using LucHeart.CoreOSC;
using System.Net;
using System.Runtime.CompilerServices;

namespace VRCFT.App.Service;

public class OscManager
{
    public bool Enabled { get; set; } = false;

    private const int _SendingPort = 9000;
    private const int _ListeningPort = 9001;

    #region Send

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

    public void SendParameterMessage(object value, [CallerMemberName] string? parameterName = null)
    {
        if (!string.IsNullOrEmpty(parameterName))
            SendMessage(parameterName, value);
    }

    private const string _BasePrefix = "/avatar/parameters/";

    private void SendMessage(string parameterName, object value)
    {
        if (!Enabled)
            return;

        string fullParameter = _BasePrefix + "v2/" + parameterName;
        var message = new OscMessage(fullParameter, [value]);

        Sender.SendAsync(message.GetBytes());
    }

    #endregion

    #region Listen

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