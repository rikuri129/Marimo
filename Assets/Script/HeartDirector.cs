using UnityEngine;
using TMPro;

public class HeartDirector : MonoBehaviour
{
    private TextMeshProUGUI heartText = null;
    private int oldHeartNum = 0;

    void Start()
    {
        //インスタンスを取得
        heartText = GetComponent<TextMeshProUGUI>();

        // GameManager があるかどうか確認
        if (GameManager.instance != null)
        {
            heartText.text = "× " + GameManager.instance.defaultHeartNum;     //あるならスコアをテキストに書く
        }
        else
        {
            Debug.Log("GameManager がありません。");                    //なかった場合はログを表示してテキストを消す
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //スコアが変わったときだけテキストを更新するようにする
        if (oldHeartNum != GameManager.instance.heartNum)
        {
            heartText.text = "× " + GameManager.instance.heartNum;
            oldHeartNum = GameManager.instance.heartNum;
        }
    }
}
