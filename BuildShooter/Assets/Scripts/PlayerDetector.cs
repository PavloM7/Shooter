using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PlayerDetector : MonoBehaviour
{
    [Range(1, 360)]
    [SerializeField] private int radius;

    [HideInInspector] public GameObject target;
    [SerializeField] private LayerMask layerMask;

    private NavMeshAgent agent;

    private EnemyDeath enemyDeath;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("GameController");
        enemyDeath = GetComponent<EnemyDeath>();

        StartCoroutine(CheckTagretCoroutine());
    }

    private void Start()
    {
        
    }

    private IEnumerator CheckTagretCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (CheckTarget())
            {
                MoveToTarget();
            }
        }
    }

    private bool CheckTarget()
    {
        if (Physics.OverlapSphere(transform.position, radius, layerMask).Length != 0)
        {
            Vector3 directionToTarget = (target.transform.position - transform.position);

            if (Vector3.Angle(transform.forward, directionToTarget) <= radius / 2)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, directionToTarget, out hit, radius, layerMask))
                {
                    if (hit.collider.gameObject == target)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void MoveToTarget()
    {
        if (enemyDeath._isAlive) agent.SetDestination(target.transform.position);
    }
}