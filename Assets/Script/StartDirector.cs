using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDirector : MonoBehaviour
{
    #region //インスペクターで設定
    [Header("プレーヤゲームオブジェクト")] public GameObject player;
    [Header("コンテニュー位置")] public GameObject[] continuePoint;
    [Header("ゲームオーバー")] public GameObject gameOverObj;
    [Header("フェード")] public FadeDirector fade;
    [Header("ゲームオーバー時に鳴らすSE")] public AudioClip gameOverSE;
    [Header("リトライ時に鳴らすSE")] public AudioClip retrySE;
    [Header("ステージクリア時に鳴らすSE")] public AudioClip stageClearSE;
    [Header("ステージクリアオブジェクト")] public GameObject stageClearObj;
    [Header("ステージクリア判定")] public PlayerTriggerDirector ptd;
    #endregion

    #region //プライベート変数
    private PlayerController p;
    private int nextStageNum;               //次のステージ番号
    private bool startFade = false;         //フェードを開始するか
    private bool doGameOver = false;        //ゲームオーバーにするか
    private bool retryGame = false;         //リトライするか
    private bool doSceneChange = false;     //シーンを切り替えるか
    private bool doClear = false;           //クリアかどうか
    #endregion

    void Start()
    {
        //インスペクターでの設定が足りているかを確認
        if(player != null && continuePoint != null && continuePoint.Length > 0 && gameOverObj != null && fade != null)
        {
            player.transform.position = continuePoint[0].transform.position;        //プレーヤをスタート地点に配置する
            gameOverObj.SetActive(false);                                           //初期状態はゲームオーバーは非アクティブ(画面に表示しない)にしておく
            stageClearObj.SetActive(false);                                         //初期状態は非アクティブに

            p = player.GetComponent<PlayerController>();

            if(p == null)
            {
                Debug.Log("PlayerController がアタッチされていません");
            }
        }
        else
        {
            Debug.Log("設定が足りていません");
        }
    }


    void Update()
    {
        //ゲームオーバー時の処理
        if (GameManager.instance.isGameOver && !doGameOver)
        {
            GameManager.instance.PlaySE(gameOverSE);
            gameOverObj.SetActive(true);                //ゲームオーバーになったら状態をアクティブにする(画面に表示)
            doGameOver = true;
        }

        //プレーヤがやられていないときの処理
        else if (p != null && p.IsContinueWaiting() && !doGameOver)
        {
            //PlayerController を取得できていて、かつコンティニュー待機状態か確認
            if (p != null && p.IsContinueWaiting())
            {
                //コンティニューしたい位置の目印の設定が足りているか確認
                if (continuePoint.Length > GameManager.instance.continueNum)
                {
                    player.transform.position = continuePoint[GameManager.instance.continueNum].transform.position;     //プレーヤをコンティニューポイントに移動する

                    p.ContinuePlayer();
                }
                else
                {
                    Debug.Log("コンティニューポイントの設定が足りていません");
                }
            }
        }

        //クリア判定内に入ったら
        else if(ptd != null && ptd.isIn && !doGameOver && !doClear)
        {
            StageClear();
            doClear = true;
        }

        //ステージを切り替える
        if (fade != null && startFade && !doSceneChange)
        {
            //フェードアウトが完了したら
            if (fade.IsFadeOutComplete())
            {
                //ゲームをリトライする
                if (retryGame)
                {
                    GameManager.instance.RetryGame();
                    gameOverObj.SetActive(false);
                }

                else
                {
                    GameManager.instance.stageNum = nextStageNum;
                }

                GameManager.instance.isStageClear = false;                              //ステージを移動したらクリアフラグを下す
                GameManager.instance.score = 0;                                         //スコアをリセットする
                GameManager.instance.heartNum = GameManager.instance.defaultHeartNum;   //ハートの値もリセット
                GameManager.instance.continueNum = 0;
                
                //今のステージをもう一度やり直すのか選択画面に戻るのか選べるようにする
                if(nextStageNum != 0)
                {
                    SceneManager.LoadScene("Stage" + nextStageNum);
                }
                else
                {
                    SceneManager.LoadScene("SelectStageScene");
                }

                    doSceneChange = true;
            }
        }
    }

    /// <summary>
    /// 今のステージを最初から始める
    /// </summary>
    public void Retry()
    {
        GameManager.instance.PlaySE(retrySE);
        ChangeScene(GameManager.instance.stageNum);     //現在のステージをやり直すので引数はGameManagerからステージ番号を受け取る
        retryGame = true;
    }

    /// <summary>
    /// ステージ選択画面にもどる
    /// </summary>
    public void Back_to_Select()
    {
        GameManager.instance.PlaySE(retrySE);
        ChangeScene(0);
        retryGame = true;

    }

    /// <summary>
    /// シーン切り替え
    /// </summary>
    /// <param name="num">ステージ番号</param>
    public void ChangeScene(int num)
    {
        if(fade != null)
        {
            nextStageNum = num;
            fade.StartFadeOut();
            startFade = true;
        }
    }

    /// <summary>
    /// クリアしたときの処理
    /// </summary>
    public void StageClear()
    {
        GameManager.instance.isStageClear = true;        //クリアフラグを立てる
        stageClearObj.SetActive(true);                   //クリアオブジェクトをアクティブに
        GameManager.instance.PlaySE(stageClearSE);       //クリアSEを鳴らす
    }
}
