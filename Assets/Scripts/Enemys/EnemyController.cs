using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum EnemyState
    {
        Idle,       // �����~�܂��ĉ������Ȃ�
        Attack,     // �v���C���[���U������
        Damaged     // ��e���i�m�b�N�o�b�N��ԁj
    }

    [SerializeField] private EnemyState currentState = EnemyState.Idle; // ���݂̓G�̏��
    [SerializeField] private float attackRange = 1.5f; // �v���C���[���U���ł���͈�
    [SerializeField] private float knockbackDuration = 0.5f; // �m�b�N�o�b�N����������
    [SerializeField] private float knockbackForce = 5f; // �m�b�N�o�b�N�̋���
    [SerializeField] private float idleDuration =2f;    //�A�C�h����Ԃ̑�������

    [SerializeField] private float idleTimer = 0f; // �A�C�h����Ԃ̌o�ߎ���

    private float damagedTimer = 0f; // �m�b�N�o�b�N��Ԃ̌o�ߎ���
    private Vector3 knockbackDirection; // �m�b�N�o�b�N�̕������L��
    private Rigidbody rb; // Rigidbody�Q��
    private bool isKnockbackApplied = false; // �m�b�N�o�b�N���K�p���ꂽ���ǂ������Ǘ�����t���O

    void Start()
    {
        // Rigidbody���擾
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // �G�̌��݂̏�Ԃɉ����ď����𕪊򂵂܂�
        switch (currentState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
        }
    }

    private void Idle()
    {
        // �����~�܂��Ă��鎞�Ԃ��J�E���g
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
        {
            // State��Attack�ɐi�s���AidleTimer��������
            currentState = EnemyState.Attack;
            idleTimer = 0f;
        }
    }

    private void Attack()
    {
        // �U�����I�������Idle��Ԃɖ߂�
        Debug.Log("Enemy is attacking the player!");
        //currentState = EnemyState.Idle;
    }

    private void Damaged()
    {
        // ��e���̌o�ߎ��Ԃ����Z
        damagedTimer += Time.deltaTime;

        if (!isKnockbackApplied)
        {
            // �m�b�N�o�b�N����x�����K�p
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            isKnockbackApplied = true; // �m�b�N�o�b�N���K�p���ꂽ���Ƃ��L�^
        }

        // �m�b�N�o�b�N���I��������Idle��Ԃɖ߂�
        if (damagedTimer >= knockbackDuration)
        {
            currentState = EnemyState.Idle;
            isKnockbackApplied = false; // �m�b�N�o�b�N�K�p�t���O�����Z�b�g
        }
    }

    // �O������Ăяo�����_���[�W����
    public void TakeDamage(Vector3 attackSource)
    {
        // ��e����Damaged��ԂɑJ��
        currentState = EnemyState.Damaged;

        // �^�C�}�[�����Z�b�g
        damagedTimer = 0f;
        idleTimer = 0f;

        // �U�������猩���m�b�N�o�b�N�̕������v�Z
        knockbackDirection = (transform.position - attackSource).normalized + Vector3.up;

        Debug.Log("Enemy is taking damage!");
    }
}
