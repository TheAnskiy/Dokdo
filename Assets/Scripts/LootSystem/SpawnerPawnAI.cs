using UnityEngine;

public class SpawnerPawnAI : MonoBehaviour
{
    [SerializeField] private TestAI _prefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(_prefab, transform.position, transform.rotation);
        }
    }

}
