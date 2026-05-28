using UnityEngine;
using TMPro;

public class ScoreDirector : MonoBehaviour
{
    private TextMeshProUGUI scoreText = null;
    private int oldScore = 0;

    void Start()
    {
        //インスタンスを取得
        scoreText = GetComponent<TextMeshProUGUI>();

        // GameManager があるかどうか確認
        if(GameManager.instance != null)
        {
            scoreText.text = "Score " + GameManager.instance.score.ToString("00000000");     //あるならスコアをテキストに書く
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
        if(oldScore != GameManager.instance.score)
        {
            scoreText.text = "Score " + GameManager.instance.score.ToString("00000000");
            oldScore = GameManager.instance.score;
        }
    }
}
