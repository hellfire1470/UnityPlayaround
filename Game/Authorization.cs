using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Network;
using Network.Packages;
using Network.Data;

public class User
{

    private UdpClient _client;

    public Character NetworkCharacter { get; private set; }

    public User(UdpClient client)
    {
        _client = client;
        client.DataReceived += OnDataReceived;
    }

    ~User()
    {
        _client.DataReceived -= OnDataReceived;
    }

    private void OnDataReceived(object sender, NetworkReceiveEventArgs e)
    {
        NetworkCharacter = NetworkHelper.Deserialize<Character>(e.Data);
    }
}

public class UserDataArgs : System.EventArgs
{
    public Account UserData { get; set; }
}

public class CharacterSelectArgs : System.EventArgs
{
    public long Id { get; set; }
}

public class UserCharactersArgs : System.EventArgs
{
    public Character[] UserCharactersData { get; set; }
}

public class Authorization
{

    enum AuthorizationStatus
    {
        None,
        LoggedIn,
        SelectedCharacter
    }

    private UdpClient _networkClient;
    //private User user;

    public event System.EventHandler<UserDataArgs> LoggedIn;
    public event System.EventHandler LoginFail;
    public event System.EventHandler<CharacterSelectArgs> CharacterSelected;
    public event System.EventHandler<UserCharactersArgs> UserCharactersReceived;
    public event System.EventHandler LoggedOut;
    public event System.EventHandler LogoutFail;

    private AuthorizationStatus _authorizationStatus = AuthorizationStatus.None;

    public Authorization(UdpClient networkClient)
    {
        _networkClient = networkClient;
        networkClient.DataReceived += OnDataReceived;
    }

    ~Authorization()
    {
        _networkClient.DataReceived -= OnDataReceived;
    }

    public void SelectChar(long Id)
    {
        _networkClient.SendBytes(NetworkHelper.Serialize(new SelectCharacterRequest() { CharacterId = Id }));
    }

    public void Login(string username, string password)
    {
        byte[] authMessage = NetworkHelper.Serialize(new LoginRequest()
        {
            Username = username,
            Password = password
        });
        _networkClient.SendBytes(authMessage);
    }

    public void Logout()
    {
        if (_authorizationStatus != AuthorizationStatus.None)
        {
            _networkClient.SendBytes(NetworkHelper.Serialize(new LogoutRequest() { Status = LogoutStatus.TitleScreen }));
        }
    }


    public void RequestUserCharacters()
    {
        if (_authorizationStatus == AuthorizationStatus.LoggedIn)
        {
            _networkClient.SendBytes(NetworkHelper.Serialize(new GetAccountCharactersRequest()));
        }
    }

    protected virtual void OnDataReceived(object sender, NetworkReceiveEventArgs e)
    {

        switch (NetworkHelper.GetPackageType(e.Data))
        {
            case PackageType.Login:
                LoginResponse loginResponse = NetworkHelper.Deserialize<LoginResponse>(e.Data);
                if (loginResponse.Success)
                {
                    OnLoggedIn(loginResponse.Data);
                }
                else
                {
                    OnLoginFailed();
                }
                break;

            case PackageType.Logout:
                LogoutResponse logoutResponse = NetworkHelper.Deserialize<LogoutResponse>(e.Data);

                if (logoutResponse.Success)
                {
                    OnLoggedOut(logoutResponse.Status);
                }
                else
                {
                    OnLogoutFailed();
                }
                break;
            case PackageType.SelectCharacter:
                SelectCharacterResponse response = NetworkHelper.Deserialize<SelectCharacterResponse>(e.Data);
                if (response.Success)
                {
                    OnCharacterSelected(response.CharacterId);
                }
                break;
            case PackageType.GetAccountCharacters:
                GetAccountCharactersResponse charSelectionResponse = NetworkHelper.Deserialize<GetAccountCharactersResponse>(e.Data);
                if (charSelectionResponse.Success)
                {
                    OnUserCharactersReceived(charSelectionResponse.Characters);
                }
                break;
        }
        //} catch (System.Exception ex){
        //	Debug.LogError (ex.Message);
        //}
    }

    protected virtual void OnUserCharactersReceived(Character[] userCharactersData)
    {
        if (_authorizationStatus == AuthorizationStatus.LoggedIn)
        {
            if (UserCharactersReceived != null)
            {
                UserCharactersReceived(this, new UserCharactersArgs() { UserCharactersData = userCharactersData });
            }
        }
    }

    protected virtual void OnCharacterSelected(long Id)
    {
        if (_authorizationStatus == AuthorizationStatus.LoggedIn)
        {
            _authorizationStatus = AuthorizationStatus.SelectedCharacter;
            if (CharacterSelected != null)
            {
                CharacterSelected(this, new CharacterSelectArgs() { Id = Id });
            }
        }
    }

    protected virtual void OnLoggedIn(Account data)
    {
        if (_authorizationStatus == AuthorizationStatus.None)
        {
            _authorizationStatus = AuthorizationStatus.LoggedIn;
            if (LoggedIn != null)
            {
                LoggedIn(this, new UserDataArgs() { UserData = data });
            }
        }
    }

    protected virtual void OnLoginFailed()
    {
        if (LoginFail != null)
        {
            LoginFail(this, new System.EventArgs());
        }
    }

    protected virtual void OnLoggedOut(LogoutStatus status)
    {
        if (_authorizationStatus != AuthorizationStatus.None)
        {
            _authorizationStatus = AuthorizationStatus.None;
            if (LoggedOut != null)
            {
                LoggedOut(this, new System.EventArgs());
            }
        }
    }

    protected virtual void OnLogoutFailed()
    {
        if (LogoutFail != null)
        {
            LogoutFail(this, new System.EventArgs());
        }
    }
}
