using UnityEngine;


public class Bubble : MonoBehaviour
{
    public float underWaterDrag = 3f;
    public float underWaterAngularDrag = 1f;

    public float overWaterDrag = 1f;
    public float overWaterAngularDrag = 0.05f;

    public Rigidbody m_rigidBody;

    public float floatingPower = 15f;
    public bool underwater = false;

    public float waveHeight = 0;

    public float waveFrequency = 0.5f;
    public float waveAmplitude = 1f;
    public float waveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
       Physics2D.gravity = new Vector3(0, -2f, 0);
        //m_rigidBody =  gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        waveHeight = Generator(transform.position.x);
        float diff = transform.position.y - waveHeight;

        if (diff < 0)
        {
            m_rigidBody.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(diff), transform.position, ForceMode.Force);
            if (!underwater)
            {
                underwater = true;
                SwitchState(true);
            }
        }
        else if (underwater)
        {
            underwater = false;
            SwitchState(false);
        }
        m_rigidBody.AddForce(-transform.right * 0.05f, ForceMode.Acceleration);
    }


    void SwitchState(bool isUnderwater)
    {
        if (isUnderwater)
        {
            m_rigidBody.drag = underWaterDrag;
            m_rigidBody.angularDrag = underWaterAngularDrag;
        }
        else
        {
            m_rigidBody.drag = overWaterDrag;
            m_rigidBody.angularDrag = overWaterAngularDrag;
        }
    }

    // Каст лучей с проверкой дальности
    //public void GetWaveHeight()
    //{
    //    //Каст лучей вниз
    //    if (!Physics.Raycast(gameObject.transform.position, -gameObject.transform.up, maxDownDistance))
    //    {
    //        if (maxDownDistance < 25)
    //            maxDownDistance += StepFind;
    //    }
    //    else
    //    {
    //        waveHeight = gameObject.transform.position.y - maxDownDistance;
    //        maxDownDistance = 0f;
    //    }

    //    ////Каст лучей вверх
    //    if (!Physics.Raycast(gameObject.transform.position, gameObject.transform.up, maxUpDistance))
    //    {
    //        if (maxUpDistance < 25)
    //            maxUpDistance += StepFind;
    //    }
    //    else
    //    {
    //        waveHeight = gameObject.transform.position.y + maxUpDistance;
    //        maxUpDistance = 0f;
    //    }
    //}

    private float Generator(float x)
    {
        return (waveAmplitude * Mathf.Sin(x * waveFrequency - waveSpeed * Time.time));
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(transform.position, transform.position - new Vector3(0f, maxDownDistance, 0f));
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position, transform.position + new Vector3(0f, maxUpDistance, 0f));
    //}
}
