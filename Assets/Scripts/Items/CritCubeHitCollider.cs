using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritCubeHitCollider : MonoBehaviour
{
    // CritCubeへの参照
    private CritCube critCube;
    void Start()
    {
        // 親オブジェクトの StatusManager を取得
        critCube = GetComponentInParent<CritCube>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"CritCubeコライダー検知！");
            // Playerに接触したら、CritCubeの処理を呼び出す
            critCube.GetCritCube();
        }
    }
}
