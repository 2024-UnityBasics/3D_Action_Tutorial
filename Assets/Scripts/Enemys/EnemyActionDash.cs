using UnityEngine;
using System.Collections;

public class EnemyActionDash : MonoBehaviour
{
    public Transform target;              // Playerの位置を指定
    public float dashForce = 10f;         // 突進の力
    public float dashDuration = 1f;       // 突進の効果時間
    public float dashCooldown = 2f;       // 突進後の待機時間

    private bool isDashing = false;       // 突進中かどうか
    private Rigidbody rb;                 // Rigidbodyコンポーネント

    void Start()
    {
        // Rigidbodyコンポーネントの取得
        rb = GetComponent<Rigidbody>();

        // 追尾対象の取得（Playerを想定）
        target = GameObject.Find("Player").GetComponent<Transform>();
        StartCoroutine(EnemyDashRoutine());
    }

    private IEnumerator EnemyDashRoutine()
    {
        while (true)
        {
            if (!isDashing)
            {
                StartDash(); // 突進を開始

                // 突進中の処理
                float dashEndTime = Time.time + dashDuration; // 突進終了の時間を計算

                // 突進が終了するまで力を加える
                while (Time.time < dashEndTime)
                {
                    // 現在の向きを取得し、突進の方向を決定
                    Vector3 direction = (target.position - transform.position).normalized;
                    rb.AddForce(direction * dashForce, ForceMode.VelocityChange); // 力を加える
                    yield return null; // 次のフレームへ
                }

                isDashing = false; // 突進が終了したのでフラグをリセット
                yield return new WaitForSeconds(dashCooldown); // 待機時間を設ける
            }
        }
    }

    void StartDash()
    {
        // Playerの方向を向く
        transform.LookAt(target.position);
        isDashing = true; // 突進中フラグを立てる
    }
}
