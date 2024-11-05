using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRutine : MonoBehaviour
{
    Transform target;

    [SerializeField] float swayAmplitude = 0.5f;   // �h��̐U��
    [SerializeField] float swayFrequency = 1f;     // �h��̑���
    Vector3 initialPosition;                       // �����̈ʒu
    Rigidbody rb;

    void Start()
    {
        // Rigidbody �R���|�[�l���g�̎擾
        rb = GetComponent<Rigidbody>();

        // �ǔ��Ώۂ̎擾�iPlayer��z��j
        target = GameObject.Find("Player").GetComponent<Transform>();

        // �����ʒu���L�^
        initialPosition = transform.position;
    }

    void Update()
    {
        // �v���C���[�̕���������
        transform.LookAt(target.position);

        // ���[�J�����W�n�ł̂���瓮���ǉ�
        float swayX = Mathf.Sin(Time.time * swayFrequency) * swayAmplitude;  // ���E�̗h��
        float swayZ = Mathf.Cos(Time.time * swayFrequency) * swayAmplitude;  // �O��̗h��

        // �����ʒu�ɗh����������ʒu���v�Z
        Vector3 targetPosition = initialPosition + new Vector3(swayX, 0, swayZ);

        // Rigidbody�ł̈ʒu�X�V
        rb.MovePosition(targetPosition);
    }
}
