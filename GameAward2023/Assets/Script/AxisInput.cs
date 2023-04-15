using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AxisInput
{
	//static int m_lastRaw = 0;
	static string m_lastAxisName = null;
	static int m_frameCnt = 0;

	/// <summary>
	/// �����͂̃��s�[�g���͂��擾
	/// </summary>
	public static int GetAxisRawRepeat(string axisName)
	{
		const int START_PRESS_CNT = 40;     //����J�n�t���[��
		const int INTERVAL_PRESS_CNT = 7;	//����Ԋu�t���[��

		// ���̓��͂��擾(-1.0�`1.0)
		int axisRaw = (int)PadInput.GetAxisRaw(axisName);

		//--- ������ĂȂ��ꍇ
		if (axisRaw == 0)
		{
			//--- �O�t���[���Ɠ���L�[
			if (m_lastAxisName == axisName)
			{
				m_lastAxisName = null;
				m_frameCnt = 0;
			}
			return axisRaw;
		}
		//--- ������Ă���ꍇ
		else
		{
			//--- �O�t���[���ƈقȂ�L�[
			if (m_lastAxisName != axisName)
			{
				m_frameCnt = 1;         // �ŏ��̃J�E���g
				m_lastAxisName = axisName;    // �ߋ��̓��͂Ƃ��đޔ�

				return axisRaw;
			}

			m_lastAxisName = axisName;	// �ߋ��̓��͂Ƃ��đޔ�
			
			//--- ���s�[�g���͒�
			if (m_frameCnt > 0)
			{
				m_frameCnt++;   // �t���[�������J�E���g

				// ���s�[�g���͔���
				if (m_frameCnt == START_PRESS_CNT)	return axisRaw;				

				//--- �t���[���𔻒�J�n�O�ɖ߂�
				if (m_frameCnt == START_PRESS_CNT + INTERVAL_PRESS_CNT + 1)
					m_frameCnt = START_PRESS_CNT - INTERVAL_PRESS_CNT;
			}

			return 0;
		}
	}
}