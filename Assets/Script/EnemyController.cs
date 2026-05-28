using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region //インスペクターで設定
    [Header("加算するスコア")] public int myScore;                   //敵を倒したときに加算するスコアの値
    [Header("移動速度")] public float speed;                         //スピード調整変数(具体的な数字はインスペクターで設定)
    [Header("重力")] public float gravity;                           //重力変数
    [Header("画面外でも行動するか")] public bool nonVisible = false; //画面外でも動くか決める
    [Header("接触判定")] public WallCheckDirector WallCollision;     //壁や敵との接触判定
    #endregion

    #region //プライベート変数
    private Rigidbody2D rb = null;                                   //Rigidbody2Dのインスタンスにアクセスする変数
    private SpriteRenderer sr = null;
    private BoundDirector bd = null;
    private CapsuleCollider2D capcol = null;
    private Animator anim = null;
    private Transform t = null;                                     //Enemyについてる子オブジェクトにアクセスるための変数
    private bool RighTelftF = false;
    private bool isDead = false;
    private string deadAreaTag = "DeadArea";
    private Vector3 defaultScal;
    #endregion

    void Start()
    {
        //それぞれのインスタンスを取得
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        bd = GetComponent<BoundDirector>();
        capcol = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        t = transform.Find("ColliderObject");
        defaultScal = transform.localScale;
    }


    void FixedUpdate()
    {
        //プレーヤに踏まれていないとき
        if (!bd.PlayerStepOn)
        {
            notStepOn();
        }

        //踏まれたとき
        else
        {
            isStepOn("EnemyDead");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == deadAreaTag)
        {
            Destroy(this.gameObject);
        }
    }

    public void notStepOn()
    {
        //画面に映っているとき
        if (sr.isVisible || nonVisible)
        {
            //敵が接触を感知したら
            if (WallCollision.isOn)
            {
                RighTelftF = !RighTelftF;       //左右反転させる
            }

            int xVector = -1;

            //右に移動する場合
            if (RighTelftF)
            {
                xVector = 1;                                        //右に進ませる
                transform.localScale = new Vector3(-defaultScal.x, defaultScal.y, defaultScal.z);       //体の向きを右向きにする
            }

            else
            {
                transform.localScale = new Vector3(defaultScal.x, defaultScal.y, defaultScal.z);
            }

            rb.linearVelocity = new Vector2(xVector * speed, -gravity);
        }

        //映っていないとき
        else
        {
            rb.Sleep();         //物理演算を切る
        }
    }

    public void isStepOn(string animationPlay)
    {
        if (!isDead)
        {
            anim.Play(animationPlay);
            rb.linearVelocity = new Vector2(0, -gravity);
            isDead = true;

            //ColliderObjectの子オブジェクトをすべて非アクティブにする
            if (t != null)
            {
                t.gameObject.SetActive(false);
            }

            if (GameManager.instance != null)
            {
                GameManager.instance.score += 10;   //プレーヤのスコアを加算する
            }

            Destroy(gameObject, 3f);            //オブジェクトを破棄する
        }

        else
        {
            transform.Rotate(0, 0, 5);
        }
    }
}
