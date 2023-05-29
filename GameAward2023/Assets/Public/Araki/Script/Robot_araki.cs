using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_araki : MonoBehaviour
{
	Vector3 forwardDirect;
	float rayCastDistance = 1.0f;
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
		RaycastHit hit;
		if (Physics.Raycast(transform.position, -transform.up, out hit, rayCastDistance))
		{
			//--- �O�ɓ��񂾃p�l���͏������Ȃ�
			if (lastHit == hit.transform) return;
			lastHit = hit.transform;

			Panel_araki panel = hit.collider.GetComponent<Panel_araki>();
			if (panel == null) return;

			// �p�l���ɂ���Đi�s�������X�V
			Vector3 vPanel = panel.GetForwardDirect() * moveSpeed;
			float dot = Vector3.Dot(vPanel.normalized, forwardDirect.normalized);

			//--- �V�����x�N�g���̕�������
			float rotY = transform.rotation.y;
			transform.Rotate(0.0f, rotY - dot * 180.0f, 0.0f);

			forwardDirect = vPanel;	// �x�N�g�����X�V
		}
	}

	private void FixedUpdate()
	{
		// �ړ�����(���������^��)
		transform.position += forwardDirect;
	}
}