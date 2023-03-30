using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreController : MonoBehaviour
{
	CoreSetting m_coreSetting;
	bool m_isStart;
	int m_freamCnt;

	// Start is called before the first frame update
	void Start()
	{
		m_coreSetting = GetComponent<CoreSetting>();
		m_isStart = false;
		m_freamCnt = 0;
	}

	// Update is called once per frame
	void Update()
	{
		//--- ��
		if (!m_isStart) return;

		m_freamCnt++;
		if (m_freamCnt > 600)	SceneManager.LoadScene("GameScene");
	}

	public void StartCore()
	{
		m_coreSetting.CoreReady();	// �R�A�Z�b�e�B���O���̐ݒ���X�e�[�W�p��

		foreach(Transform child in this.transform)
		{
			// �K���N�^�ȊO���X���[
			if (child.transform.tag != "Junk") continue;

			// �S�Ẵp�[�c���X�^�[�g������
			child.GetComponent<JunkController>().StartJunk();
		}

		m_isStart = true;
	}

	public CoreSetting CoreSetting
	{
		get { return m_coreSetting; }
	}
}