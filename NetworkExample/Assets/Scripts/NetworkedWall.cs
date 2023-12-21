using Mirror;

public class NetworkedWall : NetworkBehaviour
{
    public static NetworkedWall _instance;

    [SyncVar(hook = nameof(SetIsEnabled))]
    public bool sync_isEnabled = true;

    private void Awake()
    {
        _instance = this;
        _instance.gameObject.SetActive(false);
    }

    [Command(requiresAuthority = false)]
    public void SetActiveNetworked()
    {
        sync_isEnabled = !sync_isEnabled;
        //ToggleActiveRPC();
    }

    [ClientRpc]
    public void ToggleActiveRPC()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    void SetIsEnabled(bool oldValue, bool newValue)
    {
        gameObject.SetActive(newValue);
    }
}
