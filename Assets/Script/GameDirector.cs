using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    #region //インスペクターで設定
    [Header("フェード")] public FadeDirector Fade;
    [Header("設定画面")] public SettingDirector setting;

    #endregion

    #region //プライベート変数
    private bool firstPush = false;     //連打対策するための変数
    private bool GoNextScene = false;   //次のシーンに行ったかどうか
    #endregion

    /// <summary>
    /// フェードアウトを開始
    /// </summary>
    public void PushStart()
    {
        //次のシーンへ移行する処理を追加する(1回のみ)
        if(firstPush == false)
        {
            firstPush = true;

            Fade.StartFadeOut();        //ボタンが押されたらフェードアウトを開始
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (firstPush == false)
            {
                firstPush = true;
                setting.openSetting();
            }
            else
            {
                firstPush = false;
                setting.closeSetting();
            }
        }

        if (!GoNextScene && Fade.IsFadeOutComplete())
        {
            SceneManager.LoadScene("SelectStageScene");
            GoNextScene = true;
        }
    }

    /// <summary>
    /// 設定画面を開く
    /// </summary>
    public void PushSetting()
    {
        if(firstPush == false)
        {
            firstPush = true;
            setting.openSetting();
        }
    }

    /// <summary>
    /// 設定画面を閉じる
    /// </summary>
    public void PushClose()
    {
        if (firstPush == true)
        { 
            firstPush = false;
            setting.closeSetting();
        }
    }
}
