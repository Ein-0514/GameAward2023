using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewController : MonoBehaviour
{
	[SerializeReference] TargetFace m_targetFace;
	[SerializeReference] CoreController m_coreController;
	[SerializeReference] SeController m_seController;

    // Start is called before the first frame update
    void Start()
    {
		this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		// A�{�^��
		if(Input.GetKeyDown(KeyCode.JoystickButton0))
		{
			//--- ��]���łȂ����̂ݏ���
			if (!m_coreController.CoreSetting.IsRotate())
			{
				bool isAttach = false;

				//--- �q�I�u�W�F�N�g����K���N�^��T��
				foreach (Transform child in this.transform)
				{
					// �K���N�^�ȊO�̓X���[
					if (child.transform.tag != "Junk") continue;

					isAttach = m_coreController.CoreSetting.AttachJunk(child.gameObject);
					break;
				}

				//--- �A�^�b�`�������������̂ݏ�������
				if (isAttach)
				{
					Active = false; // �������g�𖳌���
					m_seController.PlaySe("SetParts");
				}
			}
		}

		// B�{�^��
        if(Input.GetKeyDown(KeyCode.JoystickButton1))
		{
			//--- �q�I�u�W�F�N�g����K���N�^��T��
			foreach(Transform child in this.transform)
			{
				// �K���N�^�ȊO�̓X���[
				if (child.transform.tag != "Junk") continue;

				child.GetComponent<JunkController>().ResetTransform();	// ���W�E��]�E�e��������
			}

			Active = false;	// �������g�𖳌���
		}
    }

	public bool Active
	{
		set { this.gameObject.SetActive(value); }
		get { return this.gameObject.activeSelf; }
	}

	public TargetFace TargetFace
	{
		get { return m_targetFace; }
	}
}