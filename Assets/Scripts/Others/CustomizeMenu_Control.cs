using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomizeMenu_Control : MonoBehaviour
{
    Image Core_Image;   //�I�����Ă���R�A�p�[�c�̉摜
    Image Head_Image;   //�I�����Ă���w�b�h�p�[�c�̉摜
    Image Arm_Image;    //�I�����Ă���A�[���p�[�c�̉摜
    Image Backpack_Image;   //�I�����Ă���o�b�N�p�b�N�p�[�c�̉摜
    Player_Settings Player_Settings;    //EventSystem���R���|�[�l���g���Ă���Player_Settings�X�N���v�g
    string select_part = "Head";    //���ڒ��̃p�[�c
    bool ChangeWeaponButton_flag = false;   //�Ⴄ�E�F�|���{�^���������Ă��Ȃ����̃t���O
    string[] weaponpart_list = new string[7];   //�\������E�F�|���p�[�c�ꗗ
    public GameObject[] WeaponPartButton;   //�\�����Ă���E�F�|���p�[�c�{�^��
    public Sprite A_Image;  //�A�b�N�X�p�̉摜
    public Sprite B_Image;  //�o�Y�[�J�p�̉摜
    public Sprite C_Image;  //�L���m���p�̉摜
    public Sprite D_Image;  //�h�����p�̉摜
    public Sprite E_Image;  //�G���n���X�����g�p�̉摜
    public Sprite F_Image;  //�t�@�C���[�p�̉摜
    public Sprite G_Image;  //�K�g�����O�p�̉摜
    public Sprite H_Image;  //�n���}�[�p�̉摜
    public Sprite I_Image;  //�C���r�W�u���p�̉摜
    public Sprite J_Image;  //�W�F�b�g�p�̉摜
    public Sprite K_Image;  //�i�C�t�p�̉摜
    public Sprite L_Image;  //���[�U�[�p�̉摜
    public Sprite M_Image;  //�~�T�C���p�̉摜
    public Sprite N_Image;  //�j�[�h���p�̉摜
    public Sprite O_Image;  //�n�b�g�p�̉摜
    public Sprite P_Image;  //�s�X�g���p�̉摜
    public Sprite Q_Image;  //�N�A���^���p�̉摜
    public Sprite R_Image;  //���y�A�p�̉摜
    public Sprite S_Image;  //�V�[���h�p�̉摜
    public Sprite T_Image;  //�e�[���p�̉摜
    public Sprite U_Image;  //���[�Y�A���[�p�̉摜
    public Sprite V_Image;  //�o���G�[�V�����p�̉摜
    public Sprite W_Image;  //�E�B���O�p�̉摜
    public Sprite X_Image;  //�G�N�X�g���p�̉摜
    public Sprite Y_Image;  //�����O�X�^�[�p�̉摜
    public Sprite Z_Image;  //�����R�A�p�̉摜
    public Sprite Null_Image;   //��p�̉摜
    AudioSource AudioSource;    //���I�u�W�F�N�g���R���|�[�l���g���Ă���AdioSource
    public AudioClip mounting_se;   //�p�[�c�ύX����SE

    // Start is called before the first frame update
    void Start()
    {
        Player_Settings = GameObject.Find("EventSystem").GetComponent<Player_Settings>();
        Core_Image = GameObject.Find("Canvas/PartPanel/BackgroundPanel/CoreButton").GetComponent<Image>();
        Head_Image = GameObject.Find("Canvas/PartPanel/BackgroundPanel/HeadButton").GetComponent<Image>();
        Arm_Image = GameObject.Find("Canvas/PartPanel/BackgroundPanel/ArmButton").GetComponent<Image>();
        Backpack_Image = GameObject.Find("Canvas/PartPanel/BackgroundPanel/BackpackButton").GetComponent<Image>();
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Change_Image(Core_Image, Player_Settings.core_setting); //�R�A�p�[�c�p�̉摜�X�V
        Change_Image(Head_Image, Player_Settings.head_setting); //�w�b�h�p�[�c�p�̉摜�X�V
        Change_Image(Arm_Image, Player_Settings.arm_setting);   //�A�[���p�[�c�p�̉摜�X�V
        Change_Image(Backpack_Image, Player_Settings.backpack_setting); //�o�b�N�p�b�N�p�[�c�p�̉摜�X�V
        SelectionPanel_InstanceButton();    //�p�[�c�̑I��
    }

    void Change_Image(Image part_image, string alphabet)    //�\�����Ă���p�[�c�摜�X�V
    {
        switch (alphabet)
        {
            case "A":
                part_image.sprite = A_Image;
                break;
            case "B":
                part_image.sprite = B_Image;
                break;
            case "C":
                part_image.sprite = C_Image;
                break;
            case "D":
                part_image.sprite = D_Image;
                break;
            case "E":
                part_image.sprite = E_Image;
                break;
            case "F":
                part_image.sprite = F_Image;
                break;
            case "G":
                part_image.sprite = G_Image;
                break;
            case "H":
                part_image.sprite = H_Image;
                break;
            case "I":
                part_image.sprite = I_Image;
                break;
            case "J":
                part_image.sprite = J_Image;
                break;
            case "K":
                part_image.sprite = K_Image;
                break;
            case "L":
                part_image.sprite = L_Image;
                break;
            case "M":
                part_image.sprite = M_Image;
                break;
            case "N":
                part_image.sprite = N_Image;
                break;
            case "O":
                part_image.sprite = O_Image;
                break;
            case "P":
                part_image.sprite = P_Image;
                break;
            case "Q":
                part_image.sprite = Q_Image;
                break;
            case "R":
                part_image.sprite = R_Image;
                break;
            case "S":
                part_image.sprite = S_Image;
                break;
            case "T":
                part_image.sprite = T_Image;
                break;
            case "U":
                part_image.sprite = U_Image;
                break;
            case "V":
                part_image.sprite = V_Image;
                break;
            case "W":
                part_image.sprite = W_Image;
                break;
            case "X":
                part_image.sprite = X_Image;
                break;
            case "Y":
                part_image.sprite = Y_Image;
                break;
            case "Z":
                part_image.sprite = Z_Image;
                break;
            case "null":
                part_image.sprite = Null_Image;
                break;
        }
    }

    void SelectionPanel_InstanceButton()    //�p�[�c�̑I��
    {
        if (!ChangeWeaponButton_flag)
        {
            switch (select_part)
            {
                case "Core":    //�R�A�p�[�c�̏ꍇ
                    for (int number = 0; number < WeaponPartButton.Length; number++)
                    {
                        if (Player_Settings.core_obtaining.Length > number) //�Y������p�[�c������ꍇ
                        {
                            Change_Image(WeaponPartButton[number].GetComponent<Image>(), Player_Settings.core_obtaining[number]);
                            WeaponPartButton[number].GetComponent<Image>().enabled = true;
                            weaponpart_list[number] = Player_Settings.core_obtaining[number];
                        }
                        else
                        {
                            WeaponPartButton[number].GetComponent<Image>().sprite = null;
                            WeaponPartButton[number].GetComponent<Image>().enabled = false;
                        }
                    }
                    break;
                case "Head":    //�w�b�h�p�[�c�̏ꍇ
                    for(int number = 0; number < WeaponPartButton.Length; number++)
                    {
                        if (Player_Settings.head_obtaining.Length > number) //�Y������p�[�c������ꍇ
                        {
                            Change_Image(WeaponPartButton[number].GetComponent<Image>(), Player_Settings.head_obtaining[number]);
                            WeaponPartButton[number].GetComponent<Image>().enabled = true;
                            weaponpart_list[number] = Player_Settings.head_obtaining[number];
                        }
                        else
                        {
                            WeaponPartButton[number].GetComponent<Image>().sprite = null;
                            WeaponPartButton[number].GetComponent<Image>().enabled = false;
                        }
                    }
                    break;
                case "Arm": //�A�[���p�[�c�̏ꍇ
                    for (int number = 0; number < WeaponPartButton.Length; number++)
                    {
                        if (Player_Settings.arm_obtaining.Length > number)  //�Y������p�[�c������ꍇ
                        {
                            Change_Image(WeaponPartButton[number].GetComponent<Image>(), Player_Settings.arm_obtaining[number]);
                            WeaponPartButton[number].GetComponent<Image>().enabled = true;
                            weaponpart_list[number] = Player_Settings.arm_obtaining[number];
                        }
                        else
                        {
                            WeaponPartButton[number].GetComponent<Image>().sprite = null;
                            WeaponPartButton[number].GetComponent<Image>().enabled = false;
                        }
                    }
                    break;
                case "Backpack":    //�o�b�N�p�b�N�p�[�c�̏ꍇ
                    for (int number = 0; number < WeaponPartButton.Length; number++)
                    {
                        if (Player_Settings.backpack_obtaining.Length > number) //�Y������p�[�c������ꍇ
                        {
                            Change_Image(WeaponPartButton[number].GetComponent<Image>(), Player_Settings.backpack_obtaining[number]);
                            WeaponPartButton[number].GetComponent<Image>().enabled = true;
                            weaponpart_list[number] = Player_Settings.backpack_obtaining[number];
                        }
                        else
                        {
                            WeaponPartButton[number].GetComponent<Image>().sprite = null;
                            WeaponPartButton[number].GetComponent<Image>().enabled = false;
                        }
                    }
                    break;

            }
            ChangeWeaponButton_flag = true;
        }
    }

    public void OnHeadButton()  //�w�b�h�{�^�����������ꍇ
    {
        if (select_part != "Head")
        {
            select_part = "Head";
            ChangeWeaponButton_flag = false;
        }
    }

    public void OnArmButton()   //�A�[���{�^�����������ꍇ
    {
        if (select_part != "Arm")
        {
            select_part = "Arm";
            ChangeWeaponButton_flag = false;
        }
    }

    public void OnBackpackButton()  //�o�b�N�p�b�N�{�^�����������ꍇ
    {
        if (select_part != "Backpack")
        {
            select_part = "Backpack";
            ChangeWeaponButton_flag = false;
        }
    }

    public void OnWeaponPartButton(string ButtonNumber) //�p�[�c�̍X�V
    {
        AudioSource.PlayOneShot(mounting_se);
        switch (ButtonNumber)
        {
            case "Button0": //�{�^��0�ɑΉ�����
                switch (select_part)    //�e���ʂ̃p�[�c�̍X�V
                {
                    case "Core":
                        Player_Settings.Change_Core(weaponpart_list[0]);
                        break;
                    case "Head":
                        Player_Settings.Change_Head(weaponpart_list[0]);
                        break;
                    case "Arm":
                        Player_Settings.Change_Arm(weaponpart_list[0]);
                        break;
                    case "Backpack":
                        Player_Settings.Change_Backpack(weaponpart_list[0]);
                        break;
                }
                break;
            case "Button1": //�{�^��1�ɑΉ�����
                switch (select_part)    //�e���ʂ̃p�[�c�̍X�V
                {
                    case "Core":
                        Player_Settings.Change_Core(weaponpart_list[1]);
                        break;
                    case "Head":
                        Player_Settings.Change_Head(weaponpart_list[1]);
                        break;
                    case "Arm":
                        Player_Settings.Change_Arm(weaponpart_list[1]);
                        break;
                    case "Backpack":
                        Player_Settings.Change_Backpack(weaponpart_list[1]);
                        break;
                }
                break;
            case "Button2": //�{�^��2�ɑΉ�����
                switch (select_part)    //�e���ʂ̃p�[�c�̍X�V
                {
                    case "Core":
                        Player_Settings.Change_Core(weaponpart_list[2]);
                        break;
                    case "Head":
                        Player_Settings.Change_Head(weaponpart_list[2]);
                        break;
                    case "Arm":
                        Player_Settings.Change_Arm(weaponpart_list[2]);
                        break;
                    case "Backpack":
                        Player_Settings.Change_Backpack(weaponpart_list[2]);
                        break;
                }
                break;
            case "Button3": //�{�^��3�ɑΉ�����
                switch (select_part)    //�e���ʂ̃p�[�c�̍X�V
                {
                    case "Core":
                        Player_Settings.Change_Core(weaponpart_list[3]);
                        break;
                    case "Head":
                        Player_Settings.Change_Head(weaponpart_list[3]);
                        break;
                    case "Arm":
                        Player_Settings.Change_Arm(weaponpart_list[3]);
                        break;
                    case "Backpack":
                        Player_Settings.Change_Backpack(weaponpart_list[3]);
                        break;
                }
                break;
            case "Button4": //�{�^��4�ɑΉ�����
                switch (select_part)    //�e���ʂ̃p�[�c�̍X�V
                {
                    case "Core":
                        Player_Settings.Change_Core(weaponpart_list[4]);
                        break;
                    case "Head":
                        Player_Settings.Change_Head(weaponpart_list[4]);
                        break;
                    case "Arm":
                        Player_Settings.Change_Arm(weaponpart_list[4]);
                        break;
                    case "Backpack":
                        Player_Settings.Change_Backpack(weaponpart_list[4]);
                        break;
                }
                break;
            case "Button5": //�{�^��5�ɑΉ�����
                switch (select_part)    //�e���ʂ̃p�[�c�̍X�V
                {
                    case "Core":
                        Player_Settings.Change_Core(weaponpart_list[5]);
                        break;
                    case "Head":
                        Player_Settings.Change_Head(weaponpart_list[5]);
                        break;
                    case "Arm":
                        Player_Settings.Change_Arm(weaponpart_list[5]);
                        break;
                    case "Backpack":
                        Player_Settings.Change_Backpack(weaponpart_list[5]);
                        break;
                }
                break;
            case "Button6": //�{�^��6�ɑΉ�����
                switch (select_part)    //�e���ʂ̃p�[�c�̍X�V
                {
                    case "Core":
                        Player_Settings.Change_Core(weaponpart_list[6]);
                        break;
                    case "Head":
                        Player_Settings.Change_Head(weaponpart_list[6]);
                        break;
                    case "Arm":
                        Player_Settings.Change_Arm(weaponpart_list[6]);
                        break;
                    case "Backpack":
                        Player_Settings.Change_Backpack(weaponpart_list[6]);
                        break;
                }
                break;
        }
    }

    public void Remove_Weapon() //�����E�F�|���̏�����
    {
        Player_Settings.last_weapon = "";
        switch (select_part)
        {
            case "Head":
                Player_Settings.Change_Head("null");
                break;
            case "Arm":
                Player_Settings.Change_Arm("null");
                break;
            case "Backpack":
                Player_Settings.Change_Backpack("null");
                break;
        }
    }

    public void PlayButton()    //�Q�[���̊J�n
    {
        SceneManager.LoadScene("Stage1Scene");
    }

    public void BackButton()    //�X�^�[�g��ʂɖ߂�
    {
        SceneManager.LoadScene("StartMenuScene");
    }
}
