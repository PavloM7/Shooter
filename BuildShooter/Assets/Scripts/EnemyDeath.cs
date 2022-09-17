using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]

public class EnemyDeath : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private int lifeHp = 100;

    private NavMeshAgent navMeshAgent;
    private PlayerDetector playerDetector;

    private GameObject _target;

    private NavMeshAgent agent;

    [HideInInspector] public bool _isAlive = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerDetector = GetComponent<PlayerDetector>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        _target = GetComponent<PlayerDetector>().target;
    }

    public void CheckShootAndDamage(int damage)
    {
        if (lifeHp <= 0) Death();
        else
        {
            DamageTheEnemy();
        }

        void DamageTheEnemy()
        {
            lifeHp -= damage; // FIX FIX FIX

            if (lifeHp <= 0)
            {
                Death();
                _isAlive = false;
            }

            if (lifeHp > 0) Debug.Log(lifeHp); // delete

            if (_isAlive) agent.SetDestination(_target.transform.position);
        }
    }

    private void Death()
    {
        playerDetector.enabled = false;
        navMeshAgent.enabled = false;

        _isAlive = false;

        rb.isKinematic = false;

        Debug.Log("Enemy dead");

        Destroy(gameObject, 10);
    }
}
