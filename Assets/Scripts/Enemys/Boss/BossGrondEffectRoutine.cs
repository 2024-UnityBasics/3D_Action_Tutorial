using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGrondEffectRoutine : MonoBehaviour
{
    float speed = 10;
    Transform target;

    private void Start()
    {
        //�e�I�u�W�F�N�g��20�{�T�C�Y�Ȃ̂ŋً}���
        transform.localScale = new Vector3(0.05f,0.05f,0.05f);

        // �ǔ��Ώۂ̎擾�iPlayer��z��j
        target = GameObject.Find("Player").GetComponent<Transform>();

        var punchDirection = (target.position - transform.position).normalized;
        punchDirection.y = 0f;   // y���̈ړ��x��0�ɂ��Ēn�ʂƕ��s�ȍU���ɂ���悤�ɕύX�B�������Ζʂł̓����͖�����

        // ������]��ݒ�
        if (punchDirection != Vector3.zero) // �����ȃx�N�g���`�F�b�N
        {
            transform.rotation = Quaternion.LookRotation(punchDirection);
        }

    }
    void Update()

    {

        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }
}
