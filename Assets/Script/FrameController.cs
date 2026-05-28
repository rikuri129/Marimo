using UnityEngine;

public class FrameController : MonoBehaviour
{
    [Header("移動させる幅(x座標)")] public float disX;
    [Header("移動させる幅(y座標)")] public float disY;
    [Header("拡大縮小する大きさ")] public float zoomScale = 0.05f;
    [Header("拡大縮小するスピード")] public float zoomSpeed = 10f;

    public GameObject[] stage;
    public FadeDirector fade;

    private GameObject cameraObj = null;
    private Vector3 maxPos;
    private Vector3 defaultScale;
    private float timer = 0.0f;
    private float nowX;
    private float nowY;


    void Start()
    {
        cameraObj = GameObject.Find("Main Camera");
        defaultScale = transform.localScale;
        nowX = -disX;
        nowY = disY;
        maxPos = cameraObj.transform.position + new Vector3(disX, -disY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Camera cam = cameraObj.GetComponent<Camera>();                      //Main Camera のコンポーネントを入れておく
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);     //マウスの座標を画面上の座標からワールド座標に変化する
        Collider2D col = Physics2D.OverlapPoint(mousePos);                  //マウスの位置にある Collider2D を取得する

        if(Input.GetMouseButtonUp(0))   //左クリックされたら、フレームをクリック位置にあるコライダーの位置に移動する
        {
            transform.position = new Vector3(col.transform.position.x, col.transform.position.y, col.transform.position.z);
        }

        /*
        //枠を右に移動する
        if(Input.GetKeyDown(KeyCode.D) )
        {
            if (nowX >= disX)
            {
                nowX = -disX;
                nowY *= -1;
                transform.position = new Vector3(nowX, nowY, 0);
            }
            else
            {
                nowX += disX;
                transform.position = new Vector3(nowX, nowY, 0);
            }
        }
        //枠を左に移動する
        else if(Input.GetKeyDown(KeyCode.A))
        {
            if (nowX <= -disX)
            {
                nowX = disX;
                nowY *= -1;
                transform.position = new Vector3(nowX, nowY, 0);
            }
            else
            {
                nowX -= disX;
                transform.position = new Vector3(nowX, nowY, 0);
            }
        }
        */

        //拡大縮小する
        float x = zoomScale * Mathf.Sin(zoomSpeed * timer);
        float y = zoomScale * Mathf.Sin(zoomSpeed * timer);
        transform.localScale = defaultScale + new Vector3(x, y, 0);
        timer += Time.deltaTime;                                        //時間を加算する
        
    }
        
}
