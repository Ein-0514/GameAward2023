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
        JunkBase junkBase = GetComponent<JunkBase>();

        if (junkBase != null)
        {
            junkBase.Explosion();
        }
    }
}