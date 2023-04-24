using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkBase : MonoBehaviour
{
    void Start()
    {
    }


    /// <summary>
    /// �B�R���N���[�g�ɂ��������Ƃ�
    /// </summary>
    public virtual void HitNailConcrete()
    {
    }

    /// <summary>
    /// �~�d��ɂ��������Ƃ�
    /// </summary>
    public virtual void HitCapacitor()
    {
    }

    /// <summary>
    /// �R���Ă���h�����ʂɂ��������Ƃ�
    /// </summary>
    public virtual void HitFireDrum()
    {

    }

    public virtual void Explosion()
    {
        Explosion explosion = transform.root.gameObject.GetComponent<Explosion>();
        if (explosion == null) return;

        explosion.Blast();
    }

}
