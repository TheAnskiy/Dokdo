using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public static Wave instance;

    public float amplitude = 1f;
    public float lenght = 2f;
    public float speed = 1f;
    public float offset = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed;
    }

    //public float GetWaveHeight(float x, float y)
    //{
    //    return Physics.Raycast(objects[0], transform., 10, 50);
    //}
}
