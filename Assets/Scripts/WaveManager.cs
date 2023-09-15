using UnityEngine;

//[RequireComponent(typeof(MeshFilter))]
//[RequireComponent(typeof(MeshRenderer))]

public class WaveManager : MonoBehaviour
{
    //public static float waveOffset = 0f;
    //public static float waveLength = 2f;
    //public static float waveAmplitude = 1f;
    //public static float waveSpeed = 1f;

    private MeshFilter meshFilter;

    // Start is called before the first frame update
    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    waveOffset += Time.deltaTime * waveSpeed;
    //    Vector3[] verticles = meshFilter.mesh.vertices;
    //    for (int i = 0; i < verticles.Length; i++)
    //    {
    //        verticles[i].y = Generator(transform.position.x + verticles[i].x);
    //    }
    //    meshFilter.mesh.vertices = verticles;
    //    meshFilter.mesh.RecalculateNormals();
    //}

    //private float Generator(float x)
    //{

    //    return waveAmplitude * Mathf.Sin(x / waveLength + waveOffset);
    //}
}
