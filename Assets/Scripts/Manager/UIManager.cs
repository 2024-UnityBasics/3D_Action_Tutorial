using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    GameObject clearUI;

    private void Awake()
    {
        // Awake�̓I�u�W�F�N�g�����������Ƃ��ɌĂ΂�郁�\�b�h�ł�
        // �����ŃV���O���g���p�^�[�����������Ă��܂�

        // ���ł�GameManager�̃C���X�^���X�����݂��Ă���ꍇ�A���݂̃I�u�W�F�N�g�͔j�����܂�
        if (Instance != null && Instance != this)
        {
            // ���̃C���X�^���X�����݂���ꍇ�A�d�����č��Ȃ��悤�ɃI�u�W�F�N�g��j��
            Destroy(gameObject);
        }
        else
        {
            // GameManager�̃C���X�^���X���Ǘ����邽�߁A�ŏ��ɍ쐬���ꂽ�C���X�^���X��ێ����܂�
            Instance = this;
        }
    }

    public void ActiveClearUI()
    {
        //ckearUI�����݂���ΗL�����A������΃G���[�\��
        if (clearUI != null)
        {
            clearUI.SetActive(true);
        }
        else
        {
            Debug.Log("�N���AUI��������܂���");
        }
    }

    public void LoadGameScene()
    {
        SceneLoader.Instance.LoadSceneWithFade("SampleScene");
    }
}
