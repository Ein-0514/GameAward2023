using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
	const float ROT = 90.0f;  // ��]�p�x

	[SerializeReference] float m_dampingRate;   // ������
	int m_timeToRotate;     // ��]�ɕK�v�ȃt���[����
	int m_rotFrameCnt;      // ��]���̃t���[���J�E���g
	int m_rotFrameCntX;      // ��]���̃t���[���J�E���g
	float m_rotY, m_lateY; // ��]�p�����[�^
	float m_rotX, m_lateX; // ��]�p�����[�^

	// Start is called before the first frame update
	void Start()
	{
		m_rotFrameCnt = -1;
		m_rotFrameCntX = -1;
		m_rotY = m_lateY = 0.0f;
		m_rotX = m_lateX = 0.0f;
		m_timeToRotate = (int)(Mathf.Log(0.005f) / Mathf.Log(1.0f - m_dampingRate));
	}

	// Update is called once per frame
	void Update()
	{
		RotateCoreY((int)Input.GetAxisRaw("Horizontal"));
		RotateCoreX((int)Input.GetAxisRaw("Vertical"));

		float lastY = m_lateY;
		float lastX = m_lateX;

		// �x������
		m_lateY = (m_rotY - m_lateY) * m_dampingRate + m_lateY;
		m_lateX = (m_rotX - m_lateX) * m_dampingRate + m_lateX;

		// ���W�v�Z
		//this.transform.eulerAngles = new Vector3(0.0f , m_lateY, 0.0f);
		//this.transform.eulerAngles = new Vector3(m_lateX, m_lateY, 0.0f);
		this.transform.Rotate(Vector3.up, m_lateY - lastY, Space.World);
		this.transform.Rotate(Vector3.right, m_lateX - lastX, Space.World);
		//this.transform.Rotate(new Vector3(m_lateX, m_lateY, 0.0f), Space.World);

		Debug.Log("lateX" + m_lateX);
		Debug.Log("lateY" + m_lateY);

		if (m_rotFrameCnt >= 0)
		{
			m_rotFrameCnt++;    // ��]���̃t���[���J�E���g

			if (m_rotFrameCnt > m_timeToRotate) m_rotFrameCnt = -1; // �J�E���g�̃��Z�b�g
		}

		if(m_rotFrameCntX >= 0)
		{
			m_rotFrameCntX++;

			if (m_rotFrameCntX > m_timeToRotate) m_rotFrameCntX = -1;
		}
	}

	/// <summary>
	/// �R�A��Y��]�J�n
	/// </summary>
	public void RotateCoreY(int direction)
	{
		if (direction == 0) return;
		if (m_rotFrameCnt > 0) return;
		if (m_rotFrameCntX > 0) return;

		m_rotY += ROT * direction;

		m_rotFrameCnt = 0;
	}

	/// <summary>
	/// �R�A��X��]�J�n
	/// </summary>
	public void RotateCoreX(int direction)
	{
		if (direction == 0) return;
		if (m_rotFrameCnt > 0) return;
		if (m_rotFrameCntX > 0) return;

		m_rotX -= ROT * direction;

		m_rotFrameCntX = 0;
	}

	/// <summary>
	/// ���i���A�^�b�`�o����ʂ��擾
	/// </summary>
	public List<Transform> GetAttachFace()
	{
		List<Transform> attachFace = new List<Transform>();

		//--- �q�I�u�W�F�N�g�����Ԃɔz��Ɋi�[
		foreach (Transform child in this.transform)
		{
			Ray ray = new Ray(child.position, Vector3.back);
			RaycastHit hit;
			if (!Physics.Raycast(ray, out hit, 10.0f))
				attachFace.Add(child);
		}

		//--- �\�[�g
		attachFace.Sort((a, b) => {
			if (a.position.y != b.position.y)
			{
				// Y ���W���قȂ�ꍇ�� Y ���W�Ŕ�r����
				return b.position.y.CompareTo(a.position.y);
			}
			else
			{
				// Y ���W�������ꍇ�� X ���W�Ŕ�r����
				return a.position.x.CompareTo(b.position.x);
			}
		});

		return attachFace;
	}

	/// <summary>
	/// ��]�I���t���O
	/// </summary>
	public bool IsRotEnd()
	{
		if (m_rotFrameCnt > m_timeToRotate)
		{
			m_rotFrameCnt = -1; // �J�E���g�̃��Z�b�g
			return true;
		}

		return false;
	}

	/// <summary>
	/// ���i���A�^�b�`����
	/// </summary>
	public void AttachParts(Transform face, GameObject prefabParts)
	{
		Vector3 pos = face.position + (Vector3.forward.normalized * -1.0f);
		GameObject parts = Instantiate(prefabParts, pos, Quaternion.identity);

		parts.transform.SetParent(this.transform);
	}
}