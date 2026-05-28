using UnityEngine;

public class SavePointController : MonoBehaviour
{
    #region //インスペクターで設定
    [Header("コンティニュー番号")] public int continueNum;
    [Header("音")] public AudioClip se;
    [Header("プレーヤ判定")] public PlayerTriggerDirector ptd;
    [Header("スピード")] public float speed = 2.0f;
    [Header("取得アニメーション")] public AnimationCurve curve;
    #endregion

    #region //プライベート変数
    private bool isOn;
    private float timer;
    #endregion

    void Start()
    {
        //初期化する
        if(se != null && ptd != null)
        {
            isOn = false;
            timer = 0.0f;
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
        if (ptd.isIn && !isOn)
        {
            GameManager.instance.continueNum = continueNum;                         //コンティニュー位置を更新
            GameManager.instance.heartNum = GameManager.instance.defaultHeartNum;   //セーブポイントに入ったら残機も回復させる
            GameManager.instance.PlaySE(se);                                        //SEを鳴らす
            isOn = true;                                                            //フラグを立てる
        }

        if (isOn)
        {
            if (timer < 1.0f)
            {
                transform.localScale = Vector3.one * curve.Evaluate(timer);
                timer += speed * Time.deltaTime;
            }
            else
            {
                transform.localScale = Vector3.one * curve.Evaluate(1.0f);
                gameObject.SetActive(false);
                isOn = false;
            }
        }
    }
}
