using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkBase : MonoBehaviour
{
    public Explosion explosionReference;

    void Start()
    {
        explosionReference = FindObjectOfType<Explosion>();
    }

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Danger"))
        {
            Explosion();
        }
    }

    /// <summary>
    /// �B�R���N���[�g�ɂ��������Ƃ�
    /// </summary>
    public virtual void HitNailConcrete()
    {
        Debug.Log("�B�R���N���[�g�ɂ��������I");
    }

    /// <summary>
    /// �~�d��ɂ��������Ƃ�
    /// </summary>
    public virtual void HitCapacitor()
    {
        Debug.Log("�~�d��ɂ��������I");
    }

    public virtual void Explosion()
    {
        explosionReference.Blast();
    }

}
