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
    public void Damage(int damage)
    {
        Debug.Log($"{name} は {damage} ダメージを受けます");

        hp -= damage;                               // 引数damageの分だけHPを減少

        var effect = Instantiate(damageEffect);     // ダメージエフェクトの生成
        effect.transform.position = transform.position; // ダメージエフェクトの生成場所の指定

        DamagePopupManager manager = FindObjectOfType<DamagePopupManager>(); // Managerを検索
        manager.ShowDamage(damage, transform.position); // ダメージポップアップ表示
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
