using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManager�̃C���X�^���X�ɃA�N�Z�X���邽�߂̃v���p�e�B�i�V���O���g���p�^�[���j
    // ���̃N���X����� GameManager.Instance �ł��̃C���X�^���X�ɃA�N�Z�X�ł��܂�
    public static GameManager Instance { get; private set; }

    // �Q�[���N���A�̏�Ԃ�ێ�����ϐ�
    // �N���A�������ǂ����̃t���O�ł�
    private bool isGameCleared = false;

    private void Awake()
    {
        // Awake�̓I�u�W�F�N�g�����������Ƃ��ɌĂ΂�郁�\�b�h�ł�
        // �����ŃV���O���g���p�^�[�����������Ă��܂�

        // ���ł�GameManager�̃C���X�^���X�����݂��Ă���ꍇ�A���݂̃I�u�W�F�N�g�͔j�����܂�
        if (Instance != null && Instance != this)
        {
            // ���̃C���X�^���X�����݂���ꍇ�A�d�����č��Ȃ��悤�ɃI�u�W�F�N�g��j��
            Destroy(gameObject);
        }
        else
        {
            // GameManager�̃C���X�^���X���Ǘ����邽�߁A�ŏ��ɍ쐬���ꂽ�C���X�^���X��ێ����܂�
            Instance = this;
        }
    }

    /// <summary>
    /// �{�X���|���ꂽ�ۂɌĂяo�����֐�
    /// �Q�[���N���A�̔�����s���܂�
    /// </summary>
    public void NotifyBossDefeat()
    {
        // �Q�[�������łɃN���A����Ă���ꍇ�A������x�N���A������s��Ȃ��悤�ɂ��܂�
        if (isGameCleared)
        {
            return; // ���łɃQ�[���N���A�ɂȂ��Ă���ꍇ�́A�������X�L�b�v
        }

        // �Q�[���N���A���̏������J�n
        Debug.Log("Game Cleared!");  // �Q�[���N���A�̃��O��\��

        // �Q�[���N���A��Ԃ�true�ɂ��āA��x�ƃN���A�������s��Ȃ��悤�ɂ���
        isGameCleared = true;

        // �Q�[���N���A���̒ǉ������i��: UI�X�V��V�[���J�ڂȂǁj
        HandleGameClear();
    }

    /// <summary>
    /// �Q�[���N���A���Ɏ��s�����̓I�ȏ������܂Ƃ߂����\�b�h
    /// </summary>
    private void HandleGameClear()
    {
        // �Q�[���N���A���ɍs�����������������ɏ����܂�
        Debug.Log("Congratulations! You have cleared the game!");
        // �N���AUI�̌Ăяo��
        UIManager.Instance.ActiveClearUI();

        // ��: �Q�[���N���A��UI��\��������A�V�[����J�ڂ������肷�鏈��
        // SceneManager.LoadScene("GameClearScene"); // �Ⴆ�΁A�Q�[���N���A��ɕʂ̃V�[���ɑJ�ڂ���ꍇ
    }
}
