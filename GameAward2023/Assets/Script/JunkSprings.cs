using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkSprings : JunkBase
{
	[SerializeReference] float m_bounceRate;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	/// <summary>
	/// ���˕Ԃ�����
	/// </summary>
	private void OnCollisionStay(Collision collision)	// �u�ԓI�ɑ傫�ȗ͂�������ƃ^�C�������Ă��܂���
	{
		//--- �ǂƏՓ˂������̏���
		if (collision.transform.tag == "Wall")
		{
			//--- ��������x�N�g�����v�Z
			Vector3 vToSelf = this.transform.position - collision.contacts[0].point;
			vToSelf.y = 0.0f;	// Y�������ւ̈ړ��𖳎�
			vToSelf = vToSelf.normalized * m_bounceRate;

			// ��������x�N�g����K�p
			this.GetComponent<Rigidbody>().AddForce(vToSelf,ForceMode.Impulse);
		}
	}
}