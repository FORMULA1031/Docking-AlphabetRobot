using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall_Control : MonoBehaviour
{
    GameObject Player;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("ZeroRobot");
        offset = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Player != null)
        {
            transform.position = Player.transform.position + offset;
        }
        transform.position = new Vector3(0, 6, transform.position.z);
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
