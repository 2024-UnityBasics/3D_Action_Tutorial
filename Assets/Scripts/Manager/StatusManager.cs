using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            DestoryMainObject();
        }
    }

    // 被ダメージ時の処理
    public void Damage(int baseDamage)
    {
        // ランダム補正を計算（-20%～+20%の範囲）
        float randomFactor = Random.Range(0.8f, 1.2f);
        float finalDamage = baseDamage * randomFactor;

        // クリティカル判定
        if (Random.value < criticalRate) // Random.value は 0～1の範囲でランダムな値を返す
        {
            finalDamage *= criticalMultiplier; // クリティカル発生でダメージを増加
            Debug.Log($"{name} はクリティカルヒット！ ダメージ: {finalDamage}");
        }
        else
        {
            Debug.Log($"{name} に通常ダメージ: {finalDamage}");
        }

        // HPを減少
        hp -= Mathf.RoundToInt(finalDamage);

        var effect = Instantiate(damageEffect);     // ダメージエフェクトの生成
        effect.transform.position = transform.position; // ダメージエフェクトの生成場所の指定

        DamagePopupManager manager = FindObjectOfType<DamagePopupManager>(); // Managerを検索
        manager.ShowDamage(Mathf.RoundToInt(finalDamage), transform.position); // ダメージポップアップ表示
    }

    // アタッチされたオブジェクトが破壊される際の処理
    private void DestoryMainObject()
    {
        hp = 0;
        var effect = Instantiate(destroyEffect);
        effect.transform.position = transform.position;
        Destroy(effect, 5);
        Destroy(MainObject);
    }

    // 攻撃力を返す関数
    public int GetDamageAmount()
    {
        return attackDamage;
    }

}
