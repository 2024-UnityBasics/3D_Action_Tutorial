using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] string tagName;            //�����蔻��ƂȂ�^�O
    private StatusManager statusManager;

    void Start()
    {
        // �e�I�u�W�F�N�g�� StatusManager ���擾
        statusManager = GetComponentInParent<StatusManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // �w�肳�ꂽ�^�O�ƈ�v����I�u�W�F�N�g�ƐڐG�����ꍇ
        if (other.CompareTag(tagName))
        {
            Debug.Log("Hit registered by DamageTrigger");

            // �e�� StatusManager �Ƀ_���[�W��ʒm
            if (statusManager != null)
            {
                statusManager.Damage();
            }
        }
    }
}
