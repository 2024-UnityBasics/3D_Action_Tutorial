using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRutine : MonoBehaviour
{
    Transform target;

    [SerializeField] float swayAmplitude = 0.5f;   // �h��̐U��
    [SerializeField] float swayFrequency = 1f;     // �h��̑���
    Vector3 initialLocalPosition;                 // �����̃��[�J���ʒu

    void Start()
    {
        // �ǔ��Ώۂ̎擾�iPlayer��z��j
        target = GameObject.Find("Player").GetComponent<Transform>();

        // �����̃��[�J���ʒu���L�^
        initialLocalPosition = transform.localPosition;
    }

    void Update()
    {
        // Player�̕���������
        transform.LookAt(target.position);

        // ���[�J�����W�n�ł̂���瓮���ǉ�
        float swayX = Mathf.Sin(Time.time * swayFrequency) * swayAmplitude;  // ���E�̗h��
        float swayZ = Mathf.Cos(Time.time * swayFrequency) * swayAmplitude;  // �O��̗h��

        // �����̃��[�J���ʒu�ɗh���������
        transform.localPosition = initialLocalPosition + new Vector3(swayX, 0, swayZ);
    }
}
