using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerCopy: MonoBehaviour
{
	[SerializeReference] GameSceneController m_gameSceneController;
	[SerializeReference] SeController m_seController;
	//[SerializeReference] CoreControllerCopy m_coreController;
	//[SerializeReference] PreviewControllerCopy m_previewController;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		////--- A�{�^������������
		//if (Input.GetKeyDown(KeyCode.JoystickButton0))
		//{
		//	//--- �v���r���[���L���łȂ��ꍇ�̂ݑI���\
		//	if (!m_previewController.Active)
		//	{
		//		// ����p�̃��C��p��
		//		Ray ray = CursorController.GetCameraToRay();
		//		RaycastHit hit;

		//		if (Physics.Raycast(ray, out hit))
		//		{
		//			// �K���N�^�ł͂Ȃ��Ȃ�X���[
		//			if (hit.transform.tag != "Junk") return;

		//			// �v���r���[��L����
		//			m_previewController.Active = true;
		//			m_previewController.TargetFace.AttachJunk(hit.transform.gameObject);

		//			m_seController.PlaySe("Select");
		//		}
		//	}
		//}

		////--- B�{�^������������
		//if (Input.GetKeyDown(KeyCode.JoystickButton1))
		//{
		//	//--- �v���r���[���L���łȂ��ꍇ�̂ݐ؂藣���\
		//	if (!m_previewController.Active)
		//	{
		//		m_coreController.CoreSetting.DetachJunk();
		//	}
		//}

		////--- X�{�^������������
		//if(Input.GetKeyDown(KeyCode.JoystickButton2) && !m_gameSceneController.IsStart)
		//{
		//	//--- �e�I�u�W�F�N�g�̃X�^�[�g����
		//	m_coreController.StartCore();
		//	m_gameSceneController.StartStage();
		//}
	}
}