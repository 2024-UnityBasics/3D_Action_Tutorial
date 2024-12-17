using UnityEngine;
using System.Collections;

public class OldEnemyActionDash : MonoBehaviour
{
    [SerializeField] Transform target;              // Playerの位置を指定
    [SerializeField] float dashSpeed = 10f;         // 一定の突進速度
    [SerializeField] float dashDuration = 1f;       // 突進の効果時間
    [SerializeField] float dashCooldown = 2f;       // 突進後の待機時間

    private bool isDashing = false;       // 突進中かどうか
    private Rigidbody rb;                 // Rigidbodyコンポーネント
    private Vector3 dashDirection;        // 突進の方向

    //攻撃判定（近接武器）用のコライダー
    [SerializeField]
    Collider attackCollider;

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

                AttackColliderOn();

                // 突進が終了するまで、速度を固定して突進する
                while (Time.time < dashEndTime)
                {

                    rb.velocity = dashDirection * dashSpeed; // 突進方向に一定の速度で進む
                    yield return null; // 次のフレームへ
                }

                AttackColliderOff();

                // 突進終了後、速度を0にする
                rb.velocity = Vector3.zero;
                isDashing = false; // 突進が終了したのでフラグをリセット
                yield return new WaitForSeconds(dashCooldown); // 待機時間を設ける
            }
        }
    }

    void StartDash()
    {
        // プレイヤーの方向を向き、突進方向を計算
        transform.LookAt(target.position);
        dashDirection = (target.position - transform.position).normalized; // 突進の方向を一度だけ取得
        isDashing = true; // 突進中フラグを立てる
    }

    // 近接攻撃用のコライダーを有効にする関数
    void AttackColliderOn()
    {
        attackCollider.enabled = true;
        Debug.Log("Attack c on");

    }

    // 近接攻撃用のコライダーを無効にする関数
    void AttackColliderOff()
    {
        attackCollider.enabled = false;
        Debug.Log("Attack c off");

    }
}
