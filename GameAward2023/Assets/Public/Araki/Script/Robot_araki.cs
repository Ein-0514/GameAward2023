using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_araki : MonoBehaviour
{
    Vector3 forwardDirect;
    float rayCastDistance = 1.0f;
	[SerializeReference] float moveSpeed = 0.05f;

    private void Start()
    {
		// �����Ԃ����Ă��Ă������Ȃ��悤�ɂ���
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        forwardDirect = transform.forward * moveSpeed;	// �ŏ��͑O�ɐi�ނ悤�ɐݒ�
    }

    private void Update()
    {
		//--- �p�l���𓥂񂾎��̏���
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, rayCastDistance))
        {
            Panel_araki panel = hit.collider.GetComponent<Panel_araki>();
			if (panel == null) return;

            // �p�l���ɂ���Đi�s�������X�V
            forwardDirect = panel.GetForwardDirect() * moveSpeed;
        }
    }

	private void FixedUpdate()
	{
		// �ړ�����(���������^��)
		transform.position += forwardDirect;
	}
}