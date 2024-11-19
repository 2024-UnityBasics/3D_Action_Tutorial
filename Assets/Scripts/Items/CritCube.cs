using UnityEngine;

public class CritCube : MonoBehaviour
{
    [SerializeField]
    private float critIncreaseAmount = 0.1f; // 増加量（デフォルトは0.1）

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // StatusManagerの参照を取得
            StatusManager statusManager = other.GetComponent<StatusManager>();
            if (statusManager != null)
            {
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
}
