using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_araki : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		// �X�e�[�W�쐬���ɃI�u�W�F�N�g�������Ȃ��ƕ�����ɂ����ׁA
		// �Q�[���N�����Ɍ����Ȃ��悤�ɐݒ肷��
		GetComponent<MeshRenderer>().enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
	}

	public Vector3 GetForwardDirect()
	{
		return transform.forward;
	}
}