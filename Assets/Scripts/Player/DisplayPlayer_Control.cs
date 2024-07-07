using UnityEngine;

public class DisplayPlayer_Control : MonoBehaviour
{
    string core;    //設定しているコアパーツ
    string head;    //装備しているヘッドパーツ
    string arm; //装備しているアームパーツ
    string backpack;    //装備しているバックパックパーツ
    string core_present;    //現在設定しているコアパーツ
    string head_present;    //現在装備しているヘッドパーツ
    string arm_present; //現在装備しているアームパーツ
    string backpack_present;    //現在装備しているバックパックパーツ
    GameObject Core_part;   //設定するコアパーツの生成する座標オブジェクト
    GameObject Head_part;   //装備するヘッドパーツの生成する座標オブジェクト
    GameObject ArmL_part;   //装備する左アームパーツの生成する座標オブジェクト
    GameObject ArmR_part;   //装備する右アームパーツの生成する座標オブジェクト
    GameObject Backpack_part;   //装備するバックパックパーツの生成する座標オブジェクト
    GameObject CorePart_Instance;   //生成したコアパーツ
    GameObject HeadPart_Instance;   //生成したヘッドパーツ
    GameObject ArmPartL_Instance;   //生成した左アームパーツ
    GameObject ArmPartR_Instance;   //生成した右アームパーツ
    GameObject BackpackPart_Instance;   //生成したバックパックパーツ
    public GameObject A_left;   //アックスパーツの左腕
    public GameObject A_right;  //アックスパーツの右腕
    public GameObject B_left;   //バズーカパーツの左腕
    public GameObject B_right;  //バズーカパーツの右腕
    public GameObject C;    //キャノンパーツ
    public GameObject D;    //ドリルパーツ
    public GameObject E;    //エンハンスメントパーツ
    public GameObject F;    //ファイヤーパーツ
    public GameObject G_left;   //ガトリングパーツの左腕
    public GameObject G_right;  //ガトリングパーツの右腕
    public GameObject H;    //ハンマーパーツ
    public GameObject I;    //インビジブルパーツ
    public GameObject J;    //ジェットパーツ
    public GameObject K_left;   //ナイフパーツの左腕
    public GameObject K_right;  //ナイフパーツの右腕
    public GameObject L;    //レーザーパーツ
    public GameObject M;    //ミサイルパーツ
    public GameObject N;    //ニードルパーツ
    public GameObject O;    //ハット―パーツ
    public GameObject P;    //ピストルパーツ
    public GameObject Q;    //クアンタムパーツ
    public GameObject R;    //リペアパーツ
    public GameObject S;    //シールドパーツ
    public GameObject T;    //テールパーツ
    public GameObject U;    //ユーズアリーパーツ
    public GameObject V;    //バリエーションパーツ
    public GameObject W;    //ウィングパーツ
    public GameObject X;    //エクストラパーツ
    public GameObject Y;    //エクストラパーツ
    public GameObject Z;    //初期コアパーツ

    // Start is called before the first frame update
    void Start()
    {
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
        Display_Core(); //設定するコアパーツの表示
        Display_Head(); //装備するヘッドパーツの表示
        Display_Arm();  //装備するアームパーツの表示
        Display_Backpack(); //装備するバックパックパーツの表示
    }

    private void FixedUpdate()  //自オブジェクトの回転処理
    {
        transform.Rotate(new Vector3(0, -2, 0));
    }

    void Display_Core() //設定するコアパーツの表示
    {
        if (core_present != core)
        {
            if (Core_part.transform != null)    //古いパーツの削除
            {
                for (int i = 0; Core_part.transform.childCount > i; i++)
                {
                    if (Core_part.transform.GetChild(i).gameObject != gameObject)
                        Destroy(Core_part.transform.GetChild(i).gameObject);
                }
            }
            switch (core)   //コアパーツの生成
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

    void Display_Head() //装備するヘッドパーツの表示
    {
        if (head_present != head)
        {
            if (Head_part.transform != null)    //古いパーツの削除
            {
                for (int i = 0; Head_part.transform.childCount > i; i++)
                {
                    if (Head_part.transform.GetChild(i).gameObject != gameObject)
                        Destroy(Head_part.transform.GetChild(i).gameObject);
                }
            }
            switch (head)   //ヘッドパーツの生成
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

    void Display_Arm()  //装備するアームパーツの表示
    {
        if (arm_present != arm)
        {
            if (ArmL_part.transform != null)    //古い左アームパーツの削除
            {
                for (int i = 0; ArmL_part.transform.childCount > i; i++)
                {
                    if (ArmL_part.transform.GetChild(i).gameObject != gameObject)
                        Destroy(ArmL_part.transform.GetChild(i).gameObject);
                }
            }
            if (ArmR_part.transform != null)    //古い右アームパーツの削除
            {
                for (int i = 0; ArmR_part.transform.childCount > i; i++)
                {
                    if (ArmR_part.transform.GetChild(i).gameObject != gameObject)
                        Destroy(ArmR_part.transform.GetChild(i).gameObject);
                }
            }
            switch (arm)    //アームパーツの生成
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

    void Display_Backpack() //装備するバックパックパーツの表示
    {
        if (backpack_present != backpack)
        {
            if (Backpack_part.transform != null)    //古いパーツの削除
            {
                for (int i = 0; Backpack_part.transform.childCount > i; i++)
                {
                    if (Backpack_part.transform.GetChild(i).gameObject != gameObject)
                        Destroy(Backpack_part.transform.GetChild(i).gameObject);
                }
            }
            switch (backpack)   //バックパックパーツの生成
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
