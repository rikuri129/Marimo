using UnityEngine;

public class Sample : MonoBehaviour
{
    private bool isDown = false;
    private GameObject frame;

    void Start()
    {
        frame = GameObject.Find("frame_0");
    }

    // Update is called once per frame
    void Update()
    {
        if(isDown)
        {
            Debug.Log("Hold");
        }
    }

    public void OnButtonDown()
    {
        Debug.Log("Down");
        isDown = true;
    }

    public void OnButtonUp()
    {
        Debug.Log("Up");
        isDown = false;
    }
}
