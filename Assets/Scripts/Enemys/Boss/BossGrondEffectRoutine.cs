using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGrondEffectRoutine : MonoBehaviour
{
    float speed = 10;
    Transform target;

    private void Start()
    {
        //親オブジェクトが20倍サイズなので緊急回避
        transform.localScale = new Vector3(0.05f,0.05f,0.05f);

        // 追尾対象の取得（Playerを想定）
        target = GameObject.Find("Player").GetComponent<Transform>();

        var punchDirection = (target.position - transform.position).normalized;
        punchDirection.y = 0f;   // y軸の移動度を0にして地面と平行な攻撃にするように変更。ただし斜面での動きは未検証

        // 初期回転を設定
        if (punchDirection != Vector3.zero) // 無効なベクトルチェック
        {
            transform.rotation = Quaternion.LookRotation(punchDirection);
        }

    }
    void Update()

    {

        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }
}
