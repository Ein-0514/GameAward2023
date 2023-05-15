using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PreviewCamera_araki: MonoBehaviour
{
	VideoPlayer m_videoPlayer;	// ���摀��p
	int m_freamCnt;				// �t���[���J�E���g
	bool m_isEndNoise;			// �m�C�Y�I���̏u��

    // Start is called before the first frame update
    void Start()
    {
		m_videoPlayer = GetComponent<VideoPlayer>();
		m_freamCnt = 0;
		m_isEndNoise = false;
	}

    // Update is called once per frame
    void Update()
    {
		m_isEndNoise = false;
		if(!m_videoPlayer.isPlaying) return;

		m_freamCnt++;	// �t���[�����J�E���g

		//--- 1�b�Ńm�C�Y���I��
		if (m_freamCnt > 30)
		{
			m_freamCnt = 0;                 // �J�E���g�����Z�b�g
			m_videoPlayer.Stop();			// �m�C�Y������~
			m_isEndNoise = true;            // �m�C�Y�I���t���O�𗧂Ă�
		}
    }

	/// <summary>
	/// �m�C�Y���Đ�
	/// </summary>
	public void StartNoise()
	{
		m_videoPlayer.Play();	// �m�C�Y���Đ�

		m_freamCnt = 0;
	}

	public bool isEndNoise
	{
		get { return m_isEndNoise; }
	}
}
