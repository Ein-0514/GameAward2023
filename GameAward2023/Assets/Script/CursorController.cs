using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
	static RectTransform m_rectTransform;

	// Start is called before the first frame update
	void Start()
	{
		m_rectTransform = GetComponent<RectTransform>();
		m_rectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
	}

    // Update is called once per frame
    void Update()
    {
		//--- �ړ�����
		Vector2 pos = m_rectTransform.anchoredPosition;
		pos.x += Input.GetAxisRaw("Horizontal_R") * 7.5f;
		pos.y += Input.GetAxisRaw("Vertical_R") * 7.5f;
        if (Input.GetKey(KeyCode.J)) pos.x -= 7.5f;
        if (Input.GetKey(KeyCode.L)) pos.x += 7.5f;
        if (Input.GetKey(KeyCode.I)) pos.y += 7.5f;
        if (Input.GetKey(KeyCode.K)) pos.y -= 7.5f;

		//--- ��ʊO�ɏo�Ă����̂�h��(����ʂ݈̂ړ��\)
		if (pos.x >  Screen.width / 2.0f) pos.x =  Screen.width / 2.0f;
		if (pos.x < 0.0f)				  pos.x = 0.0f;
		if (pos.y >  Screen.height / 2.0f) pos.y =  Screen.height / 2.0f;
		if (pos.y < -Screen.height / 2.0f) pos.y = -Screen.height / 2.0f;

		m_rectTransform.anchoredPosition = pos;	// �J�[�\���̈ʒu���m��
	}

	public static Ray GetCameraToRay()
	{
		// �J�������J�[�\��(���[���h���W�n)�̃��C���擾
		Vector2 pos = m_rectTransform.anchoredPosition;
		return Camera.main.ScreenPointToRay(new Vector3(pos.x + Screen.width / 2.0f, pos.y + Screen.height / 2.0f, 0.0f));
	}
}