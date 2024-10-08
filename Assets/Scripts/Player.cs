using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//�Q�l�ɂ��₷�����邽�߁AInputSystemTutorial�Ƃł��邾���ϐ������𓯈�ɍ쐬���܂�

public class Player : MonoBehaviour
{
    //�ړ����x
    [SerializeField]
    private float moveSpeed = 5f;
    // �W�����v��
    [SerializeField]
    private Vector3 jumpForce = new(0, 5f, 0);

    // Move �A�N�V�����̓��͒l[-1.0, 1.0f]
    Vector2 moveInput = Vector2.zero;

    // �R���|�[�l���g�����O�ɎQ�Ƃ��Ă����ϐ�
    new Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    // �w�肵�����x�ŁA���̃L�����N�^�[���ړ������܂��B
    public void Move()
    {
        if (Camera.main != null)
        {
            // ���C���J�����̑O���ƉE�������擾�i�J�������[�J�����W�ł����Ƃ����z����x���j
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;

            // �J������y�������𖳎����āA�n�ʂɉ������ړ��ɂ���
            cameraForward.y = 0;
            cameraRight.y = 0;

            // ���K�����āA�J�����̑O���ƉE�����Ɋ�Â����ړ��x�N�g�����v�Z
            Vector3 moveDirection = (cameraForward * moveInput.y + cameraRight * moveInput.x).normalized;

            // moveInput��2D�x�N�g���ŁA�v���C���[�̈ړ����͂�\���܂��B
            // moveInput.x: ���E�̈ړ����́i-1.0f �͍��A1.0f �͉E�j
            // moveInput.y: �O��̈ړ����́i-1.0f �͌�ށA1.0f �͑O�i�j
            // ����: ����moveInput.y�́A�W���C�X�e�B�b�N��L�[�{�[�h���͂̑O��̓����ł���A
            //       3D��Ԃ�Y���i�㉺�����j�Ƃ͈قȂ�܂��B
            //       3D��Ԃ�Y���́A�����I�ȏ㉺�ړ��i�W�����v�◎���Ȃǁj�������܂��B

            // �ړ��x�N�g���ɑ��x���|���Ĉړ�
            rigidbody.velocity = moveDirection * moveSpeed + new Vector3(0, rigidbody.velocity.y, 0);
        }
    }

    // ���̃L�����N�^�[���W�����v�����܂��B
    public void Jump()
    {
        rigidbody.AddForce(jumpForce, ForceMode.Impulse);
        Debug.Log("jump");
    }

    // Move �A�N�V�����ɂ���ČĂяo����܂��B
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Jump �A�N�V�����ɂ���ČĂяo����܂��B
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Jump();
        }
    }
}
