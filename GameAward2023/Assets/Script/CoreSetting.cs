using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreSetting : MonoBehaviour
{
	const float ROTATION	 = 90.0f;   // ��]�p�x
	const float DAMPING_RATE = 0.5f;   // ��]������

	List<Transform> m_AttachFaces;	// �g�ݗ��Ă����
	int m_selectFaceNum;			// �I��ʂ̔ԍ�
	int m_timeToRotate;				// ��]����
	float m_rotateY, m_rotateX;     // �p�x
	float m_lateY, m_lateX;			// �x���p�x
	int m_rotateFrameCnt;           // ��]�t���[���̃J�E���g]
	bool m_isDepath;		// �ʏ����擾�������t���O

    [SerializeReference] AudioClip m_RotSound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
		m_AttachFaces = GetAttachFace();	// �g�ݗ��Ă���ʂ��擾
		m_selectFaceNum = 0;
		m_rotateY = m_rotateX = 0.0f;
		m_lateY = m_lateX = 0.0f;

		// ��]���Ԃ��v�Z
		m_timeToRotate = (int)(Mathf.Log(0.00001f) / Mathf.Log(1.0f - DAMPING_RATE));

		// �I�𒆂̖ʂ�傫����������
		m_AttachFaces[m_selectFaceNum].localScale = new Vector3(1.25f, 1.25f, 1.25f);

		m_isDepath = false;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
		//--- ��]��
		if(m_rotateFrameCnt > 0)
		{
			RotateCore();	// ��]����			
			return;
		}

		if(m_isDepath)
		{
			m_AttachFaces = GetAttachFace();    // ���̑g�ݗ��Ă���ʂ��擾
		
			// �I�𒆂̖ʂ�傫����������
			m_AttachFaces[m_selectFaceNum].localScale = new Vector3(1.25f, 1.25f, 1.25f);
			Debug.Log(m_selectFaceNum);
			Debug.Log(m_AttachFaces.Count);
			for (int i = 0; i < m_AttachFaces.Count; i++)
			{
				Vector3 pos = m_AttachFaces[i].position;
				Debug.Log("X:" + pos.x + " Y:" + pos.y + " Z:" + pos.z);
			}
		
			m_isDepath = false;
		}

		SelectFace();   // �ʂ�I��
	}

	/// <summary>
	/// �g�ݗ��Ă���ʂ�Ԃ�
	/// </summary>
	List<Transform> GetAttachFace()
	{
		// �ʂ̊i�[���p��
		List<Transform> attachFaces = new List<Transform>();

		//--- �g�ݗ��Ă���ʂ����ԂɊi�[
		foreach (Transform child in this.transform)
		{
			// ��O�ɐL�т郌�C��p��
			Ray ray = new Ray(child.position, Vector3.back);
			RaycastHit hit;

            // �g�ݗ��Ă��Ȃ��ʂ̓X���[
            // ��O�ɕ�����������X�L�b�v
            if (Physics.Raycast(ray, out hit, 10.0f)) continue;

            attachFaces.Add(child);	// �ʂ��i�[
		}

		//--- �\�[�g
		attachFaces.Sort((a, b) => {
			//if (a.position.y != b.position.y)
			if(Mathf.Abs(a.position.y - b.position.y) > 0.75f)
			{
				// Y���W���قȂ�ꍇ��Y���W�Ŕ�r����
				return b.position.y.CompareTo(a.position.y);
			}
			else
			{
				// Y���W�������ꍇ��X���W�Ŕ�r����
				return a.position.x.CompareTo(b.position.x);
			}
		});

		return attachFaces;
	}

	/// <summary>
	/// �ʂ̑I��
	/// </summary>
	void SelectFace()
	{
		int lastSelectFaceNum = m_selectFaceNum;	// �ߋ��̑I��ʔԍ����i�[

		float axisX = AxisInput.GetAxisRawRepeat("Horizontal_PadX");         // ���Ɉړ�

		//--- ���ɓ��͂����������̂ݏ���
		if (axisX != 0)
		{
			Vector3 facePos = m_AttachFaces[m_selectFaceNum].position;  // ���݂̖ʂ̍��W���擾
			facePos.x += axisX; // �ړ���̖ʂ̍��W�ɏC��

			bool isRotate = true;

			//--- ��v����ʂ�T��
			for (int i = 0; i < m_AttachFaces.Count; i++)
			{
				//--- ���݂̖ʂƎ��̖ʂ�XY���W��Vector2�Ɋi�[
				Vector2 currentFacePos = new Vector2(m_AttachFaces[i].position.x, m_AttachFaces[i].position.y);
				Vector2 newxtFacePos = new Vector2(facePos.x, facePos.y);

				// XY���ʂł̋��������ꂷ���Ă�����X���[
				if (Vector2.Distance(currentFacePos, newxtFacePos) > 0.05f) continue;

				m_selectFaceNum = i;
				isRotate = false;
				break;
			}

			if (isRotate)
			{
				m_AttachFaces[lastSelectFaceNum].localScale = new Vector3(1.0f, 1.0f, 1.0f);    // �ߋ��̖ʂ�������
				StartRotateY((int)axisX);
				return;
			}
		}

		float axisY = (float)AxisInput.GetAxisRawRepeat("Vertical_PadX");    // �c�Ɉړ�

		//--- �c�ɓ��͂����������̂ݏ���
		if (axisY != 0)
		{
			Vector3 facePos = m_AttachFaces[m_selectFaceNum].position;	// ���݂̖ʂ̍��W���擾
			facePos.y += axisY; // �ړ���̖ʂ̍��W�ɏC��

			bool isRotate = true;

			//--- ��v����ʂ�T��
			for (int i = 0; i < m_AttachFaces.Count; i++)
			{
				//--- ���݂̖ʂƎ��̖ʂ�XY���W��Vector2�Ɋi�[
				Vector2 currentFacePos = new Vector2(m_AttachFaces[i].position.x, m_AttachFaces[i].position.y);
				Vector2 newxtFacePos = new Vector2(facePos.x, facePos.y);

				// XY���ʂł̋��������ꂷ���Ă�����X���[
				if (Vector2.Distance(currentFacePos, newxtFacePos) > 0.05f) continue;

				m_selectFaceNum = i;
				isRotate = false;
				break;
			}

			if (isRotate)
			{
				m_AttachFaces[lastSelectFaceNum].localScale = new Vector3(1.0f, 1.0f, 1.0f);    // �ߋ��̖ʂ�������
				StartRotateX((int)axisY);
				return;
			}
		}

		//--- �I��ʂ��ύX���ꂽ�ꍇ
		if (m_selectFaceNum != lastSelectFaceNum)
		{
			m_AttachFaces[lastSelectFaceNum].localScale = new Vector3(1.0f, 1.0f, 1.0f);	// �ߋ��̖ʂ�������
			m_AttachFaces[m_selectFaceNum].localScale = new Vector3(1.25f, 1.25f, 1.25f);	// ���݂̖ʂ�����
		}

	}

	/// <summary>
	/// �R�A�̉�]����
	/// </summary>
	void RotateCore()
	{
		float lastY = m_lateY;
		float lastX = m_lateX;

		//--- �x������
		m_lateY = (m_rotateY - m_lateY) * DAMPING_RATE + m_lateY;
		m_lateX = (m_rotateX - m_lateX) * DAMPING_RATE + m_lateX;

		//--- ���W�v�Z
		this.transform.Rotate(Vector3.up, m_lateY - lastY, Space.World);
		this.transform.Rotate(Vector3.right, m_lateX - lastX, Space.World);

		m_rotateFrameCnt++; // ��]�t���[���J�E���g

		//--- ��]�I�����̏���
		if (m_rotateFrameCnt > m_timeToRotate)
		{
			m_AttachFaces = GetAttachFace();    // ���̑g�ݗ��Ă���ʂ��擾
			m_rotateFrameCnt = 0;   // ��]�t���[�������Z�b�g
			m_selectFaceNum = 0;    // �I��ʂ̔ԍ������Z�b�g
			m_AttachFaces[m_selectFaceNum].localScale = new Vector3(1.25f, 1.25f, 1.25f);   // ���݂̖ʂ�����
		}
	}

	/// <summary>
	/// �R�A��Y����]�J�n
	/// </summary>
	void StartRotateY(int direction)
	{
		if (direction == 0) return;

		m_rotateY += ROTATION * direction;	// �p�x��ݒ�
		m_rotateFrameCnt = 1;	// �ŏ��̃J�E���g
        audioSource.PlayOneShot(m_RotSound);    //SE�̍Đ�
    }

	/// <summary>
	/// �R�A��X����]�J�n
	/// </summary>
	void StartRotateX(int direction)
	{
		if (direction == 0) return;

		m_rotateX -= ROTATION * direction;  // �p�x��ݒ�
		m_rotateFrameCnt = 1;   // �ŏ��̃J�E���g
        audioSource.PlayOneShot(m_RotSound);    //SE�̍Đ�
    }

	/// <summary>
	/// FixedJoint��ǉ�
	/// </summary>
	void AddFixedJoint(GameObject junk, Vector3 junkPos, Vector3 direction)
	{
		// �w������ɐL�т郌�C��p��
		Ray ray = new Ray(junkPos, direction);
		RaycastHit hit;

		// �w������ɃK���N�^�����݂��Ȃ��Ȃ�X���[
		if (!Physics.Raycast(ray, out hit, 1.0f)) return;

		//--- �K���N�^�̏ꍇ�A�g�ݗ��Ă���ʂ��𔻒肷��
        //iwata:�����R�A�ƃ~�j�R�A��Player�^�O����Player�𔻒肷��
		if (hit.transform.tag == "Player")
		{
			JunkSetting junkSetting = hit.transform.GetComponent<JunkSetting>();
			if (!junkSetting.CanAttach(-direction))	return;
		}

		//--- FixedJoint��ݒ�
		FixedJoint fixedJoint = junk.AddComponent<FixedJoint>();
		fixedJoint.connectedBody = hit.rigidbody;
		fixedJoint.breakForce = 2000.0f;
		fixedJoint.breakTorque = 2000.0f;

		Rigidbody junkRigidbody = junk.GetComponent<Rigidbody>();
		junkRigidbody.constraints = RigidbodyConstraints.FreezeAll; // ���W�E�p�x���Œ肷��
		junkRigidbody.isKinematic = false;  // �������Z��L����
	}

	/// <summary>
	/// FixedJoint���폜
	/// </summary>
	void RemoveFixedJoint(GameObject junk, Vector3 direction)
	{
		// �w������ɐL�т郌�C��p��
		Ray ray = new Ray(junk.transform.position, direction);
		RaycastHit hit;

		// �w������ɃK���N�^�����݂��Ȃ��Ȃ�X���[
		if (!Physics.Raycast(ray, out hit, 1.0f)) return;

		//--- �K���N�^�̏ꍇ�A�g�ݗ��Ă���ʂ��𔻒肷��
		if (hit.transform.tag == "Junk")
		{
			JunkSetting junkSetting = hit.transform.GetComponent<JunkSetting>();
			if (!junkSetting.CanAttach(-direction)) return;
		}

		//--- ���͂̎��g���q���ł���FixedJoint���폜
		Rigidbody rigidbody = junk.GetComponent<Rigidbody>();
		FixedJoint[] fixedJoints = hit.transform.GetComponents<FixedJoint>();
		for(int i = 0; i < fixedJoints.Length; i++)
		{
			if (rigidbody != fixedJoints[i].connectedBody) continue;

			Destroy(fixedJoints[i]);
		}
	}

	/// <summary>
	/// ��]���t���O���擾
	/// </summary>
	public bool IsRotate()
	{
		return (m_rotateFrameCnt > 0);
	}

	/// <summary>
	/// �K���N�^���A�^�b�`����
	/// �A�^�b�`�̐����t���O��Ԃ�
	/// </summary>
	public bool AttachJunk(GameObject junk)
	{
        if (junk.tag == "Junk" && m_AttachFaces[m_selectFaceNum].tag == "Junk") return false;

		//--- �g�ݗ��Ă��Ȃ��ʂł���΃L�����Z��
		if(m_AttachFaces[m_selectFaceNum].transform.tag == "Junk")
		{
			//--- �g�ݗ��Ă��Ȃ��ꍇ
			if (!m_AttachFaces[m_selectFaceNum].GetComponent<JunkSetting>().CanAttach(Vector3.back))	return false;
		}

		m_AttachFaces[m_selectFaceNum].localScale = new Vector3(1.0f, 1.0f, 1.0f);

		//--- �A�^�b�`������W���w��
		Vector3 junkPos = m_AttachFaces[m_selectFaceNum].position;
		//Vector3 junkPos = face.position;
		junkPos -= Vector3.forward.normalized * 1.0f;
		junk.transform.position = junkPos;

		JunkSetting junkSetting = junk.GetComponent<JunkSetting>();
		List<Vector3> searchDirects = junkSetting.GetAttachVector();

		//--- �e������FixedJoint��ݒ�
		for (int i = 0; i < searchDirects.Count; i++)
			AddFixedJoint(junk, junkPos, searchDirects[i]);

		junk.transform.SetParent(this.transform);   // �R�A�̎q�ɂ���

		m_AttachFaces = GetAttachFace();    // ���̑g�ݗ��Ă���ʂ��擾
		
		// �I�𒆂̖ʂ�傫����������
		m_AttachFaces[m_selectFaceNum].localScale = new Vector3(1.25f, 1.25f, 1.25f);

		return true;
	}

	public void DetachJunk()
	{
		// �K���N�^�łȂ��ꍇ�X���[
		if (m_AttachFaces[m_selectFaceNum].transform.tag != "Junk") return;

		m_AttachFaces[m_selectFaceNum].localScale = new Vector3(1.0f, 1.0f, 1.0f);

		// �T������������擾
		List<Vector3> searchDirects = m_AttachFaces[m_selectFaceNum].transform.GetComponent<JunkSetting>().GetAttachVector();

		//--- �e������FixedJoint���폜
		for (int i = 0; i < searchDirects.Count; i++)
			RemoveFixedJoint(
				m_AttachFaces[m_selectFaceNum].transform.gameObject,
				searchDirects[i]);

		//--- ���g��FixedJoint��S�č폜
		FixedJoint[] joints = m_AttachFaces[m_selectFaceNum].transform.GetComponents<FixedJoint>();
		for (int i = 0; i < joints.Length; i++)
			Destroy(joints[i]);

		//--- �S�~�R�ɖ߂�
		Transform junkTransform = m_AttachFaces[m_selectFaceNum].transform;
		junkTransform.GetComponent<JunkController>().ResetTransform();

		m_isDepath = true;	// �ʏ����擾�������t���O�𗧂Ă�

		// HACK:�����s����Ɩʂ̏�񂪏�肭�擾����Ȃ���
		/**
		 *	if (m_isDepath)
		 *	{
		 *		m_AttachFaces = GetAttachFace();    // ���̑g�ݗ��Ă���ʂ��擾
		 *
		 *		// �I�𒆂̖ʂ�傫����������
		 *		m_AttachFaces[m_selectFaceNum].localScale = new Vector3(1.25f, 1.25f, 1.25f);
		 *		Debug.Log(m_selectFaceNum);
		 *		Debug.Log(m_AttachFaces.Count);
		 *		for (int i = 0; i < m_AttachFaces.Count; i++)
		 *		{
		 *			Vector3 pos = m_AttachFaces[i].position;
		 *			Debug.Log("X:" + pos.x + " Y:" + pos.y + " Z:" + pos.z);
		 *		}
		 *
		 *		m_isDepath = false;
		 *	}
		 */
	}

	/// <summary>
	/// �R�A���g�̏���
	/// </summary>
	public void CoreReady()
	{
		m_AttachFaces[m_selectFaceNum].localScale = new Vector3(1.0f, 1.0f, 1.0f);

		foreach (Transform child in this.transform)
		{
			// �Œ������
			child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

			// �X�^�[�g�ʒu�ֈړ�
			this.transform.position = new Vector3(-10.0f, 2.5f, -10.0f);
		}
	}
}