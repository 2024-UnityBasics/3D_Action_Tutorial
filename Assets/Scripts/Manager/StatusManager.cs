using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    [SerializeField] GameObject MainObject; //���̃X�N���v�g���A�^�b�`����I�u�W�F�N�g
    [SerializeField] int hp = 1;             //hp���ݒl
    [SerializeField] int maxHp = 1;          //������maxHp���p����ۂɎg�p

    [SerializeField] int attackDamage = 10; // ���̃I�u�W�F�N�g�̍U����

    [SerializeField] GameObject destroyEffect;  //���j�G�t�F�N�g
    [SerializeField] GameObject damageEffect;   //��e�G�t�F�N�g

    // Update is called once per frame
    void Update()
    {
        //hp��0�ȉ��Ȃ�A���j�G�t�F�N�g�𐶐�����Main��j��
        if (hp <= 0)
        {
            DestoryMainObject();
        }
    }

    // ��_���[�W���̏���
    public void Damage(int damage)
    {
        Debug.Log($"{name} �� {damage} �_���[�W���󂯂܂�");

        hp -= damage;                               // ����damage�̕�����HP������

        var effect = Instantiate(damageEffect);     // �_���[�W�G�t�F�N�g�̐���
        effect.transform.position = transform.position; // �_���[�W�G�t�F�N�g�̐����ꏊ�̎w��

        DamagePopupManager manager = FindObjectOfType<DamagePopupManager>(); // Manager������
        manager.ShowDamage(damage, transform.position); // �_���[�W�|�b�v�A�b�v�\��
    }

    // �A�^�b�`���ꂽ�I�u�W�F�N�g���j�󂳂��ۂ̏���
    private void DestoryMainObject()
    {
        hp = 0;
        var effect = Instantiate(destroyEffect);
        effect.transform.position = transform.position;
        Destroy(effect, 5);
        Destroy(MainObject);
    }

    // �U���͂�Ԃ��֐�
    public int GetDamageAmount()
    {
        return attackDamage;
    }

}
