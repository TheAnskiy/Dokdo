using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class PathFinder : MonoBehaviour
{
    [SerializeField] private Transform _target;
    public NavMeshPath Path { get; private set; }

    private const float HeightSeaBottom = -20.0f;
    private const float TimeUpdatePath = 0.5f;

    private NavMeshAgent _agent;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        Path = new NavMeshPath();
    }

    void Start()
    {
        StartCoroutine(UpdatePath());
    }

    private IEnumerator UpdatePath()
    {
        while (_target != null)
        {
            transform.SetPositionAndRotation(
                new Vector3(transform.parent.position.x, HeightSeaBottom, transform.parent.position.z), 
                Quaternion.identity);

            Vector3 targetPosition = _target.position;
            targetPosition.y = HeightSeaBottom;
            _agent.CalculatePath(targetPosition, Path);

            yield return new WaitForSeconds(TimeUpdatePath);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        if (newTarget == null)
            return;

        if (_target == newTarget)
            return;

        _target = newTarget;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Path == null)
            return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < Path.corners.Length; i++)
        {
            Gizmos.DrawSphere(Path.corners[i], 0.5f);
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < Path.corners.Length - 1; i++)
        {
            Gizmos.DrawLine(Path.corners[i], Path.corners[i + 1]);
        }
    }
#endif
}
