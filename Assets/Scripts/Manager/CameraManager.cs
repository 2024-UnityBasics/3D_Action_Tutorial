using Cinemachine;
using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera normalCam; // �ʏ�̃J����
    [SerializeField] private CinemachineVirtualCamera ultimateSkillCamera; // �K�E�Z�p�̃J����
    [SerializeField] private Animator ultCamAnimator;           // �K�E�Z�p�J������Animator


    void Start()
    {
        // �����ݒ�Ƃ��āA�ʏ�̃J������D��I�ɃA�N�e�B�u�ɂ���
        normalCam.Priority = 10;
        ultimateSkillCamera.Priority = 1;
    }

    // �K�E�Z�J�������A�N�e�B�u�ɂ���֐�
    public void ActivateUltimateSkillCamera()
    {
        // �ʏ�̃J�������A�N�e�B�u�ɂ��A�K�E�Z�J�������A�N�e�B�u�ɂ���
        normalCam.Priority = 1;
        ultimateSkillCamera.Priority = 10;
        // �K�E�J������Animator��Trigger�𑗂�
        ultCamAnimator.SetTrigger("UltCam");
    }

    // �K�E�Z�J�������A�N�e�B�u�ɂ���֐�
    public void DeactivateUltimateSkillCamera()
    {
        // �ʏ�̃J�������ĂуA�N�e�B�u�ɂ��A�K�E�Z�J�������A�N�e�B�u�ɂ���
        normalCam.Priority = 10;
        ultimateSkillCamera.Priority = 1;
    }

}
