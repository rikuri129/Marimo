using UnityEngine;

public class UpDownEnemyGenerator : MonoBehaviour
{
    [Header("生成する敵")] public GameObject updown_enemy;
    [Header("上下移動する敵のスクリプト")] public EnemyController2 enctrl2;
    [Header("プレイヤーのスクリプト")] public PlayerController plctrl;
    [Header("移動経路")] public GameObject[] movePoint;
    [Header("生成するx座標")] public float xPoint = 0;
    [Header("生成するy座標")] public float yPoint = 0;


    private bool isCreate = false;
    private GameObject createdEnemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        createdEnemy = Instantiate(updown_enemy);
        createdEnemy.transform.position = new Vector3(xPoint, yPoint, 0);
        if (updown_enemy == null || enctrl2 == null || plctrl == null)
        {
            print("設定が足りていません");
        }

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!enctrl2.DeadJudge() && plctrl.DownJudge() && isCreate == false)
        {
            createdEnemy = Instantiate(updown_enemy);
            createdEnemy.transform.position = new Vector3(xPoint, yPoint, 0);
            isCreate = true;
        }

        if(enctrl2.DeadJudge() && isCreate == true)
        {
            print("isCreateをfalseにしています");
            isCreate = false;
        }
        */
    }

    //経由地点の数を返す
    public int NumberofMovePoint()
    {
        return movePoint.Length;
    }

    public GameObject serveMovePoint(int n)
    {
        return movePoint[n];
    }
}
