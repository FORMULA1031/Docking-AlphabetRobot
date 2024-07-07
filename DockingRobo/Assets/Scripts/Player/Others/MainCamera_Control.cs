using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera_Control : MonoBehaviour
{
    GameObject Player;
    Vector3 offset;
    Vector3 width;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("ZeroRobot");
        offset = transform.position - Player.transform.position;
    }

    private void FixedUpdate()
    {
        if (Player != null)
        {
            transform.position = Player.transform.position + offset;
        }
        transform.position = new Vector3(0, 6, transform.position.z);
    }
}
