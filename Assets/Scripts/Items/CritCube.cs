using UnityEngine;

public class CritCube : MonoBehaviour
{
    [SerializeField]
    private float critIncreaseAmount = 0.1f; // �����ʁi�f�t�H���g��0.1�j

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // StatusManager�̎Q�Ƃ��擾
            StatusManager statusManager = other.GetComponent<StatusManager>();
            if (statusManager != null)
            {
                // IncreaseCritRate���Ăяo���A�����Ƃ��đ����ʂ�n��
                statusManager.IncreaseCritRate(critIncreaseAmount);

                // ���O (�f�o�b�O�p)
                Debug.Log($"CritCube�擾�I �N���e�B�J������ {critIncreaseAmount} �������܂����B");

                // ���̃A�C�e��������
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("Player��StatusManager��������܂���B");
            }
        }
    }
}
