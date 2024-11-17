using TMPro;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    public GameObject damagePopupPrefab; // �_���[�W�|�b�v�A�b�v��Prefab
    public Canvas canvas;                // ��ʂɕ\�������Canvas�iScreen Space�j

    // �_���[�W���󂯂����ɌĂ΂��֐�
    public void ShowDamage(int damage, Vector3 position)
    {
        // ���[���h���W���X�N���[�����W�ɕϊ�
        Vector3 screenPos = Camera.main.WorldToScreenPoint(position);

        // �_���[�W�|�b�v�A�b�v�𐶐�
        GameObject popup = Instantiate(damagePopupPrefab, canvas.transform);

        // ���������|�b�v�A�b�v�̈ʒu���X�N���[�����W�ɐݒ�
        RectTransform rectTransform = popup.GetComponent<RectTransform>();
        rectTransform.position = screenPos;

        // �|�b�v�A�b�v�̃e�L�X�g��ݒ�
        DamagePopup popupScript = popup.GetComponent<DamagePopup>();
        popupScript.ShowDamage(damage);
    }
}
