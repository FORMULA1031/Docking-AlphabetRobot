using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPlayer_Control : MonoBehaviour
{
    string core;
    string head;
    string arm;
    string backpack;
    string core_present;
    string head_present;
    string arm_present;
    string backpack_present;
    Player_Settings Player_Settings;
    GameObject Core_part;
    GameObject Head_part;
    GameObject ArmL_part;
    GameObject ArmR_part;
    GameObject Backpack_part;
    GameObject CorePart_Instance;
    GameObject HeadPart_Instance;
    GameObject ArmPartL_Instance;
    GameObject ArmPartR_Instance;
    GameObject BackpackPart_Instance;
    public GameObject A_left;
    public GameObject A_right;
    public GameObject B_left;
    public GameObject B_right;
    public GameObject C;
    public GameObject D;
    public GameObject E;
    public GameObject F;
    public GameObject G_left;
    public GameObject G_right;
    public GameObject H;
    public GameObject I;
    public GameObject J;
    public GameObject K_left;
    public GameObject K_right;
    public GameObject L;
    public GameObject M;
    public GameObject N;
    public GameObject O;
    public GameObject P;
    public GameObject Q;
    public GameObject R;
    public GameObject S;
    public GameObject T;
    public GameObject U;
    public GameObject V;
    public GameObject W;
    public GameObject X;
    public GameObject Y;
    public GameObject Z;

    // Start is called before the first frame update
    void Start()
    {
        Player_Settings = GameObject.Find("EventSystem").GetComponent<Player_Settings>();
        Core_part = gameObject.transform.Find("Core").gameObject;
        Head_part = gameObject.transform.Find("Head").gameObject;
        ArmL_part = gameObject.transform.Find("Arm_left").gameObject;
        ArmR_part = gameObject.transform.Find("Arm_right").gameObject;
        Backpack_part = gameObject.transform.Find("Backpack").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        core = Player_Settings.core_setting;
        head = Player_Settings.head_setting;
        arm = Player_Settings.arm_setting;
        backpack = Player_Settings.backpack_setting;
        Display_Core();
        Display_Head();
        Display_Arm();
        Display_Backpack();
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, -2, 0));
    }

    void Display_Core()
    {
        if (core_present != core)
        {
            if (Core_part.transform != null)
            {
                for (int i = 0; Core_part.transform.childCount > i; i++)
                {
                    if (Core_part.transform.GetChild(i).gameObject != gameObject)
                        Destroy(Core_part.transform.GetChild(i).gameObject);
                }
            }
            switch (core)
            {
                case "O":
                    CorePart_Instance = Instantiate(O, Core_part.transform.position, transform.rotation);
                    break;
                case "Q":
                    CorePart_Instance = Instantiate(Q, Core_part.transform.position, transform.rotation);
                    break;
                case "U":
                    CorePart_Instance = Instantiate(U, Core_part.transform.position, transform.rotation);
                    break;
                case "V":
                    CorePart_Instance = Instantiate(V, Core_part.transform.position, transform.rotation);
                    break;
                case "X":
                    CorePart_Instance = Instantiate(X, Core_part.transform.position, transform.rotation);
                    break;
                case "Y":
                    CorePart_Instance = Instantiate(Y, Core_part.transform.position, transform.rotation);
                    break;
                case "Z":
                    CorePart_Instance = Instantiate(Z, Core_part.transform.position, transform.rotation);
                    break;
            }
            if (CorePart_Instance != null)
            {
                CorePart_Instance.transform.parent = Core_part.transform;
            }
            core_present = core;
        }
    }

    void Display_Head()
    {
        if (head_present != head)
        {
            if (Head_part.transform != null)
            {
                for (int i = 0; Head_part.transform.childCount > i; i++)
                {
                    if (Head_part.transform.GetChild(i).gameObject != gameObject)
                        Destroy(Head_part.transform.GetChild(i).gameObject);
                }
            }
            switch (head)
            {
                case "C":
                    HeadPart_Instance = Instantiate(C, Head_part.transform.position, transform.rotation);
                    break;
                case "D":
                    HeadPart_Instance = Instantiate(D, Head_part.transform.position, transform.rotation);
                    break;
                case "F":
                    HeadPart_Instance = Instantiate(F, Head_part.transform.position, transform.rotation);
                    break;
                case "O":
                    HeadPart_Instance = Instantiate(O, Head_part.transform.position, transform.rotation);
                    break;
                case "L":
                    HeadPart_Instance = Instantiate(L, Head_part.transform.position, transform.rotation);
                    break;
                case "R":
                    HeadPart_Instance = Instantiate(R, Head_part.transform.position, transform.rotation);
                    break;
                case "H":
                    HeadPart_Instance = Instantiate(H, Head_part.transform.position, transform.rotation);
                    break;
                case "null":
                    break;
            }
            if (HeadPart_Instance != null)
            {
                HeadPart_Instance.transform.parent = Head_part.transform;
            }
            head_present = head;
        }
    }

    void Display_Arm()
    {
        if (arm_present != arm)
        {
            if (ArmL_part.transform != null)
            {
                for (int i = 0; ArmL_part.transform.childCount > i; i++)
                {
                    if (ArmL_part.transform.GetChild(i).gameObject != gameObject)
                        Destroy(ArmL_part.transform.GetChild(i).gameObject);
                }
            }
            if (ArmR_part.transform != null)
            {
                for (int i = 0; ArmR_part.transform.childCount > i; i++)
                {
                    if (ArmR_part.transform.GetChild(i).gameObject != gameObject)
                        Destroy(ArmR_part.transform.GetChild(i).gameObject);
                }
            }
            switch (arm)
            {
                case "A":
                    ArmPartL_Instance = Instantiate(A_left, ArmL_part.transform.position, transform.rotation);
                    ArmPartR_Instance = Instantiate(A_right, ArmR_part.transform.position, transform.rotation);
                    break;
                case "B":
                    ArmPartL_Instance = Instantiate(B_left, ArmL_part.transform.position, transform.rotation);
                    ArmPartR_Instance = Instantiate(B_right, ArmR_part.transform.position, transform.rotation);
                    break;
                case "G":
                    ArmPartL_Instance = Instantiate(G_left, ArmL_part.transform.position, transform.rotation);
                    ArmPartR_Instance = Instantiate(G_right, ArmR_part.transform.position, transform.rotation);
                    break;
                case "K":
                    ArmPartL_Instance = Instantiate(K_left, ArmL_part.transform.position, transform.rotation);
                    ArmPartR_Instance = Instantiate(K_right, ArmR_part.transform.position, transform.rotation);
                    break;
                case "P":
                    ArmPartL_Instance = Instantiate(P, ArmL_part.transform.position, transform.rotation);
                    ArmPartR_Instance = Instantiate(P, ArmR_part.transform.position, transform.rotation);
                    break;
                case "H":
                    ArmPartL_Instance = Instantiate(H, ArmL_part.transform.position, transform.rotation);
                    ArmPartR_Instance = Instantiate(H, ArmR_part.transform.position, transform.rotation);
                    break;
                case "N":
                    ArmPartL_Instance = Instantiate(N, ArmL_part.transform.position, transform.rotation);
                    ArmPartR_Instance = Instantiate(N, ArmR_part.transform.position, transform.rotation);
                    break;
                case "null":
                    break;
            }
            if (ArmPartL_Instance != null)
            {
                ArmPartL_Instance.transform.parent = ArmL_part.transform;
            }
            if (ArmPartR_Instance != null)
            {
                ArmPartR_Instance.transform.parent = ArmR_part.transform;
            }
            arm_present = arm;
        }
    }

    void Display_Backpack()
    {
        if (backpack_present != backpack)
        {
            if (Backpack_part.transform != null)
            {
                for (int i = 0; Backpack_part.transform.childCount > i; i++)
                {
                    if (Backpack_part.transform.GetChild(i).gameObject != gameObject)
                        Destroy(Backpack_part.transform.GetChild(i).gameObject);
                }
            }
            switch (backpack)
            {
                case "E":
                    BackpackPart_Instance = Instantiate(E, Backpack_part.transform.position, transform.rotation);
                    break;
                case "I":
                    BackpackPart_Instance = Instantiate(I, Backpack_part.transform.position, transform.rotation);
                    break;
                case "J":
                    BackpackPart_Instance = Instantiate(J, Backpack_part.transform.position, transform.rotation);
                    break;
                case "M":
                    BackpackPart_Instance = Instantiate(M, Backpack_part.transform.position, transform.rotation);
                    break;
                case "S":
                    BackpackPart_Instance = Instantiate(S, Backpack_part.transform.position, transform.rotation);
                    break;
                case "T":
                    BackpackPart_Instance = Instantiate(T, Backpack_part.transform.position, transform.rotation);
                    break;
                case "W":
                    BackpackPart_Instance = Instantiate(W, Backpack_part.transform.position, transform.rotation);
                    break;
                case "null":
                    break;
            }
            if (BackpackPart_Instance != null)
            {
                BackpackPart_Instance.transform.parent = Backpack_part.transform;
            }
            backpack_present = backpack;
        }
    }
}
