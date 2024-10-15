using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //��{�̉^���������i�d�͗����j

public class GravityTest : MonoBehaviour
{
    Vector3 acceleration;   // ���̂̉����x
    Vector3 velocity;       // ���̂̑��x
    Vector3 position;       // ���̂̈ʒu

    private void Start()
    {
        //���̂̏����x
        velocity = Vector3.zero;
        //���̂̏����ʒu
        position = transform.position;
    }
    void Update()
    {
        acceleration = Vector3.zero; // �����x��0�Ƀ��Z�b�g�i���d���������Ȃ����߁j

        //�d�͉����x(g)��^����
        acceleration += new Vector3(0, -9.8f, 0);

        // ���݂̑��x���X�V
        velocity += acceleration * Time.deltaTime;
        // ���݂̈ʒu���X�V
        position += velocity * Time.deltaTime;

        // �I�u�W�F�N�g�̈ʒu���X�V
        transform.position = position;

        /////////////////////////////////
        /// �^���������ŏ����ƁA
        /// ���x(v) = g * t
        /// ��������
        /// ��������(d) = (1/2) * g * t^2
        /// ��L���͂܂�ϕ��Ȃ̂ŁATime.deltaTime���Ƃɒ����������ƂŐϕ��Ɠ������ʂ�������
        /////////////////////////////////
    }
}
