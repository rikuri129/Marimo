using UnityEngine;
using TMPro;

public class TimeDirector : MonoBehaviour
{
    private TextMeshProUGUI timeText = null;
    private float time;

    void Start()
    {
        //インスタンスを取得 
        if (GameManager.instance != null)
        {
            timeText = GetComponent<TextMeshProUGUI>();
            time = GameManager.instance.defaultTime;

            timeText.text = "TIME " + time.ToString("###");
        }
        else
        {
            Debug.Log("GameManager がありません");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
            timeText.text = "TIME " + time.ToString("000");
        }
        else
        {
            time = 0.0f;
            timeText.text = "TIME 000";
            GameManager.instance.isGameOver = true;
        }
    }
}
