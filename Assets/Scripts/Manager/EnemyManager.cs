using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int currentEnemyCount = 0; // ���݂̓G��

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

            // Enemy�^�O�����I�u�W�F�N�g�̐����擾(.Length�Ő����擾�ł���)
            currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

            // �G�����ׂē|���ꂽ�ꍇ
            if (currentEnemyCount <= 0)
            {
                Debug.Log("Enemy Clear");
            }
        }
    }
}