using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomizeMenu_Control : MonoBehaviour
{
    Image Core_Image;   //選択しているコアパーツの画像
    Image Head_Image;   //選択しているヘッドパーツの画像
    Image Arm_Image;    //選択しているアームパーツの画像
    Image Backpack_Image;   //選択しているバックパックパーツの画像
    Player_Settings Player_Settings;    //EventSystemがコンポーネントしているPlayer_Settingsスクリプト
    string select_part = "Head";    //注目中のパーツ
    bool ChangeWeaponButton_flag = false;   //違うウェポンボタンを押していないかのフラグ
    string[] weaponpart_list = new string[7];   //表示するウェポンパーツ一覧
    public GameObject[] WeaponPartButton;   //表示しているウェポンパーツボタン
    public Sprite A_Image;  //アックス用の画像
    public Sprite B_Image;  //バズーカ用の画像
    public Sprite C_Image;  //キャノン用の画像
    public Sprite D_Image;  //ドリル用の画像
    public Sprite E_Image;  //エンハンスメント用の画像
    public Sprite F_Image;  //ファイヤー用の画像
    public Sprite G_Image;  //ガトリング用の画像
    public Sprite H_Image;  //ハンマー用の画像
    public Sprite I_Image;  //インビジブル用の画像
    public Sprite J_Image;  //ジェット用の画像
    public Sprite K_Image;  //ナイフ用の画像
    public Sprite L_Image;  //レーザー用の画像
    public Sprite M_Image;  //ミサイル用の画像
    public Sprite N_Image;  //ニードル用の画像
    public Sprite O_Image;  //ハット用の画像
    public Sprite P_Image;  //ピストル用の画像
    public Sprite Q_Image;  //クアンタム用の画像
    public Sprite R_Image;  //リペア用の画像
    public Sprite S_Image;  //シールド用の画像
    public Sprite T_Image;  //テール用の画像
    public Sprite U_Image;  //ユーズアリー用の画像
    public Sprite V_Image;  //バリエーション用の画像
    public Sprite W_Image;  //ウィング用の画像
    public Sprite X_Image;  //エクストラ用の画像
    public Sprite Y_Image;  //ヤングスター用の画像
    public Sprite Z_Image;  //初期コア用の画像
    public Sprite Null_Image;   //空用の画像
    AudioSource AudioSource;    //自オブジェクトがコンポーネントしているAdioSource
    public AudioClip mounting_se;   //パーツ変更時のSE

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
        Change_Image(Core_Image, Player_Settings.core_setting); //コアパーツ用の画像更新
        Change_Image(Head_Image, Player_Settings.head_setting); //ヘッドパーツ用の画像更新
        Change_Image(Arm_Image, Player_Settings.arm_setting);   //アームパーツ用の画像更新
        Change_Image(Backpack_Image, Player_Settings.backpack_setting); //バックパックパーツ用の画像更新
        SelectionPanel_InstanceButton();    //パーツの選択
    }

    void Change_Image(Image part_image, string alphabet)    //表示しているパーツ画像更新
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

    void SelectionPanel_InstanceButton()    //パーツの選択
    {
        if (!ChangeWeaponButton_flag)
        {
            switch (select_part)
            {
                case "Core":    //コアパーツの場合
                    for (int number = 0; number < WeaponPartButton.Length; number++)
                    {
                        if (Player_Settings.core_obtaining.Length > number) //該当するパーツがある場合
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
                case "Head":    //ヘッドパーツの場合
                    for(int number = 0; number < WeaponPartButton.Length; number++)
                    {
                        if (Player_Settings.head_obtaining.Length > number) //該当するパーツがある場合
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
                case "Arm": //アームパーツの場合
                    for (int number = 0; number < WeaponPartButton.Length; number++)
                    {
                        if (Player_Settings.arm_obtaining.Length > number)  //該当するパーツがある場合
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
                case "Backpack":    //バックパックパーツの場合
                    for (int number = 0; number < WeaponPartButton.Length; number++)
                    {
                        if (Player_Settings.backpack_obtaining.Length > number) //該当するパーツがある場合
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

    public void OnHeadButton()  //ヘッドボタンを押した場合
    {
        if (select_part != "Head")
        {
            select_part = "Head";
            ChangeWeaponButton_flag = false;
        }
    }

    public void OnArmButton()   //アームボタンを押した場合
    {
        if (select_part != "Arm")
        {
            select_part = "Arm";
            ChangeWeaponButton_flag = false;
        }
    }

    public void OnBackpackButton()  //バックパックボタンを押した場合
    {
        if (select_part != "Backpack")
        {
            select_part = "Backpack";
            ChangeWeaponButton_flag = false;
        }
    }

    public void OnWeaponPartButton(string ButtonNumber) //パーツの更新
    {
        AudioSource.PlayOneShot(mounting_se);
        switch (ButtonNumber)
        {
            case "Button0": //ボタン0に対応する
                switch (select_part)    //各部位のパーツの更新
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
            case "Button1": //ボタン1に対応する
                switch (select_part)    //各部位のパーツの更新
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
            case "Button2": //ボタン2に対応する
                switch (select_part)    //各部位のパーツの更新
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
            case "Button3": //ボタン3に対応する
                switch (select_part)    //各部位のパーツの更新
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
            case "Button4": //ボタン4に対応する
                switch (select_part)    //各部位のパーツの更新
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
            case "Button5": //ボタン5に対応する
                switch (select_part)    //各部位のパーツの更新
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
            case "Button6": //ボタン6に対応する
                switch (select_part)    //各部位のパーツの更新
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

    public void Remove_Weapon() //装備ウェポンの初期化
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

    public void PlayButton()    //ゲームの開始
    {
        SceneManager.LoadScene("Stage1Scene");
    }

    public void BackButton()    //スタート画面に戻る
    {
        SceneManager.LoadScene("StartMenuScene");
    }
}
