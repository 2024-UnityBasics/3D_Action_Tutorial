using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

    //�A�j���[�V�����p�̃A�j���[�^�[��錾�iStart�Ŏ擾�j
    Animator playerAnimator;
    //���蔻��p��bool
    bool isRun = false;

    //���[�U�[�����p
    [SerializeField]
    GameObject laserPrefab;
    //���[�U�[���˒n�_�w��p
    [SerializeField]
    Transform laserSpawner;

    //�U������i�ߐڕ���j�p�̃R���C�_�[
    [SerializeField]
    Collider attackCollider;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
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
            // ���C���J�����̑O���ƉE�������擾�i�J�������[�J�����W�ł����Ƃ����z��������x�������j
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

            // �L�����N�^�[���ړ���������Ɍ������邽�߂̏���
            if (moveDirection != Vector3.zero)  // ��������ړ����������Ă���ꍇ�̂݉�]������
            {
                // Quaternion.LookRotation�́A�w�肳�ꂽ�����imoveDirection�j���������߂̉�]���v�Z���܂��B
                // moveDirection�̓J�����̌����Ɋ�Â����ړ������ł��B
                // �܂�A�L�����N�^�[���i�ޕ����ɍ��킹�ăL�����N�^�[�̌�����ς��邽�߂̉�]�����߂Ă��܂��B
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

                // transform.rotation�̓L�����N�^�[�̌��݂̉�]��\���܂��B
                // Quaternion.Slerp�́A���݂̉�]�itransform.rotation�j����ڕW�̉�]�itargetRotation�j�܂ł����炩�ɕ�Ԃ��܂��B
                // Time.deltaTime * 10f�́A��Ԃ̑��x�����߂邽�߂̂��̂ł��B�l���傫���قǑ�����]���A�������قǂ�������]���܂��B
                // ���̕�ԏ����ɂ���āA�L�����N�^�[�͋}�Ɍ�����ς���̂ł͂Ȃ��A���R�ȑ��x�ŉ�]���܂��B
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            
                //�ړ�������Ȃ̂ŁA�A�j���[�V�����p�̃t���O��true�ɂ���
                isRun = true;
            
            }
            else
            {
                //�ړ�������Ȃ���΃t���O�����낷
                isRun = false;
            }

            //Animator��isRun�̏�Ԃ𑗂�
            playerAnimator.SetBool("Run", isRun);

        }



    }

    // ���̃L�����N�^�[���W�����v�����܂��B
    public void Jump()
    {
        rigidbody.AddForce(jumpForce, ForceMode.Impulse);
        Debug.Log("jump");

        //Animator��Jump�̃g���K�[�𑗂�
        playerAnimator.SetTrigger("Jump");
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
    // ���[�U�[�𔭎˂���֐�
    public void Fire()
    {
        // laserPrefab�i���[�U�[�̃v���n�u�j��laserSpawner�̈ʒu�ƌ����Ő�������
        Instantiate(laserPrefab, laserSpawner.transform.position, laserSpawner.transform.rotation);

        // �v���C���[�̃A�j���[�^�[�ɁuSingleLaserAction�v�g���K�[���Z�b�g���A���[�U�[���˂̃A�j���[�V�������Đ�
        playerAnimator.SetTrigger("SingleLaserAction");
    }

    // InputSystem�����Fire���͂ɉ���������
    public void OnFire(InputAction.CallbackContext context)
    {
        // ���͂��n�܂����u�ԁi�{�^�����������Ƃ��j��Fire()���Ăяo��
        if (context.started)
        {
            Fire();
        }
    }

    // �ߐڍU�����s���֐�
    public void Attack()
    {
        // �v���C���[�̃A�j���[�^�[�ɁuCrossRangeAttack�v�g���K�[���Z�b�g���A�ߐڍU���̃A�j���[�V�������Đ�
        playerAnimator.SetTrigger("CrossRangeAttack");
    }

    // InputSystem�����Attack���͂ɉ���������
    public void OnAttack(InputAction.CallbackContext context)
    {
        // ���͂��n�܂����u�ԁi�{�^�����������Ƃ��j��Attack()���Ăяo��
        if (context.started)
        {
            Attack();
        }
    }

    // �ߐڍU���p�̃R���C�_�[��L���ɂ���֐�
    void AttackColliderOn()
    {
        attackCollider.enabled = true;
    }

    // �ߐڍU���p�̃R���C�_�[�𖳌��ɂ���֐�
    void AttackColliderOff()
    {
        attackCollider.enabled = false;
    }
}
