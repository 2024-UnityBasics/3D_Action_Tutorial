using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSearchCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // �w�肳�ꂽ�^�O�ƈ�v����I�u�W�F�N�g�ƐڐG�����ꍇ
        if (other.CompareTag("Player"))
        {
            Debug.Log("Awake");
            BossAction bossAction = GetComponentInParent<BossAction>();
            if (bossAction != null)
            {
                bossAction.Activate();
            }
        }
    }
}