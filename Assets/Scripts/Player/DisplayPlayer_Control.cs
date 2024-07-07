using UnityEngine;

public class DisplayPlayer_Control : MonoBehaviour
{
    string core;    //�ݒ肵�Ă���R�A�p�[�c
    string head;    //�������Ă���w�b�h�p�[�c
    string arm; //�������Ă���A�[���p�[�c
    string backpack;    //�������Ă���o�b�N�p�b�N�p�[�c
    string core_present;    //���ݐݒ肵�Ă���R�A�p�[�c
    string head_present;    //���ݑ������Ă���w�b�h�p�[�c
    string arm_present; //���ݑ������Ă���A�[���p�[�c
    string backpack_present;    //���ݑ������Ă���o�b�N�p�b�N�p�[�c
    GameObject Core_part;   //�ݒ肷��R�A�p�[�c�̐���������W�I�u�W�F�N�g
    GameObject Head_part;   //��������w�b�h�p�[�c�̐���������W�I�u�W�F�N�g
    GameObject ArmL_part;   //�������鍶�A�[���p�[�c�̐���������W�I�u�W�F�N�g
    GameObject ArmR_part;   //��������E�A�[���p�[�c�̐���������W�I�u�W�F�N�g
    GameObject Backpack_part;   //��������o�b�N�p�b�N�p�[�c�̐���������W�I�u�W�F�N�g
    GameObject CorePart_Instance;   //���������R�A�p�[�c
    GameObject HeadPart_Instance;   //���������w�b�h�p�[�c
    GameObject ArmPartL_Instance;   //�����������A�[���p�[�c
    GameObject ArmPartR_Instance;   //���������E�A�[���p�[�c
    GameObject BackpackPart_Instance;   //���������o�b�N�p�b�N�p�[�c
    public GameObject A_left;   //�A�b�N�X�p�[�c�̍��r
    public GameObject A_right;  //�A�b�N�X�p�[�c�̉E�r
    public GameObject B_left;   //�o�Y�[�J�p�[�c�̍��r
    public GameObject B_right;  //�o�Y�[�J�p�[�c�̉E�r
    public GameObject C;    //�L���m���p�[�c
    public GameObject D;    //�h�����p�[�c
    public GameObject E;    //�G���n���X�����g�p�[�c
    public GameObject F;    //�t�@�C���[�p�[�c
    public GameObject G_left;   //�K�g�����O�p�[�c�̍��r
    public GameObject G_right;  //�K�g�����O�p�[�c�̉E�r
    public GameObject H;    //�n���}�[�p�[�c
    public GameObject I;    //�C���r�W�u���p�[�c
    public GameObject J;    //�W�F�b�g�p�[�c
    public GameObject K_left;   //�i�C�t�p�[�c�̍��r
    public GameObject K_right;  //�i�C�t�p�[�c�̉E�r
    public GameObject L;    //���[�U�[�p�[�c
    public GameObject M;    //�~�T�C���p�[�c
    public GameObject N;    //�j�[�h���p�[�c
    public GameObject O;    //�n�b�g�\�p�[�c
    public GameObject P;    //�s�X�g���p�[�c
    public GameObject Q;    //�N�A���^���p�[�c
    public GameObject R;    //���y�A�p�[�c
    public GameObject S;    //�V�[���h�p�[�c
    public GameObject T;    //�e�[���p�[�c
    public GameObject U;    //���[�Y�A���[�p�[�c
    public GameObject V;    //�o���G�[�V�����p�[�c
    public GameObject W;    //�E�B���O�p�[�c
    public GameObject X;    //�G�N�X�g���p�[�c
    public GameObject Y;    //�G�N�X�g���p�[�c
    public GameObject Z;    //�����R�A�p�[�c

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
        Display_Core(); //�ݒ肷��R�A�p�[�c�̕\��
        Display_Head(); //��������w�b�h�p�[�c�̕\��
        Display_Arm();  //��������A�[���p�[�c�̕\��
        Display_Backpack(); //��������o�b�N�p�b�N�p�[�c�̕\��
    }

    private void FixedUpdate()  //���I�u�W�F�N�g�̉�]����
    {
        transform.Rotate(new Vector3(0, -2, 0));
    }

    void Display_Core() //�ݒ肷��R�A�p�[�c�̕\��
    {
        if (core_present != core)
        {
            if (Core_part.transform != null)    //�Â��p�[�c�̍폜
            {
                for (int i = 0; Core_part.transform.childCount > i; i++)
                {
                    if (Core_part.transform.GetChild(i).gameObject != gameObject)
                        Destroy(Core_part.transform.GetChild(i).gameObject);
                }
            }
            switch (core)   //�R�A�p�[�c�̐���
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

    void Display_Head() //��������w�b�h�p�[�c�̕\��
    {
        if (head_present != head)
        {
            if (Head_part.transform != null)    //�Â��p�[�c�̍폜
            {
                for (int i = 0; Head_part.transform.childCount > i; i++)
                {
                    if (Head_part.transform.GetChild(i).gameObject != gameObject)
                        Destroy(Head_part.transform.GetChild(i).gameObject);
                }
            }
            switch (head)   //�w�b�h�p�[�c�̐���
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

    void Display_Arm()  //��������A�[���p�[�c�̕\��
    {
        if (arm_present != arm)
        {
            if (ArmL_part.transform != null)    //�Â����A�[���p�[�c�̍폜
            {
                for (int i = 0; ArmL_part.transform.childCount > i; i++)
                {
                    if (ArmL_part.transform.GetChild(i).gameObject != gameObject)
                        Destroy(ArmL_part.transform.GetChild(i).gameObject);
                }
            }
            if (ArmR_part.transform != null)    //�Â��E�A�[���p�[�c�̍폜
            {
                for (int i = 0; ArmR_part.transform.childCount > i; i++)
                {
                    if (ArmR_part.transform.GetChild(i).gameObject != gameObject)
                        Destroy(ArmR_part.transform.GetChild(i).gameObject);
                }
            }
            switch (arm)    //�A�[���p�[�c�̐���
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

    void Display_Backpack() //��������o�b�N�p�b�N�p�[�c�̕\��
    {
        if (backpack_present != backpack)
        {
            if (Backpack_part.transform != null)    //�Â��p�[�c�̍폜
            {
                for (int i = 0; Backpack_part.transform.childCount > i; i++)
                {
                    if (Backpack_part.transform.GetChild(i).gameObject != gameObject)
                        Destroy(Backpack_part.transform.GetChild(i).gameObject);
                }
            }
            switch (backpack)   //�o�b�N�p�b�N�p�[�c�̐���
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
