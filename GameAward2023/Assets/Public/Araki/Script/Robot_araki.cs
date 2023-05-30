using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_araki : MonoBehaviour
{
	Vector3 forwardDirect;
	float rayCastDistance = 5.0f;
	[SerializeReference] float moveSpeed = 0.05f;
	Transform lastHit;

	private void Start()
	{
		// �����Ԃ����Ă��Ă������Ȃ��悤�ɂ���
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		forwardDirect = transform.forward * moveSpeed;  // �ŏ��͑O�ɐi�ނ悤�ɐݒ�
		lastHit = null;
	}

	private void Update()
	{
		//--- �p�l���𓥂񂾎��̏���
		Ray ray = new Ray(transform.position + transform.up.normalized * 3.0f, -transform.up);
		RaycastHit[] hits = Physics.RaycastAll(ray);
		for(int i = 0; i < hits.Length; i++)
		{
			Panel_araki panel = hits[i].transform.gameObject.GetComponent<Panel_araki>();
			if (panel == null) continue;

			//--- �O�ɓ��񂾃p�l���͏������Ȃ�
			if (lastHit == hits[i].transform) return;
			lastHit = hits[i].transform;

			// �p�l���ɂ���Đi�s�������X�V
			Vector3 vPanel = panel.GetForwardDirect() * moveSpeed;
			float dot = Vector3.Dot(vPanel.normalized, forwardDirect.normalized);

			//--- �V�����x�N�g���̕�������
			float rotY = transform.rotation.y;
			transform.Rotate(0.0f, rotY - dot * 180.0f, 0.0f);

			forwardDirect = vPanel; // �x�N�g�����X�V

			break;
		}
	}

	private void FixedUpdate()
	{
		// �ړ�����(���������^��)
		transform.position += forwardDirect;
	}
}