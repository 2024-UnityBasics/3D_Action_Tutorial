using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSearchCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 指定されたタグと一致するオブジェクトと接触した場合
        if (other.CompareTag("Player"))
        {
            Debug.Log("Awake");
            BossAction bossAction = GetComponentInParent<BossAction>();
            if (bossAction != null)
            {
                bossAction.Activate();
            }
        }
    }
}