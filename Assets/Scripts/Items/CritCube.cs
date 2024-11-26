using UnityEngine;

public class CritCube : MonoBehaviour
{
    [SerializeField]
    private float critIncreaseAmount = 0.1f; // �����ʁi�f�t�H���g��0.1�j

    private bool isCollected = false;

    [SerializeField] GameObject itemGetEffect;

    // Player�ɐڐG�����Ƃ��ɌĂяo����郁�\�b�h
    public void GetCritCube(Collider other)
    {
        if (isCollected) return;  // ���łɎ擾�ς݂Ȃ珈�����Ȃ�

        // ����Collider����StatusManager���擾�iPlayer��StatusManager�̂݁j
        StatusManager statusManager = other.GetComponentInParent<StatusManager>();

        if (statusManager != null)
        {
            isCollected = true;
            // IncreaseCritRate���Ăяo���A�����Ƃ��đ����ʂ�n��
            statusManager.IncreaseCritRate(critIncreaseAmount);

            // ���O (�f�o�b�O�p)
            Debug.Log($"CritCube�擾�I �N���e�B�J������ {critIncreaseAmount} �������܂����B");

            // �A�C�e���擾���̃G�t�F�N�g
            var effect = Instantiate(itemGetEffect);
            effect.transform.position = transform.position;

            // ���̃A�C�e��������
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Player��StatusManager��������܂���B");
        }
    }
}
