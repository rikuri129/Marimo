using System.Collections.Generic;
using UnityEngine;

public class DownFloorGenerator : MonoBehaviour
{
    #region //インスペクターで設定
    [Header("下がっていく床のプレハブ")] public GameObject dowmFloorPrefab;
    [Header("生成間隔")] public float blinkTime = 3.0f;
    [Header("速さ")] public float speed = 1.0f;
    [Header("床を生成するy座標")] public float topY = 0.0f;
    [Header("床を消すy座標")] public float bottomY = -10.0f;
    #endregion

    #region //プライベート変数
    private List<GameObject> floors = new List<GameObject>();           //複数の floor を記録する
    private GameObject floor;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private float timer = 0.0f;
    private string downFloorTag = "DownFloor";
    private string upFloorTag = "UpFloor";
    #endregion

    void Start()
    {
        if(dowmFloorPrefab != null)
        {
            rb = GetComponent<Rigidbody2D>();
            moveVelocity = new Vector2(0, speed);
        }
        else
        {
            Debug.Log("設定が足りていません");
        }
    }


    void Update()
    {
        //timer が生成間隔時間より大きくなれば床を生成する
        if (timer > blinkTime)
        {
            timer = 0.0f;           //タイマーをリセット

            floor = Instantiate(dowmFloorPrefab);
            floor.transform.position = new Vector3(transform.position.x, topY, 0);

            if (gameObject.tag == downFloorTag)
            {
                floor.GetComponent<Rigidbody2D>().linearVelocity = -moveVelocity;            //生成した床を下に移動させる
            }
            else if(gameObject.tag == upFloorTag)
            {
                floor.GetComponent<Rigidbody2D>().linearVelocity = moveVelocity;            //生成した床を下に移動させる
            }

                floors.Add(floor);                                                          //リストに生成された床を追加していく
        }
        else
        {
            timer += Time.deltaTime;        //時間を加算
        }

        if (gameObject.tag == downFloorTag)
        {
            //記録している floor をチェックする
            for (int i = floors.Count - 1; i >= 0; i--)
            {
                //条件を満たしたものは消していく
                if (floors[i].transform.position.y < bottomY)
                {
                    Destroy(floors[i]);
                    floors.RemoveAt(i);
                }
            }
        }

        else if(gameObject.tag == upFloorTag)
        {
            //記録している floor をチェックする
            for (int i = floors.Count - 1; i >= 0; i--)
            {
                //条件を満たしたものは消していく
                if (floors[i].transform.position.y > bottomY)
                {
                    Destroy(floors[i]);
                    floors.RemoveAt(i);
                }
            }
        }
    }
}
