using UnityEngine;

public class PlayerTriggerDirector : MonoBehaviour
{
    /// <summary>
    /// 判定内にプレーヤがいるか判定する
    /// </summary>
    [HideInInspector] public bool isIn = false;

    #region //プライベート変数
    private string PlayerTag = "Player";
    #endregion

    #region //接触判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == PlayerTag)
        {
            isIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == PlayerTag)
        {
            isIn = false;
        }
    }
    #endregion
}
