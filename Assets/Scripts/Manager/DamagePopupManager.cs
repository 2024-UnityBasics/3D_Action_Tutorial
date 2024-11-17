using TMPro;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    public GameObject damagePopupPrefab; // ダメージポップアップのPrefab
    public Canvas canvas;                // 画面に表示されるCanvas（Screen Space）

    [SerializeField] Color normalPopupColor = Color.white; // 通常ダメージのポップアップ色
    [SerializeField] Color criticalPopupColor = Color.red; // クリティカルヒット時のポップアップ色
    [SerializeField] Color superCriticalPopupColor = Color.red; // スーパークリティカル時のポップアップ色
    [SerializeField] Color hyperCriticalPopupColor = Color.red; // ハイパークリティカル時のポップアップ色

    [SerializeField] float fontSizeScale = 1f;  // フォントサイズのスケール

    // ダメージを受けた時に呼ばれる関数
    public void ShowDamage(int damage, Vector3 position, DamageType damageType)
    {
        // ワールド座標をスクリーン座標に変換
        Vector3 screenPos = Camera.main.WorldToScreenPoint(position);
        // ポップアップ色
        Color popupColor;

        switch (damageType)
        {
            case DamageType.HyperCritical:
                popupColor = hyperCriticalPopupColor;  // クリティカルポップアップ色
                fontSizeScale = 2.5f;    // フォントサイズ係数を拡大
                break;

            case DamageType.SuperCritical:
                popupColor = superCriticalPopupColor;  // クリティカルポップアップ色
                fontSizeScale = 2.0f;    // フォントサイズ係数を拡大
                break;

            case DamageType.Critical:
                popupColor = criticalPopupColor;  // クリティカルポップアップ色
                fontSizeScale = 1.5f;    // フォントサイズ係数を拡大
                break;

            case DamageType.Normal:
            default:
                popupColor = normalPopupColor;    // 通常ポップアップ色
                fontSizeScale = 1.0f;    // フォントサイズ係数を設定

                break;
        }

        // ダメージポップアップを生成
        GameObject popup = Instantiate(damagePopupPrefab, canvas.transform);

        // 生成したポップアップの位置をスクリーン座標に設定
        RectTransform rectTransform = popup.GetComponent<RectTransform>();
        rectTransform.position = screenPos;

        // ポップアップのテキストを設定
        DamagePopup popupScript = popup.GetComponent<DamagePopup>();
        popupScript.ShowDamage(damage,popupColor,fontSizeScale);
    }
}
