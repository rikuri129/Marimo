using UnityEngine;
using TMPro;

public class StageNumberDirector : MonoBehaviour
{
    private TextMeshProUGUI stageText = null;
    private int oldStageNum = 0;

    void Start()
    {
        //インスタンスを取得
        stageText = GetComponent<TextMeshProUGUI>();

        // GameManager があるかどうか確認
        if (GameManager.instance != null)
        {
            stageText.text = "Stage " + GameManager.instance.stageNum;     //あるならスコアをテキストに書く
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
        //ステージが変わったときだけテキストを更新するようにする
        if (oldStageNum != GameManager.instance.stageNum)
        {
            stageText.text = "Stage " + GameManager.instance.stageNum;
            oldStageNum = GameManager.instance.stageNum;
        }
    }
}
