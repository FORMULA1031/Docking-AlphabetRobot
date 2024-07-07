using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomizeMenu_Control : MonoBehaviour
{
    Image Core_Image;
    Image Head_Image;
    Image Arm_Image;
    Image Backpack_Image;
    Player_Settings Player_Settings;
    string select_part = "Head";
    bool ChangeWeaponButton_flag = false;
    string[] weaponpart_list = new string[7];
    public GameObject[] WeaponPartButton;
    public Sprite A_Image;
    public Sprite B_Image;
    public Sprite C_Image;
    public Sprite D_Image;
    public Sprite E_Image;
    public Sprite F_Image;
    public Sprite G_Image;
    public Sprite H_Image;
    public Sprite I_Image;
    public Sprite J_Image;
    public Sprite K_Image;
    public Sprite L_Image;
    public Sprite M_Image;
    public Sprite N_Image;
    public Sprite O_Image;
    public Sprite P_Image;
    public Sprite Q_Image;
    public Sprite R_Image;
    public Sprite S_Image;
    public Sprite T_Image;
    public Sprite U_Image;
    public Sprite V_Image;
    public Sprite W_Image;
    public Sprite X_Image;
    public Sprite Y_Image;
    public Sprite Z_Image;
    public Sprite Null_Image;
    AudioSource AudioSource;
    public AudioClip mounting_se;

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
        Change_Image(Core_Image, Player_Settings.core_setting);
        Change_Image(Head_Image, Player_Settings.head_setting);
        Change_Image(Arm_Image, Player_Settings.arm_setting);
        Change_Image(Backpack_Image, Player_Settings.backpack_setting);
        SelectionPanel_InstanceButton();
    }

    void Change_Image(Image part_image, string alphabet)
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

    void SelectionPanel_InstanceButton()
    {
        if (!ChangeWeaponButton_flag)
        {
            switch (select_part)
            {
                case "Core":
                    for (int number = 0; number < WeaponPartButton.Length; number++)
                    {
                        if (Player_Settings.core_obtaining.Length > number)
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
                case "Head":
                    for(int number = 0; number < WeaponPartButton.Length; number++)
                    {
                        if (Player_Settings.head_obtaining.Length > number)
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
                case "Arm":
                    for (int number = 0; number < WeaponPartButton.Length; number++)
                    {
                        if (Player_Settings.arm_obtaining.Length > number)
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
                case "Backpack":
                    for (int number = 0; number < WeaponPartButton.Length; number++)
                    {
                        if (Player_Settings.backpack_obtaining.Length > number)
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

    public void OnCoreButton()
    {/*
        if (select_part != "Core")
        {
            select_part = "Core";
            ChangeWeaponButton_flag = false;
        }*/
    }

    public void OnHeadButton()
    {
        if (select_part != "Head")
        {
            select_part = "Head";
            ChangeWeaponButton_flag = false;
        }
    }

    public void OnArmButton()
    {
        if (select_part != "Arm")
        {
            select_part = "Arm";
            ChangeWeaponButton_flag = false;
        }
    }

    public void OnBackpackButton()
    {
        if (select_part != "Backpack")
        {
            select_part = "Backpack";
            ChangeWeaponButton_flag = false;
        }
    }

    public void OnWeaponPartButton(string ButtonNumber)
    {
        AudioSource.PlayOneShot(mounting_se);
        switch (ButtonNumber)
        {
            case "Button0":
                switch (select_part)
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
            case "Button1":
                switch (select_part)
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
            case "Button2":
                switch (select_part)
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
            case "Button3":
                switch (select_part)
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
            case "Button4":
                switch (select_part)
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
            case "Button5":
                switch (select_part)
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
            case "Button6":
                switch (select_part)
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

    public void Remove_Weapon()
    {
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

    public void PlayButton()
    {
        SceneManager.LoadScene("Stage1Scene");
    }
}
