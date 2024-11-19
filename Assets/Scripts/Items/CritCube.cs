using UnityEngine;

public class CritCube : MonoBehaviour
{
    [SerializeField]
    private float critIncreaseAmount = 0.1f; // 増加量（デフォルトは0.1）

    private bool isCollected = false;

    // Playerに接触したときに呼び出されるメソッド
    public void GetCritCube()
    {
        if (isCollected) return;  // すでに取得済みなら処理しない

        // StatusManagerの参照を取得
        StatusManager statusManager = FindObjectOfType<StatusManager>();
        if (statusManager != null)
        {
            isCollected = true;
            // IncreaseCritRateを呼び出し、引数として増加量を渡す
            statusManager.IncreaseCritRate(critIncreaseAmount);

            // ログ (デバッグ用)
            Debug.Log($"CritCube取得！ クリティカル率が {critIncreaseAmount} 増加しました。");

            // このアイテムを消去
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("PlayerにStatusManagerが見つかりません。");
        }
    }
}
