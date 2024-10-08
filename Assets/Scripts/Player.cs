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
        var speed = Vector3.zero;
        speed = new Vector3(moveInput.x, 0, moveInput.y);
        Move(speed);
    }

    // �w�肵�����x�ŁA���̃L�����N�^�[���ړ������܂��B
    public void Move(Vector3 normalizedSpeed)
    {
        // �����x�^��
        var velocity = rigidbody.velocity;
        velocity.x = normalizedSpeed.x * moveSpeed;
        velocity.z = normalizedSpeed.z * moveSpeed;
        rigidbody.velocity = velocity;
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
