using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--- ���C�̏Փˏ󋵂̑J��
enum E_RAY_HIT_STATE
{
	ENTER,	//���������u��
	EXIT,	//���ꂽ�u��
	STAY,	//�������Ă���
	NOT_HIT	//�������Ă��Ȃ�
}

public class CursorController_araki : MonoBehaviour
{
	static RectTransform m_rectTransform;   // �J�[�\���̍��W���
    [SerializeReference] GameObject m_lastPointJunk; // �O�t���[���Ŏw���Ă����K���N�^�̃f�[�^
    [SerializeReference] GameObject m_previewJunk;   // �v���r���[�p�K���N�^�̃f�[�^
	[SerializeReference]PreviewCamera_araki m_previreCamera;

	// Start is called before the first frame update
	void Start()
	{
		m_rectTransform = GetComponent<RectTransform>();
		m_rectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
		m_lastPointJunk = null;
	}

    // Update is called once per frame
    void Update()
    {
		//--- �v���r���[�p�K���N�^�𐶐�
		switch (CheckRayHitState())
		{
			case E_RAY_HIT_STATE.ENTER: // �w�����u��
				m_previreCamera.StartNoise();	// �m�C�Y���Đ�		
				break;
			case E_RAY_HIT_STATE.EXIT:  // ���ꂽ�u��
				m_previreCamera.EndPreview();
				Destroy(m_previewJunk);
				m_previewJunk = null;
				break;
			case E_RAY_HIT_STATE.STAY:
				if (!m_previreCamera.isEndNoise) break;

				//--- �m�C�Y���I�������K���N�^�𐶐�
				m_previewJunk = (GameObject)Instantiate((Object)m_lastPointJunk,
					new Vector3(1114.4f, 0.0f, 2.5f), Quaternion.identity);
				// ������Œ�
				m_previewJunk.GetComponent<Rigidbody>().constraints
					= RigidbodyConstraints.FreezeAll;
				m_previewJunk.AddComponent<PreviewJunk_araki>();
				break;
			default:    // ��L�ȊO�̏ꍇ�͏������Ȃ�
				break;
		}

		//--- �ړ�����
		Vector2 pos = m_rectTransform.anchoredPosition;
		pos.x += PadInput.GetAxis("Horizontal_R") * 7.5f;
		pos.y += PadInput.GetAxis("Vertical_R") * 7.5f;

		//--- ��ʊO�ɏo�Ă����̂�h��(����ʂ݈̂ړ��\)
		if (pos.x >  Screen.width / 2.0f) pos.x =  Screen.width / 2.0f;
		if (pos.x < -Screen.width / 2.0f) pos.x = -Screen.width / 2.0f;
		if (pos.y >  Screen.height / 2.0f) pos.y =  Screen.height / 2.0f;
		if (pos.y < -Screen.height / 2.0f) pos.y = -Screen.height / 2.0f;

		m_rectTransform.anchoredPosition = pos;	// �J�[�\���̈ʒu���m��
	}

	/// <summary>
	/// �J�[�\���ƃK���N�^�̏Փˏ󋵂��擾
	/// </summary>
	E_RAY_HIT_STATE CheckRayHitState()
	{
		//--- �J�������擾
		GameObject cam = GameObject.Find("JointCamera");
		if (cam == null) return E_RAY_HIT_STATE.NOT_HIT;    // �J������������Ώ������Ȃ�

		//--- ���C�œ����蔻������
		Ray ray = GetCameraToRay(cam);
		RaycastHit hit;
		// ����q���팸����ׂɔے�Ŕ���
		if (!Physics.Raycast(ray, out hit)) // �J�[�\�����w�������擾
		{
			GameObject temp = m_lastPointJunk;
			m_lastPointJunk = null; // �ߋ��̃f�[�^�����Z�b�g

			// �O�t���[���ŃK���N�^���w���Ă����ꍇ
			if (temp != null) return E_RAY_HIT_STATE.EXIT;

			return E_RAY_HIT_STATE.NOT_HIT;
		}

		GameObject hitJunk = hit.transform.gameObject;

		// �K���N�^�ɓ������Ă��Ȃ���Ώ������Ȃ�
		if (hitJunk.transform.parent.name != "Jank")
		{
			GameObject temp = m_lastPointJunk;
			m_lastPointJunk = null; // �ߋ��̃f�[�^�����Z�b�g

			// �O�t���[���ŃK���N�^���w���Ă����ꍇ
			if (temp != null) return E_RAY_HIT_STATE.EXIT;

			return E_RAY_HIT_STATE.NOT_HIT;
		}

		//--- �O�t���[���ŃK���N�^���w���Ă��Ȃ������ꍇ
		if (m_lastPointJunk == null)
		{
			m_lastPointJunk = hitJunk;  // �ߋ��̃f�[�^�Ƃ��đޔ�
			return E_RAY_HIT_STATE.ENTER;
		}

		// �w�������Ă���ꍇ
		if (m_lastPointJunk == hitJunk) return E_RAY_HIT_STATE.STAY;

		//--- �����w���Ȃ��Ȃ����ꍇ
		m_lastPointJunk = null; // �ߋ��̃f�[�^�����Z�b�g
		return E_RAY_HIT_STATE.EXIT;
	}

	/// <summary>
	/// �J�[�\������̃��C���擾
	/// </summary>
	public static Ray GetCameraToRay(GameObject cam)
	{
        // �J�������J�[�\��(���[���h���W�n)�̃��C���擾
		Vector2 pos = m_rectTransform.anchoredPosition;
        Camera camdata = cam.GetComponent<Camera>();
        Debug.Log(camdata.transform.name);
		return camdata.ScreenPointToRay(new Vector3(pos.x + Screen.width / 2.0f, pos.y + Screen.height / 2.0f, 0.0f));
	}

    public GameObject SelectJank
    {
        get { return m_lastPointJunk; }
    }
}