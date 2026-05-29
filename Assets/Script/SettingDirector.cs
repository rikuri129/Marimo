using UnityEngine;
using UnityEngine.UI;

public class SettingDirector : MonoBehaviour
{
    #region
    private Image img = null;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img = GetComponent<Image>();
        img.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openSetting()
    {
        img.gameObject.SetActive(true);
    }

    public void closeSetting() 
    {
        img.gameObject.SetActive(false);
    }
}
