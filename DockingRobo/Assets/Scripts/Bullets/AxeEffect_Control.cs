using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeEffect_Control : MonoBehaviour
{
    private Rigidbody rb;
    float time = 0;
    int power = 20;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 rotation = gameObject.transform.localRotation.eulerAngles;
        rotation.y += 70;
        gameObject.transform.localRotation = Quaternion.Euler(rotation);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 0.5f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(10, rb.velocity.y, -3);
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
            Destroy(gameObject);
        }
    }
}
