using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


// DamageTypeをクラス外で宣言
public enum DamageType
{
    Normal,          // 通常ダメージ
    Critical,        // クリティカルダメージ
    SuperCritical,   // スーパークリティカル
    HyperCritical    // ハイパークリティカル
}

public class StatusManager : MonoBehaviour
{
    [SerializeField] GameObject MainObject; //このスクリプトをアタッチするオブジェクト
    [SerializeField] int hp = 1;             //hp現在値
    [SerializeField] int maxHp = 1;          //いずれmaxHp利用する際に使用

    [SerializeField] int attackDamage = 10; // このオブジェクトの攻撃力

    [SerializeField] GameObject destroyEffect;  //撃破エフェクト
    [SerializeField] GameObject damageEffect;   //被弾エフェクト

    [SerializeField] float criticalRate = 0.3f; // クリティカル発生率（0～1）
    [SerializeField] float criticalMultiplier = 2f; // クリティカル発生時のダメージ倍率

    // Update is called once per frame
    void Update()
    {
        //hpが0以下なら、撃破エフェクトを生成してMainを破壊
        if (hp <= 0)
        {
            BossNotifier bossNotifier = GetComponent<BossNotifier>();
            if (bossNotifier != null) // このオブジェクトがボスの場合
            {
                bossNotifier.Defeat(); // ボス専用の処理を呼び出す
            }

            DestoryMainObject();
        }
    }

    // 被ダメージ時の処理
    public void Damage(int baseDamage ,float takeCrit,Vector3 attackPoint)
    {
        DamageType damageType;        // DamageTypeを宣言

        float finalDamage;  // 計算後の最終ダメージ

        // ダメージ計算を専用関数化
        DamageCalc(baseDamage, out damageType, out finalDamage, takeCrit);

        // HPを減少
        hp -= Mathf.RoundToInt(finalDamage);

        var effect = Instantiate(damageEffect);     // ダメージエフェクトの生成
        effect.transform.position = attackPoint; // ダメージエフェクトの生成場所の指定

        DamagePopupManager manager = FindObjectOfType<DamagePopupManager>(); // Managerを検索
        manager.ShowDamage(Mathf.RoundToInt(finalDamage), attackPoint, damageType); // ダメージポップアップ表示

        HPGageUpdateUI();

    }

    private void HPGageUpdateUI()
    {
        // UIManager にHP更新を通知
        if (this.gameObject.CompareTag("Player")) // プレイヤーの場合にのみUI更新
        {
            Debug.Log("UI");
            UIManager.Instance.UpdateHPBar(hp, maxHp);
        }
    }

    private void DamageCalc(int baseDamage, out DamageType damageType, out float finalDamage, float takeCrit)
    {
        // クリティカル判定
        int criticalHits = 0;  // クリティカルが発生した回数
        float remainingRate = takeCrit; // criticalRateが1を超えた場合に、繰り返しの回数を制御


        // ランダム補正を計算（-20%～+20%の範囲）
        float randomFactor = Random.Range(0.8f, 1.2f);
        finalDamage = baseDamage * randomFactor;


        // remainingRateに基づいて繰り返し
        while (remainingRate > 0)
        {
            // remainingRate が 1 より大きい場合は確実にクリティカルを加算
            if (remainingRate >= 1.0f)
            {
                criticalHits++; // 1回目のクリティカル
                remainingRate -= 1.0f; // 1を引いて次に進む
            }
            else if (Random.value < remainingRate)
            {
                // remainingRate が 1 以下のとき、確率判定
                criticalHits++; // クリティカル判定成功
                break; // 1回判定したら終了
            }
            else { break; } //クリティカル判定に失敗したら終了
        }

        // クリティカル回数に基づいてダメージを調整
        switch (criticalHits)
        {
            case 0:
                // クリティカル発生しなかった場合（通常ダメージ）
                damageType = DamageType.Normal;
                break;

            case 1:
                // 1回目のクリティカル（通常クリティカル）
                finalDamage *= criticalMultiplier;  // 通常クリティカル（倍率はcriticalMultiplier）
                damageType = DamageType.Critical;
                break;

            case 2:
                // 2回目のクリティカル（SuperCritical）
                finalDamage *= criticalMultiplier * 2f;  // SuperCritical（倍率は2倍）
                damageType = DamageType.SuperCritical;
                break;

            case 3:
                // 3回目のクリティカル（HyperCritical）
                finalDamage *= criticalMultiplier * 3f;  // HyperCritical（倍率は3倍）
                damageType = DamageType.HyperCritical;
                break;

            default:
                // それ以上のクリティカル（倍率はさらに増加）
                finalDamage *= criticalMultiplier * (1 + (criticalHits - 1) * 1f);  // 1回目以降はさらに倍増
                damageType = DamageType.HyperCritical;  // クリティカル段階を割り当てる（仮にDamageTypeの最大値を設定）
                break;
        }
    }

    // アタッチされたオブジェクトが破壊される際の処理
    private void DestoryMainObject()
    {
        hp = 0;
        var effect = Instantiate(destroyEffect);
        effect.transform.position = transform.position;
        Destroy(effect, 5);

        // DropTableを取得してアイテムドロップ
        DropTable dropTable = GetComponentInChildren<DropTable>();
        if (dropTable != null)
        {
            dropTable.DropItems();
        }

        Destroy(MainObject);
    }

    // 攻撃力を返す関数
    public int GetDamageAmount()
    {
        return attackDamage;
    }
    public float GetCritAmount()
    {
        return criticalRate;
    }

    // クリティカル率を増加させる汎用的な関数
    public void IncreaseCritRate(float amount)
    {
        criticalRate += amount;

        // ログ (デバッグ用)
        Debug.Log($"クリティカル率が {amount} 増加しました。現在のクリティカル率: {criticalRate}");
    }
}
