using UnityEngine;

public class BoundDirector : MonoBehaviour
{
    [Header("これを踏んだときのプレーヤが跳ねる高さ")] public float BoundHeight;

    /// <summary>
    /// このオブジェクトをプレーヤが踏んだかどうか
    /// </summary>
    [HideInInspector] public bool PlayerStepOn;
}
