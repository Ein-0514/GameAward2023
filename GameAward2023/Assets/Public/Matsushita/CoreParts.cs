using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreParts : Metal
{
    /// <summary>
    /// �B�R���N���[�g�ɂ��������Ƃ�
    /// </summary>
    public override void HitNailConcrete()
    {
        Debug.Log("�Q�[���I�[�o�[");
    }

    /// <summary>
    /// �S�[���ɐG�ꂽ�Ƃ�
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            Debug.Log("�Q�[���N���A");
        }
    }

}
