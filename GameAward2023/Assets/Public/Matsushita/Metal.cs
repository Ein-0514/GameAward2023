using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : JunkBase
{
    /// <summary>
    /// �~�d��ɓ��������Ƃ�
    /// </summary>
    public override void HitCapacitor()
    {
        JunkBase JunkBase = GetComponent<JunkBase>();

        if (JunkBase != null)
        {
            JunkBase.Explosion();
        }
    }
}