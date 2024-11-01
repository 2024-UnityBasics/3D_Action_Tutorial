using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int currentEnemyCount = 0; // 現在の敵数

    private void Start()
    {
        // 敵の数を定期的にチェックするコルーチンを開始
        StartCoroutine(CheckEnemyCountRoutine());
    }

    // 敵の数を定期的に確認するコルーチン
    private IEnumerator CheckEnemyCountRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // 1秒ごとに確認（必要に応じて調整）

            // Enemyタグを持つオブジェクトの数を取得(.Lengthで数が取得できる)
            currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

            // 敵がすべて倒された場合
            if (currentEnemyCount <= 0)
            {
                Debug.Log("Enemy Clear");
            }
        }
    }
}