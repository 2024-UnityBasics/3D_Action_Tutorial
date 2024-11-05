using UnityEngine;
using System.Collections;

public class EnemyActionDash : MonoBehaviour
{
    [SerializeField] Transform target;              // Player�̈ʒu���w��
    [SerializeField] float dashSpeed = 10f;         // ���̓ːi���x
    [SerializeField] float dashDuration = 1f;       // �ːi�̌��ʎ���
    [SerializeField] float dashCooldown = 2f;       // �ːi��̑ҋ@����

    private bool isDashing = false;       // �ːi�����ǂ���
    private Rigidbody rb;                 // Rigidbody�R���|�[�l���g
    private Vector3 dashDirection;        // �ːi�̕���

    void Start()
    {
        // Rigidbody�R���|�[�l���g�̎擾
        rb = GetComponent<Rigidbody>();

        // �ǔ��Ώۂ̎擾�iPlayer��z��j
        target = GameObject.Find("Player").GetComponent<Transform>();
        StartCoroutine(EnemyDashRoutine());
    }

    private IEnumerator EnemyDashRoutine()
    {
        while (true)
        {
            if (!isDashing)
            {
                StartDash(); // �ːi���J�n

                // �ːi���̏���
                float dashEndTime = Time.time + dashDuration; // �ːi�I���̎��Ԃ��v�Z

                // �ːi���I������܂ŁA���x���Œ肵�ēːi����
                while (Time.time < dashEndTime)
                {
                    rb.velocity = dashDirection * dashSpeed; // �ːi�����Ɉ��̑��x�Ői��
                    yield return null; // ���̃t���[����
                }

                // �ːi�I����A���x��0�ɂ���
                rb.velocity = Vector3.zero;
                isDashing = false; // �ːi���I�������̂Ńt���O�����Z�b�g
                yield return new WaitForSeconds(dashCooldown); // �ҋ@���Ԃ�݂���
            }
        }
    }

    void StartDash()
    {
        // �v���C���[�̕����������A�ːi�������v�Z
        transform.LookAt(target.position);
        dashDirection = (target.position - transform.position).normalized; // �ːi�̕�������x�����擾
        isDashing = true; // �ːi���t���O�𗧂Ă�
    }
}
