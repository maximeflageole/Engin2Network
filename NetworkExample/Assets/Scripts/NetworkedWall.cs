using Mirror;

public class NetworkedWall : NetworkBehaviour
{
    public static NetworkedWall _instance;

    private void Awake()
    {
        _instance = this;
        _instance.gameObject.SetActive(false);
    }

    [Command(requiresAuthority = false)]
    public void SetActiveNetworked()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
        //We have an issue to solve here!
    }
}
