using UnityEngine;

public class WallCheckDirector : MonoBehaviour
{
    /// <summary>
    /// 判定内に敵か壁があるか判定する
    /// </summary>
    [HideInInspector] public bool isOn = false;

    #region //プライベート変数
    private string GroundTag = "Ground";
    private string EnemyTag = "Enemy";
    private string WallTag = "Wall";
    #endregion

    #region //接触判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == GroundTag || collision.tag == EnemyTag || collision.tag == WallTag)
        {
            isOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == GroundTag || collision.tag == EnemyTag || collision.tag == WallTag)
        {
            isOn = false;
        }
    }
    #endregion
}
