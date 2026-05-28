using UnityEngine;

public class FadeUGUIController : MonoBehaviour
{
    #region //インスペクターで設定
    [Header("フェードスピード")] public float speed = 1.0f;
    [Header("上昇量")] public float moveDis = 10.0f;
    [Header("上昇時間")] public float moveTime = 1.0f;
    [Header("キャンバスグループ")] public CanvasGroup cg;
    [Header("プレーヤ判定")] public PlayerTriggerDirector ptd;
    #endregion

    #region //プライベート変数  
    private Vector3 defauluPos;
    private float timer = 0.0f;
    #endregion 

    void Start()
    {
        //初期化する
        if(cg != null && ptd != null)
        {
            cg.alpha = 0.0f;                                            //アルファ値を0にする(透明の状態)
            defauluPos = cg.transform.position;
            cg.transform.position = defauluPos - Vector3.up * moveDis;  //初期位置から少し下にする
        }
        else
        {
            Debug.Log("設定が足りていません");
            Destroy(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //プレーヤが判定内に入ったら
        if (ptd.isIn)
        {
            //上昇しながらフェードイン
            if (cg.transform.position.y < defauluPos.y || cg.alpha < 1.0f)
            {
                cg.alpha = timer / moveTime;                                                            //設定した時間で1になるようにする
                cg.transform.position += Vector3.up * (moveDis / moveTime) * speed * Time.deltaTime;    //(moveDis / moveTime)は速さを求めてる、また speed * Time.deltaTime は時間の進む速さを調整している
                timer += speed * Time.deltaTime;
            }
            else
            {
                //終わったらちゃんとした値を入れる
                cg.alpha = 1.0f;
                cg.transform.position = defauluPos;
            }
        }
        //プレーヤが範囲内にいないとき
        else
        {
            //下降しながらフェードアウト
            if (cg.transform.position.y > defauluPos.y - moveDis || cg.alpha > 0.0f)
            {
                cg.alpha = timer / moveTime;                                                            //設定した時間で1になるようにする
                cg.transform.position -= Vector3.up * (moveDis / moveTime) * speed * Time.deltaTime;    //(moveDis / moveTime)は速さを求めてる、また speed * Time.deltaTime は時間の進む速さを調整している
                timer -= speed * Time.deltaTime;
            }
            else
            {
                //終わったらちゃんとした値を入れる
                timer = 0.0f;
                cg.alpha = 0.0f;
                cg.transform.position = defauluPos - Vector3.up * moveDis;
            }
        }
    }
}
