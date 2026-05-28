using UnityEngine;

public class FallFloorController : MonoBehaviour
{
    #region //インスペクターで設定
    [Header("スプライトがあるオブジェクト")] public GameObject spriteObj;
    [Header("振動幅")] public float vibrationWidth = 0.05f;
    [Header("振動速度")] public float vibrationSpeed = 30.0f;
    [Header("落ちるまでの時間")] public float fallTime = 1.0f;
    [Header("落ちていく速度")] public float fallSpeed = 10.0f;
    [Header("落ちてから戻ってくるまでの時間")] public float returnTime = 5.0f;
    //[Header("振動アニメーション")] public AnimationCurve curve;
    #endregion

    #region //プライベート変数
    private bool isOn;
    private bool isFall;
    private bool isReturn;
    private Vector3 spriteDefaultPos;
    private Vector3 floorDefaultPos;
    private Vector2 fallVelocity;
    private BoxCollider2D col;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private BoundDirector bd;
    private float timer = 0.0f;
    private float fallingTimer = 0.0f;
    private float returnTimer = 0.0f;
    private float blinkTimer = 0.0f;
    #endregion
    void Start()
    {
        //インスタンスを取得
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        bd = GetComponent<BoundDirector>();

        //初期設定
        if(spriteObj != null && col != null && rb != null && bd != null)
        {
            spriteDefaultPos = spriteObj.transform.position;
            fallVelocity = new Vector2(0, -fallSpeed);
            floorDefaultPos = gameObject.transform.position;
            sr = spriteObj.GetComponent<SpriteRenderer>();

            if (sr == null)
            {
                Debug.Log("SpriteRendererの設定が足りていません");
                Destroy(this);
            }
        }
        else
        {
            Debug.Log("設定が足りていません");
            Destroy(this);
        }
    }


    void Update()
    {
        //プレーヤが1回でも乗ったらフラグを立てる
        if(bd.PlayerStepOn)
        {
            isOn = true;
            bd.PlayerStepOn = false;
        }

        //振動させる
        if(isOn && !isFall)
        {
            //float x = curve.Evaluate(timer * vibrationSpeed) * vibrationWidth;            //アニメーションカーブを使った式
            float x = vibrationWidth * Mathf.Sin(vibrationSpeed * timer);                   //sinの波(正弦波)を使った式(アニメーションカーブを使わなくてよくなる)
            spriteObj.transform.position = spriteDefaultPos + new Vector3(x, 0, 0);

            //一定時間たったら落ちる
            if(timer > fallTime)
            {
                isFall = true;          //落ちるフラグを立てる
            }
            timer += Time.deltaTime;
        }

        //一定時間たったら点滅して戻ってくる
        if(isReturn)
        {
            //blinkTime が 0.2 より大きいとき、全ての状態をリセットする
            if (blinkTimer > 0.2f)
            {
                sr.enabled = true;
                blinkTimer = 0.0f;
            }

            //0.1 より大きいとき
            else if (blinkTimer > 0.1f)
            {
                sr.enabled = false;             //床を消す
                col.enabled = false;
            }

            //それ以外のとき
            else
            {
                sr.enabled = true;              //床を表示
            }

            //1秒経ったら点滅を終わる
            if (returnTimer > 1.0f)
            {
                isReturn = false;
                blinkTimer = 0.0f;
                returnTimer = 0.0f;
                sr.enabled = true;
                col.enabled = true;
            }
            else
            {
                blinkTimer += Time.deltaTime;
                returnTimer += Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        //落下中
        if(isFall)
        {
            rb.linearVelocity = fallVelocity;

            //一定時間たったら元に戻す
            if(fallingTimer > returnTime)
            {
                isReturn = true;                            //元に戻すフラグを立てる
                transform.position = floorDefaultPos;       //床の位置をもとの場所に戻す
                rb.linearVelocity = Vector2.zero;           //velocityを0にする
                isFall = false;                             //落ちているフラグを下す
                timer = 0.0f;                               //タイマーをリセットする
                fallingTimer = 0.0f;
            }
            else
            {
                fallingTimer += Time.deltaTime;
                isOn = false;
            }

        }
    }
}
