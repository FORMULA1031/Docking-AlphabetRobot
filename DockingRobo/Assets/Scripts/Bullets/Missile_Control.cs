using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_Control : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject BombBlast;
    int power = 80;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * power, ForceMode.Impulse);
    }

    public void Change_Power(int _power)
    {
        power = _power;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(BombBlast, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
