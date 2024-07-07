using UnityEngine;
using UnityEngine.UI;

public class DescriptionText_Control : MonoBehaviour
{
    public Text ItemText;   //装備したパーツ名を表示するテキスト
    public Text DescriptionText;    //装備したパーツの説明を表示するテキスト
    Player_Settings Player_Settings;    //EventSystemがコンポーネントしているPlayer_Settingsスクリプト

    // Start is called before the first frame update
    void Start()
    {
        Player_Settings = GameObject.Find("EventSystem").GetComponent<Player_Settings>();
    }

    // Update is called once per frame
    void Update()   //表示するテキストの更新
    {
        switch (Player_Settings.last_weapon)
        {
            case "A":
                ItemText.text = "Axe(Arm):";
                DescriptionText.text = "近距離武器で高い攻撃力を持つ。";
                break;
            case "B":
                ItemText.text = "Bazooka(Arm):";
                DescriptionText.text = "破壊力の高いエネルギー弾を放出する。";
                break;
            case "C":
                ItemText.text = "Cannon(Head):";
                DescriptionText.text = "空中にいる敵を倒せるシンプルな大砲。";
                break;
            case "D":
                ItemText.text = "Drill(Head):";
                DescriptionText.text = "破壊力は高いが壊れやすい巨大なドリル。";
                break;
            case "E":
                ItemText.text = "Enhancement(Back):";
                DescriptionText.text = "他の武装を強化できる時限装置。";
                break;
            case "F":
                ItemText.text = "Fire(Head):";
                DescriptionText.text = "広範囲に炎を放射する。";
                break;
            case "G":
                ItemText.text = "Gatling(Arm):";
                DescriptionText.text = "弾数はすぐ尽きるが強力な弾を連射できる。";
                break;
            case "H":
                ItemText.text = "Hammer(Arm):";
                DescriptionText.text = "2つのハンマーで敵を叩き潰す。";
                break;
            case "I":
                ItemText.text = "Invisible(Back):";
                DescriptionText.text = "姿を消して敵に見つからなくなる時限装置。";
                break;
            case "J":
                ItemText.text = "Jet(Back):";
                DescriptionText.text = "燃費の良い高機動時限装置。";
                break;
            case "K":
                ItemText.text = "Knife(Arm):";
                DescriptionText.text = "リーチや破壊力は良くないが小回りが効く近接武器。";
                break;
            case "L":
                ItemText.text = "Laser(Head):";
                DescriptionText.text = "燃費は悪いが高威力のレーザーで上空を狙い打てる。";
                break;
            case "M":
                ItemText.text = "Missile(Back):";
                DescriptionText.text = "アーム攻撃に連動して放物線を描くように爆風付きミサイルを発射する。";
                break;
            case "N":
                ItemText.text = "Needle(Arm):";
                DescriptionText.text = "弾速の速い針を2本同時に撃つ。";
                break;
            case "O":
                ItemText.text = "OldHat(Head):";
                DescriptionText.text = "威力は弱いが燃費が良く敵に当てやすい。";
                break;
            case "P":
                ItemText.text = "Pistol(Arm):";
                DescriptionText.text = "全武装の中で一番燃費が良く2連射できる。";
                break;
            case "R":
                ItemText.text = "Repair(Head):";
                DescriptionText.text = "特殊なエネルギーで自機の耐久力を回復する。";
                break;
            case "S":
                ItemText.text = "Shield(Back):";
                DescriptionText.text = "上空や横からの攻撃を防ぐことがある。";
                break;
            case "T":
                ItemText.text = "Tail(Back):";
                DescriptionText.text = "横にいる敵を破壊できる自動操縦型の時限装置。";
                break;
            case "W":
                ItemText.text = "Wing(Back):";
                DescriptionText.text = "自動操縦型の空に上がれる時限装置。";
                break;
            default:
                ItemText.text = "None:";
                DescriptionText.text = "";
                break;
        }
    }
}
