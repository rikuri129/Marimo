using UnityEngine;

public class RLButtonDirector : MonoBehaviour
{
    private bool isPush = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonDown()
    {
        isPush = true;
    }

    public void OnButtonUp()
    {
        isPush= false;
    }

    public void OnButtonHold()
    {
        isPush = true;
    }

    public bool IsPush()
    {
        return isPush;
    }

    public bool IsNotPush()
    {
        return isPush;
    }

    public bool IsHold()
    {
        return isPush;
    }
}
