using TMPro;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    public GameObject damagePopupPrefab; // ダメージポップアップのPrefab
    public Canvas canvas;                // 画面に表示されるCanvas（Screen Space）

    // ダメージを受けた時に呼ばれる関数
    public void ShowDamage(int damage, Vector3 position)
    {
        // ワールド座標をスクリーン座標に変換
        Vector3 screenPos = Camera.main.WorldToScreenPoint(position);

        // ダメージポップアップを生成
        GameObject popup = Instantiate(damagePopupPrefab, canvas.transform);

        // 生成したポップアップの位置をスクリーン座標に設定
        RectTransform rectTransform = popup.GetComponent<RectTransform>();
        rectTransform.position = screenPos;

        // ポップアップのテキストを設定
        DamagePopup popupScript = popup.GetComponent<DamagePopup>();
        popupScript.ShowDamage(damage);
    }
}
