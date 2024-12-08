using Cinemachine;
using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera normalCam; // 通常のカメラ
    [SerializeField] private CinemachineVirtualCamera ultimateSkillCamera; // 必殺技用のカメラ
    [SerializeField] private Animator ultCamAnimator;           // 必殺技用カメラのAnimator


    void Start()
    {
        // 初期設定として、通常のカメラを優先的にアクティブにする
        normalCam.Priority = 10;
        ultimateSkillCamera.Priority = 1;
    }

    // 必殺技カメラをアクティブにする関数
    public void ActivateUltimateSkillCamera()
    {
        // 通常のカメラを非アクティブにし、必殺技カメラをアクティブにする
        normalCam.Priority = 1;
        ultimateSkillCamera.Priority = 10;
        // 必殺カメラのAnimatorにTriggerを送る
        ultCamAnimator.SetTrigger("UltCam");
    }

    // 必殺技カメラを非アクティブにする関数
    public void DeactivateUltimateSkillCamera()
    {
        // 通常のカメラを再びアクティブにし、必殺技カメラを非アクティブにする
        normalCam.Priority = 10;
        ultimateSkillCamera.Priority = 1;
    }

}
