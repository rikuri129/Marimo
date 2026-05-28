using UnityEngine;

public class ClearDirector : MonoBehaviour
{
    #region //インスペクターで設定
    [Header("拡大縮小のアニメーション")] public AnimationCurve curve;
    [Header("スタートディレクター")] public StartDirector sd;
    #endregion

    #region //プライベート変数
    private bool comp = false;
    private float timer = 0.0f;
    #endregion

    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(!comp)
        {
            //演出をする
            if(timer < 1.0f)
            {
                transform.localScale = Vector3.one * curve.Evaluate(timer);
                timer += Time.deltaTime;
            }
            //終わったら次のシーンに行く
            else
            {
                transform.localScale = Vector3.one;
                sd.ChangeScene(GameManager.instance.stageNum + 1);
                comp = true;
            }
        }
    }
}
