using UnityEngine;
using System.Collections;

public class EnemyActionDash : MonoBehaviour
{
    public Transform target;              // Player�̈ʒu���w��
    public float dashForce = 10f;         // �ːi�̗�
    public float dashDuration = 1f;       // �ːi�̌��ʎ���
    public float dashCooldown = 2f;       // �ːi��̑ҋ@����

    private bool isDashing = false;       // �ːi�����ǂ���
    private Rigidbody rb;                 // Rigidbody�R���|�[�l���g

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

                // �ːi���I������܂ŗ͂�������
                while (Time.time < dashEndTime)
                {
                    // ���݂̌������擾���A�ːi�̕���������
                    Vector3 direction = (target.position - transform.position).normalized;
                    rb.AddForce(direction * dashForce, ForceMode.VelocityChange); // �͂�������
                    yield return null; // ���̃t���[����
                }

                isDashing = false; // �ːi���I�������̂Ńt���O�����Z�b�g
                yield return new WaitForSeconds(dashCooldown); // �ҋ@���Ԃ�݂���
            }
        }
    }

    void StartDash()
    {
        // Player�̕���������
        transform.LookAt(target.position);
        isDashing = true; // �ːi���t���O�𗧂Ă�
    }
}
