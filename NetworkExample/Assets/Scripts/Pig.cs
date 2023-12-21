using Mirror;
using TMPro;
using UnityEngine;

public class Pig : NetworkBehaviour, IDamageable
{
    [SerializeField]
    private TextMeshPro m_text;


    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with " + collision.gameObject);
        LayerMask mask = LayerMask.NameToLayer("WeaponCharacter");
        if (collision.gameObject.layer == mask)
        {
            InflictDamageCommand();
        }
    }

    [Command(requiresAuthority = false)]
    public void InflictDamageCommand()
    {
        //Normalement, on effectue ici un sanity check
        OnDamageReceivedRPC();
    }

    [ClientRpc]
    public void OnDamageReceivedRPC()
    {
        m_text.gameObject.SetActive(true);
    }
}
