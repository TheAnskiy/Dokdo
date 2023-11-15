using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Global : MonoBehaviour
{
    public static Global Instance { get; private set; }
    [field: SerializeField] public GameObject MainShip {  get; private set; }
    [field: SerializeField] public Camera MainCamera { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
