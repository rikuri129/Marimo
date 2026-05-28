using UnityEngine;

public class GroundCheckDirector : MonoBehaviour
{
    #region //インスペクターで設定
    [Header("エフェクトがついた床を判定するか")] public bool checkPlatformGround;
    #endregion

    #region //プライベート変数
    private string GroundTag = "Ground";
    private string platformTag = "GroundPlatform";
    private string moveFloorTag = "MoveFloor";
    private string fallFloorTag = "FallFloor";
    private string downFloorTag = "DownFloor";
    private string upFloorTag = "UpFloor";
    private string enemyGroundTag = "EnemyGround";
    private bool isGround = false;
    private bool isEnemyGround = false;
    private bool isGroundEnter, isGroundStay, isGroundExit, isEnemyGroundEnter, isEnemyGroundExit;     //個別に判定してあげる
    #endregion

    /// <summary>
    /// 接地判定
    /// </summary>
    /// <returns>地面にいるかどうか</returns>
    public bool IsGround()
    {
        //地面に触れたまたは地面に触れ続けているなら接地判定を true にする
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }

        //地面に触れていなければ接地判定を false にする
        else if (isGroundExit)
        {
            isGround = false;
        }

        //それぞれのフラグを下す
        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;

        return isGround;
    }

    public  bool IsEnemyGround()
    {
        if(isEnemyGroundEnter)
        {
            isEnemyGround = true; 
        }
        else if(isEnemyGroundExit)
        {
            isEnemyGround = false;
        }

        isEnemyGroundEnter = false;
        isEnemyGroundExit = false;

        return isEnemyGround;
    }

    /// <summary>
    /// プレーヤが地面と接触したら処理を開始する
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)     //判定内に入ったオブジェクトの Collider を引数として受け取る。
    {
        //判定内に入った Collider のタグが Ground なら
        if (collision.tag == GroundTag)
        {
            isGroundEnter = true;
        }
        else if (checkPlatformGround && (collision.tag == platformTag || 
                                         collision.tag == moveFloorTag || 
                                         collision.tag == fallFloorTag || 
                                         collision.tag == downFloorTag || 
                                         collision.tag == upFloorTag))
        {
            isGroundEnter = true;
        }
        else if(collision.tag == enemyGroundTag)
        {
            isEnemyGroundEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == GroundTag)
        {
            isGroundStay = true;
        }
        else if (checkPlatformGround && (collision.tag == platformTag || 
                                         collision.tag == moveFloorTag || 
                                         collision.tag == fallFloorTag || 
                                         collision.tag == downFloorTag || 
                                         collision.tag == upFloorTag))
        {
            isGroundStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == GroundTag)
        {
            isGroundExit = true;
        }
        else if (checkPlatformGround && (collision.tag == platformTag || 
                                         collision.tag == moveFloorTag || 
                                         collision.tag == fallFloorTag || 
                                         collision.tag == downFloorTag || 
                                         collision.tag == upFloorTag))
        {
            isGroundExit = true;
        }
        else if(collision.tag == enemyGroundTag)
        {
            isEnemyGroundExit = true;
        }
    }
}
