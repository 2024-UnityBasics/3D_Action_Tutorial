using TMPro;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    public GameObject damagePopupPrefab; // �_���[�W�|�b�v�A�b�v��Prefab
    public Canvas canvas;                // ��ʂɕ\�������Canvas�iScreen Space�j

    [SerializeField] Color normalPopupColor = Color.white; // �ʏ�_���[�W�̃|�b�v�A�b�v�F
    [SerializeField] Color criticalPopupColor = Color.red; // �N���e�B�J���q�b�g���̃|�b�v�A�b�v�F
    [SerializeField] Color superCriticalPopupColor = Color.red; // �X�[�p�[�N���e�B�J�����̃|�b�v�A�b�v�F
    [SerializeField] Color hyperCriticalPopupColor = Color.red; // �n�C�p�[�N���e�B�J�����̃|�b�v�A�b�v�F

    [SerializeField] float fontSizeScale = 1f;  // �t�H���g�T�C�Y�̃X�P�[��

    // �_���[�W���󂯂����ɌĂ΂��֐�
    public void ShowDamage(int damage, Vector3 position, DamageType damageType)
    {
        // ���[���h���W���X�N���[�����W�ɕϊ�
        Vector3 screenPos = Camera.main.WorldToScreenPoint(position);
        // �|�b�v�A�b�v�F
        Color popupColor;

        switch (damageType)
        {
            case DamageType.HyperCritical:
                popupColor = hyperCriticalPopupColor;  // �N���e�B�J���|�b�v�A�b�v�F
                fontSizeScale = 2.5f;    // �t�H���g�T�C�Y�W�����g��
                break;

            case DamageType.SuperCritical:
                popupColor = superCriticalPopupColor;  // �N���e�B�J���|�b�v�A�b�v�F
                fontSizeScale = 2.0f;    // �t�H���g�T�C�Y�W�����g��
                break;

            case DamageType.Critical:
                popupColor = criticalPopupColor;  // �N���e�B�J���|�b�v�A�b�v�F
                fontSizeScale = 1.5f;    // �t�H���g�T�C�Y�W�����g��
                break;

            case DamageType.Normal:
            default:
                popupColor = normalPopupColor;    // �ʏ�|�b�v�A�b�v�F
                fontSizeScale = 1.0f;    // �t�H���g�T�C�Y�W����ݒ�

                break;
        }

        // �_���[�W�|�b�v�A�b�v�𐶐�
        GameObject popup = Instantiate(damagePopupPrefab, canvas.transform);

        // ���������|�b�v�A�b�v�̈ʒu���X�N���[�����W�ɐݒ�
        RectTransform rectTransform = popup.GetComponent<RectTransform>();
        rectTransform.position = screenPos;

        // �|�b�v�A�b�v�̃e�L�X�g��ݒ�
        DamagePopup popupScript = popup.GetComponent<DamagePopup>();
        popupScript.ShowDamage(damage,popupColor,fontSizeScale);
    }
}
