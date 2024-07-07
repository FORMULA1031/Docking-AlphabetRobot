using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPosition_Stage1 : MonoBehaviour
{
    public GameObject AxeRobot;
    public GameObject BazookaRobot;
    public GameObject CannonRobot;
    public GameObject DrillRobot;
    public GameObject EnhancementRobot;
    public GameObject FRobot;
    public GameObject GRobot;
    public GameObject HRobot;
    public GameObject IRobot;
    public GameObject JetRobot;
    public GameObject KnifeRobot;
    public GameObject LRobot;
    public GameObject MRobot;
    public GameObject NRobot;
    public GameObject PRobot;
    public GameObject RRobot;
    public GameObject SRobot;
    public GameObject TailRobot;
    public GameObject WRobot;
    public GameObject HatRobot;

    Vector3[] positions = { new Vector3(0, 1, 0), new Vector3(-2.5f, 1, 30), new Vector3(2.5f, 1, 30),
        new Vector3(0, 1, 45), new Vector3(2.5f, 1, 60), new Vector3(-2.5f, 1, 80),
        new Vector3(0, 1, 115), new Vector3(0, 1, 130), new Vector3(2.5f, 1, 130),
        new Vector3(-2.5f, 1, 145), new Vector3(2.5f, 1, 160), new Vector3(2.5f, 1, 180),
        new Vector3(-2.5f, 1, 180), new Vector3(0, 1, 180), new Vector3(0, 1, 195),
        new Vector3(-2.5f, 1, 215), new Vector3(2.5f, 1, 215), new Vector3(0f, 1, 230)};
    Vector3 enemy_quaternion;
    public GameObject[] Stages = new GameObject[3];
    GameObject Player;
    int destination = 100;
    int instance_number = 1;

    // Start is called before the first frame update
    void Start()
    {
        for(int enemy_number = 0; enemy_number < positions.Length; enemy_number++)
        {
            GeneratingEnemies(positions[enemy_number]);
        }
        Player = GameObject.Find("ZeroRobot");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            if (Player.transform.position.z >= destination)
            {
                Debug.Log("ê∂ê¨" + instance_number);
                switch (instance_number)
                {
                    case 1:
                        Instantiate(Stages[0], new Vector3(0, 0, destination + 200), Quaternion.identity);
                        for (int enemy_number = 0; enemy_number < 4; enemy_number++)
                        {
                            positions[enemy_number] =
                                new Vector3(positions[enemy_number].x, positions[enemy_number].y, positions[enemy_number].z + 300);
                            GeneratingEnemies(positions[enemy_number]);
                        }
                        instance_number++;
                        break;
                    case 2:
                        Instantiate(Stages[1], new Vector3(0, 0, destination + 200), Quaternion.identity);
                        for (int enemy_number = 4; enemy_number < 10; enemy_number++)
                        {
                            positions[enemy_number] =
                                new Vector3(positions[enemy_number].x, positions[enemy_number].y, positions[enemy_number].z + 300);
                            GeneratingEnemies(positions[enemy_number]);
                        }
                        instance_number++;
                        break;
                    case 3:
                        Instantiate(Stages[2], new Vector3(0, 0, destination + 200), Quaternion.identity);
                        for (int enemy_number = 10; enemy_number < positions.Length; enemy_number++)
                        {
                            positions[enemy_number] =
                                new Vector3(positions[enemy_number].x, positions[enemy_number].y, positions[enemy_number].z + 300);
                            GeneratingEnemies(positions[enemy_number]);
                        }
                        instance_number = 1;
                        break;
                }
                destination += 100;
            }
        }
    }

    void GeneratingEnemies(Vector3 enemy_position)
    {
        switch(Random.Range(1, 21))
        {
            case 1:
                GameObject CR = Instantiate(CannonRobot, enemy_position, Quaternion.identity);
                CR.transform.position = new Vector3(CR.transform.position.x, CR.transform.position.y + 2, CR.transform.position.z);
                EnemyDirection(CR);
                break;
            case 2:
                GameObject GR = Instantiate(GRobot, enemy_position, Quaternion.identity);
                EnemyDirection(GR);
                break;
            case 3:
                GameObject JR = Instantiate(JetRobot, enemy_position, Quaternion.identity);
                EnemyDirection(JR);
                break;
            case 4:
                GameObject DR = Instantiate(DrillRobot, enemy_position, Quaternion.identity);
                EnemyDirection(DR);
                break;
            case 5:
                GameObject KR = Instantiate(KnifeRobot, enemy_position, Quaternion.identity);
                EnemyDirection(KR);
                break;
            case 6:
                GameObject TR = Instantiate(TailRobot, enemy_position, Quaternion.identity);
                EnemyDirection(TR);
                break;
            case 7:
                GameObject AR = Instantiate(AxeRobot, enemy_position, Quaternion.identity);
                EnemyDirection(AR);
                break;
            case 8:
                GameObject BR = Instantiate(BazookaRobot, enemy_position, Quaternion.identity);
                EnemyDirection(BR);
                break;
            case 9:
                GameObject ER = Instantiate(EnhancementRobot, enemy_position, Quaternion.identity);
                EnemyDirection(ER);
                break;
            case 10:
                GameObject FR = Instantiate(FRobot, enemy_position, Quaternion.identity);
                EnemyDirection(FR);
                break;
            case 11:
                GameObject HR = Instantiate(HRobot, enemy_position, Quaternion.identity);
                EnemyDirection(HR);
                break;
            case 12:
                GameObject IR = Instantiate(IRobot, enemy_position, Quaternion.identity);
                EnemyDirection(IR);
                break;
            case 13:
                GameObject LR = Instantiate(LRobot, enemy_position, Quaternion.identity);
                LR.transform.position = new Vector3(LR.transform.position.x, LR.transform.position.y + 2, LR.transform.position.z);
                EnemyDirection(LR);
                break;
            case 14:
                GameObject MR = Instantiate(MRobot, enemy_position, Quaternion.identity);
                MR.transform.position = new Vector3(MR.transform.position.x, MR.transform.position.y + 2, MR.transform.position.z);
                EnemyDirection(MR);
                break;
            case 15:
                GameObject NR = Instantiate(NRobot, enemy_position, Quaternion.identity);
                NR.transform.position = new Vector3(NR.transform.position.x, NR.transform.position.y + 2, NR.transform.position.z);
                EnemyDirection(NR);
                break;
            case 16:
                GameObject PR = Instantiate(PRobot, enemy_position, Quaternion.identity);
                EnemyDirection(PR);
                break;
            case 17:
                GameObject RR = Instantiate(RRobot, enemy_position, Quaternion.identity);
                EnemyDirection(RR);
                break;
            case 18:
                GameObject SR = Instantiate(SRobot, enemy_position, Quaternion.identity);
                EnemyDirection(SR);
                break;
            case 19:
                GameObject WR = Instantiate(WRobot, enemy_position, Quaternion.identity);
                EnemyDirection(WR);
                break;
            case 20:
                GameObject HatR = Instantiate(HatRobot, enemy_position, Quaternion.identity);
                EnemyDirection(HatR);
                break;
        }
    }

    void EnemyDirection(GameObject Robot)
    {
        enemy_quaternion = Robot.transform.localRotation.eulerAngles;
        enemy_quaternion.y += 90;
        Robot.transform.localRotation = Quaternion.Euler(enemy_quaternion);
    }
}
