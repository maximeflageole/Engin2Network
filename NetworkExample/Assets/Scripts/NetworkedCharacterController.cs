using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class NetworkedCharacterController : NetworkBehaviour
{
    private const float MINIMUM_MOVEMENT_SPEED = 0.01f;

    [SerializeField]
    private BoxCollider m_shieldCollider;
    [SerializeField]
    private NavMeshAgent m_navmeshAgent;
    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private Camera m_camera;

    private void Start()
    {
        if (isLocalPlayer)
        {
            m_camera.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        float currentSpeed = m_navmeshAgent.velocity.magnitude;
        m_animator.SetBool("Moving", currentSpeed > MINIMUM_MOVEMENT_SPEED);
        m_animator.SetFloat("Velocity Z", currentSpeed / m_navmeshAgent.speed);

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            int layerMask = LayerMask.NameToLayer("Navmesh");

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layerMask))
            {
                m_navmeshAgent.SetDestination(hit.point);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            NetworkedWall._instance.SetActiveNetworked();
        }
        if (Input.GetMouseButtonDown(0))
        { 
            Attack();
        }
    }

    private void Attack()
    {
        var mr = m_shieldCollider.GetComponent<MeshRenderer>();
        mr.enabled = true;
        m_shieldCollider.enabled = true;
        m_animator.SetInteger("Action", 1);
        m_animator.SetInteger("TriggerNumber", 4);
        m_animator.SetTrigger("Trigger");
        StartCoroutine(DisableShield(0.5f));
    }

    private IEnumerator DisableShield(float timer = 1.0f)
    {
        yield return new WaitForSeconds(timer);
        m_shieldCollider.enabled = false;
        var mr = m_shieldCollider.GetComponent<MeshRenderer>();
        mr.enabled = false;
    }
}