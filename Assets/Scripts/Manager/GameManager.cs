using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManagerのインスタンスにアクセスするためのプロパティ（シングルトンパターン）
    // 他のクラスからは GameManager.Instance でこのインスタンスにアクセスできます
    public static GameManager Instance { get; private set; }

    // ゲームクリアの状態を保持する変数
    // クリアしたかどうかのフラグです
    private bool isGameCleared = false;

    private void Awake()
    {
        // Awakeはオブジェクトが生成されるときに呼ばれるメソッドです
        // ここでシングルトンパターンを実現しています

        // すでにGameManagerのインスタンスが存在している場合、現在のオブジェクトは破棄します
        if (Instance != null && Instance != this)
        {
            // 他のインスタンスが存在する場合、重複して作らないようにオブジェクトを破棄
            Destroy(gameObject);
        }
        else
        {
            // GameManagerのインスタンスを管理するため、最初に作成されたインスタンスを保持します
            Instance = this;
        }
    }

    /// <summary>
    /// ボスが倒された際に呼び出される関数
    /// ゲームクリアの判定を行います
    /// </summary>
    public void NotifyBossDefeat()
    {
        // ゲームがすでにクリアされている場合、もう一度クリア判定を行わないようにします
        if (isGameCleared)
        {
            return; // すでにゲームクリアになっている場合は、処理をスキップ
        }

        // ゲームクリア時の処理を開始
        Debug.Log("Game Cleared!");  // ゲームクリアのログを表示

        // ゲームクリア状態をtrueにして、二度とクリア処理を行わないようにする
        isGameCleared = true;

        // ゲームクリア時の追加処理（例: UI更新やシーン遷移など）
        HandleGameClear();
    }

    /// <summary>
    /// ゲームクリア時に実行する具体的な処理をまとめたメソッド
    /// </summary>
    private void HandleGameClear()
    {
        // ゲームクリア時に行いたい処理をここに書きます
        Debug.Log("Congratulations! You have cleared the game!");
        // クリアUIの呼び出し
        UIManager.Instance.ActiveClearUI();

        // 例: ゲームクリアのUIを表示したり、シーンを遷移させたりする処理
        // SceneManager.LoadScene("GameClearScene"); // 例えば、ゲームクリア後に別のシーンに遷移する場合
    }
}
