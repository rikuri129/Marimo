using UnityEngine;

public class ScoreItemController : MonoBehaviour
{
    [Header("加算するスコア")] public int myScore;
    [Header("プレーヤの判定")] public PlayerTriggerDirector check;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //判定内に入ったら
        if(check.isIn)
        {
            GameManager.instance.score += 10;       //スコアを加算
            Destroy(this.gameObject);               //破棄する
        }
    }
}
