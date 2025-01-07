using UnityEngine;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

public class BossAction : MonoBehaviour
{
    [SerializeField] float punchCooldown = 5f;       // �U����̑ҋ@����
    [SerializeField] GameObject bossGroundAttack;
    [SerializeField] GameObject bossHandsL;
    [SerializeField] GameObject bossHandsR;
    Animator bossAnimator;

    private bool isPunching = false;       // �U�������ǂ���
    private bool punchHandsLR = false;  // �U�����E
    private bool isActive = false;

    private void Start()
    {
        bossAnimator = GetComponent<Animator>();
    }
    public void Activate()
    {
        Debug.Log("Wake!!!");
        bossAnimator.SetTrigger("Wake");
        StartCoroutine(WaitWake());

    }
    private void Update()
    {
        if(isActive)
        {
            Debug.Log("punchi!!!");
            Startpunch();
        }
    }

    public void Startpunch()
    {
        if (isPunching)
        {
            return;
        }

        StartCoroutine(EnemypunchRoutine());
    }

    private IEnumerator WaitWake()
    {
        yield return new WaitForSeconds(7f);
        isActive = true;
    }

    private IEnumerator EnemypunchRoutine()
    {

        // �U�����t���O�𗧂Ă�
        isPunching = true;

        if (punchHandsLR)
        {
            bossAnimator.SetTrigger("L_Fist");
        }
        else
        {
            bossAnimator.SetTrigger("R_Fist");
        }

        punchHandsLR = !punchHandsLR;

        yield return new WaitForSeconds(punchCooldown); // �ҋ@���Ԃ�݂���

        isPunching = false;

    }


    public void PunchL()
    {
        // �I�u�W�F�N�g�𐶐�
        Instantiate(bossGroundAttack, bossHandsL.transform.position, Quaternion.identity, transform);
    }
    public void PunchR()
    {
        // �I�u�W�F�N�g�𐶐�
        Instantiate(bossGroundAttack, bossHandsR.transform.position, Quaternion.identity, transform);
    }
}
