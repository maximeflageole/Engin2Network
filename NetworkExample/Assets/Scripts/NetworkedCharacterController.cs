using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class NetworkedCharacterController : NetworkBehaviour
{
    private const float MINIMUM_MOVEMENT_SPEED = 0.01f;
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
    }
}