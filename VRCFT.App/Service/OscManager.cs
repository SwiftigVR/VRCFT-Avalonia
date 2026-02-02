using LucHeart.CoreOSC;
using System;
using System.Net;

namespace VRCFT.App.Service;

public class OscManager
{
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

    private const string _BasePrefix = "/avatar/parameters/";

    public void SendMessage(string parameterName, object value)
    {
        string fullParameter = _BasePrefix + parameterName;
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