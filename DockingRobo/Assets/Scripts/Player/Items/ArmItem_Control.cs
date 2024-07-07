using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmItem_Control : MonoBehaviour
{
    GameObject Player;
    public GameObject weapon;
    public GameObject weapon2;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("ZeroRobot");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 3, 0));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.GetComponent<Core_Control>().AdditionalEquipment("arm", weapon, weapon2);
            Destroy(gameObject);
        }
    }
}
