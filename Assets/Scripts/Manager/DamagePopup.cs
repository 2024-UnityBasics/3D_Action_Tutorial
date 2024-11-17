using TMPro;  // TextMeshPro���g�p���邽�߂̖��O���
using UnityEngine;  // Unity�@�\���g�p���邽�߂̖��O���
using System.Collections;  // �R���[�`�����g�����߂̖��O���

public class DamagePopup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageText; // �_���[�W���l��\������TextMeshProUGUI
    [SerializeField] Color startColor;           // �e�L�X�g�̏����F

    private Vector3 initialPosition;        // popup�����ʒu
    [SerializeField] float moveSpeed = 1f;  // popup�ړ��X�s�[�h�̐ݒ�

    // �_���[�W��\������֐�
    public void ShowDamage(int damage)
    {
        // �����ʒu���L�^
        initialPosition = transform.position;

        damageText.text = damage.ToString();  // �_���[�W���l�𕶎���ɕϊ����Đݒ�
        damageText.color = new Color(startColor.r, startColor.g, startColor.b, 1f); // ���S�ɕ\��

        StartCoroutine(FadeOut());  // �t�F�[�h�A�E�g�������R���[�`���ŊJ�n
    }

    // �_���[�W�e�L�X�g���t�F�[�h�A�E�g������R���[�`��
    private IEnumerator FadeOut()
    {
        float time = 0;  // �o�ߎ��Ԃ̏�����

        // 1�b�ԂŃt�F�[�h�A�E�g
        while (time < 1f)
        {
            time += Time.deltaTime;  // ���Ԃ����Z
            float alpha = Mathf.Lerp(1f, 0f, time);  // �A���t�@��1����0�ɕ⊮
            damageText.color = new Color(startColor.r, startColor.g, startColor.b, alpha);  // �V�����F��ݒ�

            // ������ւ̈ړ��i�y���㏸������j
            transform.position = initialPosition + new Vector3(0, Mathf.Lerp(0, 1f, time) * moveSpeed, 0);

            yield return null;  // ���̃t���[���܂őҋ@
        }

        Destroy(gameObject);  // �e�L�X�g����������I�u�W�F�N�g���폜
    }
}
