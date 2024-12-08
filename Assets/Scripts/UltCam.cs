using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AnimatorEventからの呼び出しを受け取るためだけのスクリプト
public class UltCam : MonoBehaviour
{

    public void UltCamEnd()
    {
        // CameraManagerを取得(必ず親にいるはずなのでInPearent)
        CameraManager cameraManager = GetComponentInParent<CameraManager>();
        cameraManager.DeactivateUltimateSkillCamera();
    }
}
