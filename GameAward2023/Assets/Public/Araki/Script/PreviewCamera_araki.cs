using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PreviewCamera_araki: MonoBehaviour
{
	VideoPlayer m_videoPlayer;
	int m_freamCnt;
	bool m_isEndNoise;

    // Start is called before the first frame update
    void Start()
    {
		m_videoPlayer = GetComponent<VideoPlayer>();
		m_freamCnt = 0;
		m_isEndNoise = false;
		gameObject.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
		m_isEndNoise = false;
		if(!m_videoPlayer.isPlaying) return;

		m_freamCnt++;	// �t���[�����J�E���g

		//--- 1�b�Ńm�C�Y���I��
		if (m_freamCnt > 60)
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
		gameObject.SetActive(true);		// ���M��L����
		m_videoPlayer.Play();			// �m�C�Y������Đ�

		m_freamCnt = 0;	// �J�E���g�����Z�b�g
	}

	/// <summary>
	/// �v���r���[�p�J�����̖�����
	/// </summary>
	public void EndPreview()
	{
		gameObject.SetActive(false);	// ���M�𖳌���
	}

	public bool isEndNoise
	{
		get { return m_isEndNoise; }
	}
}
