using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritCubeHitCollider : MonoBehaviour
{
    // CritCube�ւ̎Q��
    private CritCube critCube;
    void Start()
    {
        // �e�I�u�W�F�N�g�� StatusManager ���擾
        critCube = GetComponentInParent<CritCube>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"CritCube�R���C�_�[���m�I");
            // Player�ɐڐG������ACritCube�̏������Ăяo��
            critCube.GetCritCube();
        }
    }
}
