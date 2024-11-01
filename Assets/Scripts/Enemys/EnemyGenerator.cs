using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // �G�̃v���n�u
    [SerializeField] private float spawnInterval = 0.2f; // �G�̏o���Ԋu
    [SerializeField] private int enemiesToSpawn = 5; // �e�t�F�[�Y�ŏo������G�̐�

    [SerializeField] private float spawnOffsetRange = 5f; // �����ʒu�̃����_���ȃI�t�Z�b�g�͈�

    private bool isSpawning = false; // �G�̐������J�n����Ă��邩�ǂ����̃t���O

    private void OnTriggerEnter(Collider other)
    {
        //�v���C���[�ɐڐG������Enemy��Spawn������
        if (other.CompareTag("Player") && !isSpawning)
        {
            Debug.Log("pop");
            isSpawning = true;              //�����J�n�t���O������A�������͍ĐڐG���Ă�Spawn����Ȃ�
            StartCoroutine(SpawnEnemies()); // �R���[�`�����J�n
        }
    }

    private IEnumerator SpawnEnemies()
    {

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // ���g�̈ʒu����Ƀ����_���ɃI�t�Z�b�g
            Vector3 spawnPosition = transform.position + new Vector3(
                Random.Range(-spawnOffsetRange, spawnOffsetRange),
                0f, // �����̃I�t�Z�b�g��0�ɐݒ�i�K�v�ɉ����ĕύX�\�j
                Random.Range(-spawnOffsetRange, spawnOffsetRange)
            );

            // �G���X�|�[���|�C���g�ɃX�|�[��������
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // �w�肳�ꂽ�Ԋu�Ŏ��̓G���X�|�[��������
            yield return new WaitForSeconds(spawnInterval);
        }

        // ����������������I�u�W�F�N�g��j��
        Destroy(gameObject);
    }

}
