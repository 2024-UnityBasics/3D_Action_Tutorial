using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AnimatorEvent����̌Ăяo�����󂯎�邽�߂����̃X�N���v�g
public class UltCam : MonoBehaviour
{

    public void UltCamEnd()
    {
        // CameraManager���擾(�K���e�ɂ���͂��Ȃ̂�InPearent)
        CameraManager cameraManager = GetComponentInParent<CameraManager>();
        cameraManager.DeactivateUltimateSkillCamera();
    }
}
