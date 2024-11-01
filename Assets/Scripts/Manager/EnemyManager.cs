using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int currentEnemyCount = 0; // ���݂̓G��
    private bool isExplorationPhase = false; // �T���t�F�[�Y�̐���t���O

    private void Start()
    {
        // �G�̐������I�Ƀ`�F�b�N����R���[�`�����J�n
        StartCoroutine(CheckEnemyCountRoutine());
    }

    // �G�̐������I�Ɋm�F����R���[�`��
    private IEnumerator CheckEnemyCountRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // 1�b���ƂɊm�F�i�K�v�ɉ����Ē����j

            // �f�o�b�O�p���O
            Debug.Log("Current Enemy Count: " + currentEnemyCount); // ���݂̓G�������O�ɏo��

            // Enemy�^�O�����I�u�W�F�N�g�̐����擾(.Length�Ő����擾�ł���)
            currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (currentEnemyCount <= 0 && !isExplorationPhase)
            {
                isExplorationPhase = true;  // �T���t�F�[�Y�ɐ؂�ւ�

                Debug.Log("Enemy Clear");   //�G�����ׂē|���ꂽ����A���o��t�F�[�Y�ύX�������Ă��悢

            // �G���ēx�o������܂őҋ@
            while (currentEnemyCount <= 0)
            {
                currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length; // �G�̐����Ċm�F
                yield return new WaitForSeconds(1f); // 1�b���ƂɍĊm�F
            }
                isExplorationPhase = false; // �ēx�G���o��������T���t�F�[�Y���I��
            }
        }
    }
}