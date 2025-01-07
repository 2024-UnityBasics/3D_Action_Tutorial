using UnityEngine;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

public class BossAction : MonoBehaviour
{
    [SerializeField] float punchCooldown = 5f;       // 攻撃後の待機時間
    [SerializeField] GameObject bossGroundAttack;
    [SerializeField] GameObject bossHandsL;
    [SerializeField] GameObject bossHandsR;
    Animator bossAnimator;

    private bool isPunching = false;       // 攻撃中かどうか
    private bool punchHandsLR = false;  // 攻撃左右
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

        // 攻撃中フラグを立てる
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

        yield return new WaitForSeconds(punchCooldown); // 待機時間を設ける

        isPunching = false;

    }


    public void PunchL()
    {
        // オブジェクトを生成
        Instantiate(bossGroundAttack, bossHandsL.transform.position, Quaternion.identity, transform);
    }
    public void PunchR()
    {
        // オブジェクトを生成
        Instantiate(bossGroundAttack, bossHandsR.transform.position, Quaternion.identity, transform);
    }
}
