using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] GameObject GSManager;
    [SerializeReference] float m_bounceRate;
    
    private void OnCollisionStay(Collision collision)	// �u�ԓI�ɑ傫�ȗ͂�������ƃ^�C�������Ă��܂���
    {
        if (GSManager.transform.GetComponent<GameStatusManager>().GameStatus == GameStatusManager.eGameStatus.E_GAME_STATUS_PLAY)
        {
            if (!this.transform.parent.name.Contains("Core")) return;

            //--- �ǂƏՓ˂������̏���
            if (collision.transform.tag == "Wall")
            {
                //--- ��������x�N�g�����v�Z
                Vector3 vToSelf = this.transform.position - collision.contacts[0].point;
                vToSelf.y = 0.0f;   // Y�������ւ̈ړ��𖳎�
                vToSelf = vToSelf.normalized * m_bounceRate;

                // ��������x�N�g����K�p
                this.GetComponent<Rigidbody>().AddForce(vToSelf, ForceMode.Impulse);
            }
        }
    }
}
