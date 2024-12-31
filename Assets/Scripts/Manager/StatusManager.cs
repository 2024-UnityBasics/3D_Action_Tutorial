using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


// DamageType���N���X�O�Ő錾
public enum DamageType
{
    Normal,          // �ʏ�_���[�W
    Critical,        // �N���e�B�J���_���[�W
    SuperCritical,   // �X�[�p�[�N���e�B�J��
    HyperCritical    // �n�C�p�[�N���e�B�J��
}

public class StatusManager : MonoBehaviour
{
    [SerializeField] GameObject MainObject; //���̃X�N���v�g���A�^�b�`����I�u�W�F�N�g
    [SerializeField] int hp = 1;             //hp���ݒl
    [SerializeField] int maxHp = 1;          //������maxHp���p����ۂɎg�p

    [SerializeField] int attackDamage = 10; // ���̃I�u�W�F�N�g�̍U����

    [SerializeField] GameObject destroyEffect;  //���j�G�t�F�N�g
    [SerializeField] GameObject damageEffect;   //��e�G�t�F�N�g

    [SerializeField] float criticalRate = 0.3f; // �N���e�B�J���������i0�`1�j
    [SerializeField] float criticalMultiplier = 2f; // �N���e�B�J���������̃_���[�W�{��

    // Update is called once per frame
    void Update()
    {
        //hp��0�ȉ��Ȃ�A���j�G�t�F�N�g�𐶐�����Main��j��
        if (hp <= 0)
        {
            BossNotifier bossNotifier = GetComponent<BossNotifier>();
            if (bossNotifier != null) // ���̃I�u�W�F�N�g���{�X�̏ꍇ
            {
                bossNotifier.Defeat(); // �{�X��p�̏������Ăяo��
            }

            DestoryMainObject();
        }
    }

    // ��_���[�W���̏���
    public void Damage(int baseDamage ,float takeCrit,Vector3 attackPoint)
    {
        DamageType damageType;        // DamageType��錾

        float finalDamage;  // �v�Z��̍ŏI�_���[�W

        // �_���[�W�v�Z���p�֐���
        DamageCalc(baseDamage, out damageType, out finalDamage, takeCrit);

        // HP������
        hp -= Mathf.RoundToInt(finalDamage);

        var effect = Instantiate(damageEffect);     // �_���[�W�G�t�F�N�g�̐���
        effect.transform.position = attackPoint; // �_���[�W�G�t�F�N�g�̐����ꏊ�̎w��

        DamagePopupManager manager = FindObjectOfType<DamagePopupManager>(); // Manager������
        manager.ShowDamage(Mathf.RoundToInt(finalDamage), attackPoint, damageType); // �_���[�W�|�b�v�A�b�v�\��

        HPGageUpdateUI();

    }

    private void HPGageUpdateUI()
    {
        // UIManager ��HP�X�V��ʒm
        if (this.gameObject.CompareTag("Player")) // �v���C���[�̏ꍇ�ɂ̂�UI�X�V
        {
            Debug.Log("UI");
            UIManager.Instance.UpdateHPBar(hp, maxHp);
        }
    }

    private void DamageCalc(int baseDamage, out DamageType damageType, out float finalDamage, float takeCrit)
    {
        // �N���e�B�J������
        int criticalHits = 0;  // �N���e�B�J��������������
        float remainingRate = takeCrit; // criticalRate��1�𒴂����ꍇ�ɁA�J��Ԃ��̉񐔂𐧌�


        // �����_���␳���v�Z�i-20%�`+20%�͈̔́j
        float randomFactor = Random.Range(0.8f, 1.2f);
        finalDamage = baseDamage * randomFactor;


        // remainingRate�Ɋ�Â��ČJ��Ԃ�
        while (remainingRate > 0)
        {
            // remainingRate �� 1 ���傫���ꍇ�͊m���ɃN���e�B�J�������Z
            if (remainingRate >= 1.0f)
            {
                criticalHits++; // 1��ڂ̃N���e�B�J��
                remainingRate -= 1.0f; // 1�������Ď��ɐi��
            }
            else if (Random.value < remainingRate)
            {
                // remainingRate �� 1 �ȉ��̂Ƃ��A�m������
                criticalHits++; // �N���e�B�J�����萬��
                break; // 1�񔻒肵����I��
            }
            else { break; } //�N���e�B�J������Ɏ��s������I��
        }

        // �N���e�B�J���񐔂Ɋ�Â��ă_���[�W�𒲐�
        switch (criticalHits)
        {
            case 0:
                // �N���e�B�J���������Ȃ������ꍇ�i�ʏ�_���[�W�j
                damageType = DamageType.Normal;
                break;

            case 1:
                // 1��ڂ̃N���e�B�J���i�ʏ�N���e�B�J���j
                finalDamage *= criticalMultiplier;  // �ʏ�N���e�B�J���i�{����criticalMultiplier�j
                damageType = DamageType.Critical;
                break;

            case 2:
                // 2��ڂ̃N���e�B�J���iSuperCritical�j
                finalDamage *= criticalMultiplier * 2f;  // SuperCritical�i�{����2�{�j
                damageType = DamageType.SuperCritical;
                break;

            case 3:
                // 3��ڂ̃N���e�B�J���iHyperCritical�j
                finalDamage *= criticalMultiplier * 3f;  // HyperCritical�i�{����3�{�j
                damageType = DamageType.HyperCritical;
                break;

            default:
                // ����ȏ�̃N���e�B�J���i�{���͂���ɑ����j
                finalDamage *= criticalMultiplier * (1 + (criticalHits - 1) * 1f);  // 1��ڈȍ~�͂���ɔ{��
                damageType = DamageType.HyperCritical;  // �N���e�B�J���i�K�����蓖�Ă�i����DamageType�̍ő�l��ݒ�j
                break;
        }
    }

    // �A�^�b�`���ꂽ�I�u�W�F�N�g���j�󂳂��ۂ̏���
    private void DestoryMainObject()
    {
        hp = 0;
        var effect = Instantiate(destroyEffect);
        effect.transform.position = transform.position;
        Destroy(effect, 5);

        // DropTable���擾���ăA�C�e���h���b�v
        DropTable dropTable = GetComponentInChildren<DropTable>();
        if (dropTable != null)
        {
            dropTable.DropItems();
        }

        Destroy(MainObject);
    }

    // �U���͂�Ԃ��֐�
    public int GetDamageAmount()
    {
        return attackDamage;
    }
    public float GetCritAmount()
    {
        return criticalRate;
    }

    // �N���e�B�J�����𑝉�������ėp�I�Ȋ֐�
    public void IncreaseCritRate(float amount)
    {
        criticalRate += amount;

        // ���O (�f�o�b�O�p)
        Debug.Log($"�N���e�B�J������ {amount} �������܂����B���݂̃N���e�B�J����: {criticalRate}");
    }
}
