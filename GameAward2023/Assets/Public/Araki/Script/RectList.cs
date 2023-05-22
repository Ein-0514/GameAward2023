using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RectList : MonoBehaviour
{
	List<GameObject> m_rectList;

	// Start is called before the first frame update
	void Start()
	{
		m_rectList = new List<GameObject>();

		//--- �q��RectTransform�����X�g��
		int childCnt = transform.childCount;
		for (int i = 0; i < childCnt; i++)
		{
			m_rectList.Add(transform.GetChild(i).gameObject);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// �摜�̐؂�ւ�
	/// </summary>
	/// <param name="index">���ԍ�</param>
	/// <param name="image">�摜�f�[�^</param>
	public void ChangeImage(int index, Sprite image)
	{
		if (index >= m_rectList.Count) return;
		Image currentImage = m_rectList[index].GetComponent<Image>();
		if (currentImage == null) return;
		currentImage.sprite = image;
	}

	List<RectTransform> rectList
	{
		get {
			List<RectTransform>list = new List<RectTransform>();

			//--- �q��RectTransform�����X�g��
			int childCnt = transform.childCount;
			for (int i = 0; i < childCnt; i++)
			{
				RectTransform rect = transform.GetChild(i).GetComponent<RectTransform>();
				if (rect == null) continue;

				list.Add(rect);
			}

			return list;
		}
	}
}