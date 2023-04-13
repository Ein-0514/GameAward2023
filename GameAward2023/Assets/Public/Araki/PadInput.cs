using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PadInput
{
	static Dictionary<KeyCode, KeyCode> m_keyCode;
	static Dictionary<string, string> m_axisName;

	/// <summary>
	/// �Q�[���N�����ɌĂяo�����
	/// </summary>
	[RuntimeInitializeOnLoadMethod]
	static void Initialize()
	{
		//--- �v�f�̒ǉ�
		m_keyCode = new Dictionary<KeyCode, KeyCode>();
		m_keyCode.Add(KeyCode.JoystickButton0, KeyCode.Space);			// A�{�^��
		m_keyCode.Add(KeyCode.JoystickButton1, KeyCode.Backspace);		// B�{�^��
		m_keyCode.Add(KeyCode.JoystickButton2, KeyCode.Return);			// X�{�^��
		m_keyCode.Add(KeyCode.JoystickButton3, KeyCode.RightShift);		// Y�{�^��
		m_keyCode.Add(KeyCode.JoystickButton4, KeyCode.Q);				// L�{�^��
		m_keyCode.Add(KeyCode.JoystickButton5, KeyCode.E);				// R�{�^��
		m_keyCode.Add(KeyCode.JoystickButton6, KeyCode.LeftShift);		// �r���[�{�^��
		m_keyCode.Add(KeyCode.JoystickButton7, KeyCode.LeftControl);	// ���j���[�{�^��
		m_keyCode.Add(KeyCode.JoystickButton8, KeyCode.LeftAlt);		// ���X�e�B�b�N��������
		m_keyCode.Add(KeyCode.JoystickButton9, KeyCode.RightAlt);		// �E�X�e�B�b�N��������
		m_keyCode.Add(KeyCode.JoystickButton10, KeyCode.RightAlt);		// �E�X�e�B�b�N��������

		//--- �v�f�̒ǉ�
		m_axisName = new Dictionary<string, string>();
		m_axisName.Add("Horizontal_R", "Horizontal_Arrow");	// �E�X�e�B�b�N[��������]
		m_axisName.Add("Vertical_R", "Vertical_Arrow");		// �E�X�e�B�b�N[��������]
		m_axisName.Add("Horizontal_L", "Horizontal_AD");	// ���X�e�B�b�N[��������]
		m_axisName.Add("Vertical_L", "Vertical_SW");		// ���X�e�B�b�N[��������]
		m_axisName.Add("Horizontal_PadX", "Horizontal_JL"); // �\���L�[[��������]
		m_axisName.Add("Vertical_PadX", "Vertical_KI");		// �\���L�[[��������]
	}

	/// <summary>
	/// �v���X����
	/// </summary>
	public static bool GetKey(KeyCode keyCode)
	{
#if UNITY_EDITOR || DEVELOPMENT_BUILD	// �f�o�b�O�p����

		// �L�[�{�[�h��������͂��擾
		if (m_keyCode.ContainsKey(keyCode))
			return Input.GetKey(keyCode) || Input.GetKey(m_keyCode[keyCode]);
		
		return Input.GetKey(keyCode);

#else  // �����[�X�p����
		return Input.GetKey(keyCode);
#endif
	}

	/// <summary>
	/// �g���K�[����
	/// </summary>
	public static bool GetKeyDown(KeyCode keyCode)
	{
#if UNITY_EDITOR || DEVELOPMENT_BUILD  // �f�o�b�O�p����

		// �L�[�{�[�h��������͂��擾
		if (m_keyCode.ContainsKey(keyCode))
			return Input.GetKeyDown(keyCode) || Input.GetKeyDown(m_keyCode[keyCode]);

		return Input.GetKeyDown(keyCode);

#else   // �����[�X�p����
		return Input.GetKeyDown(keyCode);
#endif
	}

	/// <summary>
	/// �����[�X����
	/// </summary>
	public static bool GetKeyUp(KeyCode keyCode)
	{
#if UNITY_EDITOR || DEVELOPMENT_BUILD  // �f�o�b�O�p����

		// �L�[�{�[�h��������͂��擾
		if (m_keyCode.ContainsKey(keyCode))
			return Input.GetKeyUp(keyCode) || Input.GetKeyUp(m_keyCode[keyCode]);

		return Input.GetKeyUp(keyCode);

#else   // �����[�X�p����
		return Input.GetKeyUp(keyCode);
#endif
	}

	/// <summary>
	/// �X�e�B�b�N(�\���L�[)����
	/// ���v���X����
	/// </summary>
	public static float GetAxis(string axisName)
	{
#if UNITY_EDITOR || DEVELOPMENT_BUILD  // �f�o�b�O�p����

		// �Q�[���p�b�h����̓���
		float axis = Input.GetAxis(axisName);

		// �Q�[���p�b�h�̓��͂�D��
		if (Mathf.Abs(axis) > 0.0f) return axis;

		// �L�[�{�[�h��������͂��擾
		if (m_axisName.ContainsKey(axisName))
			axis = Input.GetAxis(m_axisName[axisName]);

		return axis;

#else   // �����[�X�p����
		return Input.GetAxis(axisName);
#endif
	}

	/// <summary>
	/// �X�e�B�b�N(�\���L�[)����(-1 �` 0 �` 1)
	/// ���v���X����
	/// </summary>
	public static int GetAxisRaw(string axisName)
	{
#if UNITY_EDITOR || DEVELOPMENT_BUILD  // �f�o�b�O�p����

		// �Q�[���p�b�h����̓���
		int axis = (int)Input.GetAxisRaw(axisName);

		// �L�[�{�[�h��������͂��擾
		if (m_axisName.ContainsKey(axisName))
			axis += (int)Input.GetAxisRaw(m_axisName[axisName]);

		// ���͂��Ȃ��ꍇ
		if (Mathf.Abs(axis) <= 0) return 0;

		// ����������͂��������ꍇ���l�����Čv�Z
		return axis / Mathf.Abs(axis);

#else   // �����[�X�p����
		return (int)Input.GetAxisRaw(axisName);
#endif
	}

	/// <summary>
	/// �X�e�B�b�N(�\���L�[)����
	/// �����s�[�g����
	/// </summary>
	public static int GetAxisRawRepeat(string axisName)
	{
#if UNITY_EDITOR || DEVELOPMENT_BUILD  // �f�o�b�O�p����

		// �Q�[���p�b�h����̓���
		int axis = AxisInput.GetAxisRawRepeat(axisName);

		// �L�[�{�[�h��������͂��擾
		if (m_axisName.ContainsKey(axisName))
			axis += AxisInput.GetAxisRawRepeat(m_axisName[axisName]);

		// ���͂��Ȃ��ꍇ
		if (Mathf.Abs(axis) <= 0) return 0;

		// ����������͂��������ꍇ���l�����Čv�Z
		return axis / Mathf.Abs(axis);

#else   // �����[�X�p����
		return AxisInput.GetAxisRawRepeat(axisName);
#endif
	}
}