using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    [SerializeField] GameObject MainObject; //���̃X�N���v�g���A�^�b�`����I�u�W�F�N�g
    [SerializeField] int hp = 1;             //hp���ݒl
    [SerializeField] int maxHp = 1;          //������maxHp���p����ۂɎg�p

    [SerializeField] GameObject destroyEffect;  //���j�G�t�F�N�g
    [SerializeField] GameObject damageEffect;   //��e�G�t�F�N�g

    [SerializeField] string TagName;            //�����蔻��ƂȂ�^�O

    // Update is called once per frame
    void Update()
    {
        //hp��0�ȉ��Ȃ�A���j�G�t�F�N�g�𐶐�����Main��j��
        if (hp <= 0)
        {
            DestoryMainObject();
        }
    }

    public void Damage()
    {
        hp--;
        var effect = Instantiate(damageEffect);
        effect.transform.position = transform.position;
    }
    private void DestoryMainObject()
    {
        hp = 0;
        var effect = Instantiate(destroyEffect);
        effect.transform.position = transform.position;
        Destroy(effect, 5);
        Destroy(MainObject);
    }

}
