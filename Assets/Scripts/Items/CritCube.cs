using UnityEngine;

public class CritCube : MonoBehaviour
{
    [SerializeField]
    private float critIncreaseAmount = 0.1f; // �����ʁi�f�t�H���g��0.1�j

    private bool isCollected = false;

    // Player�ɐڐG�����Ƃ��ɌĂяo����郁�\�b�h
    public void GetCritCube()
    {
        if (isCollected) return;  // ���łɎ擾�ς݂Ȃ珈�����Ȃ�

        // StatusManager�̎Q�Ƃ��擾
        StatusManager statusManager = FindObjectOfType<StatusManager>();
        if (statusManager != null)
        {
            isCollected = true;
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
