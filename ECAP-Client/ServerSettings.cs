namespace ECAP_Client;
internal class ServerSettings
{
    public ServerSettings(string ip, string port)
    {
        Ip = ip;
        Port = port;
    }

    public string Ip { get; private set; } = "localhost";
    public string? Port { get; private set; }
}
