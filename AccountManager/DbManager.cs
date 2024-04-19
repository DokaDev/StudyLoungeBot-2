using AuthorizeKey.Database;

namespace AccountManager;

public class DbManager {
    private readonly string _host;
    private readonly uint _port;
    private readonly string _userName;
    private readonly string _password;

    public DbManager() {
        _host = Authorize.Host;
        _port = Authorize.Port;
        _userName = Authorize.UserName;
        _password = Authorize.Password;
    }
}