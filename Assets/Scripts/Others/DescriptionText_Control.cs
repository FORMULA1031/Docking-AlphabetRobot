using UnityEngine;
using UnityEngine.UI;

public class DescriptionText_Control : MonoBehaviour
{
    public Text ItemText;   //���������p�[�c����\������e�L�X�g
    public Text DescriptionText;    //���������p�[�c�̐�����\������e�L�X�g
    Player_Settings Player_Settings;    //EventSystem���R���|�[�l���g���Ă���Player_Settings�X�N���v�g

    // Start is called before the first frame update
    void Start()
    {
        Player_Settings = GameObject.Find("EventSystem").GetComponent<Player_Settings>();
    }

    // Update is called once per frame
    void Update()   //�\������e�L�X�g�̍X�V
    {
        switch (Player_Settings.last_weapon)
        {
            case "A":
                ItemText.text = "Axe(Arm):";
                DescriptionText.text = "�ߋ�������ō����U���͂����B";
                break;
            case "B":
                ItemText.text = "Bazooka(Arm):";
                DescriptionText.text = "�j��͂̍����G�l���M�[�e����o����B";
                break;
            case "C":
                ItemText.text = "Cannon(Head):";
                DescriptionText.text = "�󒆂ɂ���G��|����V���v���ȑ�C�B";
                break;
            case "D":
                ItemText.text = "Drill(Head):";
                DescriptionText.text = "�j��͍͂��������₷������ȃh�����B";
                break;
            case "E":
                ItemText.text = "Enhancement(Back):";
                DescriptionText.text = "���̕����������ł��鎞�����u�B";
                break;
            case "F":
                ItemText.text = "Fire(Head):";
                DescriptionText.text = "�L�͈͂ɉ�����˂���B";
                break;
            case "G":
                ItemText.text = "Gatling(Arm):";
                DescriptionText.text = "�e���͂����s���邪���͂Ȓe��A�˂ł���B";
                break;
            case "H":
                ItemText.text = "Hammer(Arm):";
                DescriptionText.text = "2�̃n���}�[�œG��@���ׂ��B";
                break;
            case "I":
                ItemText.text = "Invisible(Back):";
                DescriptionText.text = "�p�������ēG�Ɍ�����Ȃ��Ȃ鎞�����u�B";
                break;
            case "J":
                ItemText.text = "Jet(Back):";
                DescriptionText.text = "�R��̗ǂ����@���������u�B";
                break;
            case "K":
                ItemText.text = "Knife(Arm):";
                DescriptionText.text = "���[�`��j��͂͗ǂ��Ȃ�������肪�����ߐڕ���B";
                break;
            case "L":
                ItemText.text = "Laser(Head):";
                DescriptionText.text = "�R��͈��������З͂̃��[�U�[�ŏ���_���łĂ�B";
                break;
            case "M":
                ItemText.text = "Missile(Back):";
                DescriptionText.text = "�A�[���U���ɘA�����ĕ�������`���悤�ɔ����t���~�T�C���𔭎˂���B";
                break;
            case "N":
                ItemText.text = "Needle(Arm):";
                DescriptionText.text = "�e���̑����j��2�{�����Ɍ��B";
                break;
            case "O":
                ItemText.text = "OldHat(Head):";
                DescriptionText.text = "�З͎͂ア���R��ǂ��G�ɓ��Ă₷���B";
                break;
            case "P":
                ItemText.text = "Pistol(Arm):";
                DescriptionText.text = "�S�����̒��ň�ԔR��ǂ�2�A�˂ł���B";
                break;
            case "R":
                ItemText.text = "Repair(Head):";
                DescriptionText.text = "����ȃG�l���M�[�Ŏ��@�̑ϋv�͂��񕜂���B";
                break;
            case "S":
                ItemText.text = "Shield(Back):";
                DescriptionText.text = "���≡����̍U����h�����Ƃ�����B";
                break;
            case "T":
                ItemText.text = "Tail(Back):";
                DescriptionText.text = "���ɂ���G��j��ł��鎩�����c�^�̎������u�B";
                break;
            case "W":
                ItemText.text = "Wing(Back):";
                DescriptionText.text = "�������c�^�̋�ɏオ��鎞�����u�B";
                break;
            default:
                ItemText.text = "None:";
                DescriptionText.text = "";
                break;
        }
    }
}
