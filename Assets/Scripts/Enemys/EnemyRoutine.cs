using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRutine : MonoBehaviour
{
    Transform target;

    [SerializeField] float swayAmplitude = 0.5f;   // 揺れの振幅
    [SerializeField] float swayFrequency = 1f;     // 揺れの速さ
    Vector3 initialLocalPosition;                 // 初期のローカル位置

    void Start()
    {
        // 追尾対象の取得（Playerを想定）
        target = GameObject.Find("Player").GetComponent<Transform>();

        // 初期のローカル位置を記録
        initialLocalPosition = transform.localPosition;
    }

    void Update()
    {
        // Playerの方向を向く
        transform.LookAt(target.position);

        // ローカル座標系でのゆらゆら動作を追加
        float swayX = Mathf.Sin(Time.time * swayFrequency) * swayAmplitude;  // 左右の揺れ
        float swayZ = Mathf.Cos(Time.time * swayFrequency) * swayAmplitude;  // 前後の揺れ

        // 初期のローカル位置に揺れを加える
        transform.localPosition = initialLocalPosition + new Vector3(swayX, 0, swayZ);
    }
}
