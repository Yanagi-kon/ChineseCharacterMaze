using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
	// ������
	public Slider processBar;

	// Application.LoadLevelAsync()��������ķ���ֵ������AsyncOperation
	private AsyncOperation async;

	// ��ǰ���ȣ����ƻ������İٷֱ�
	private uint nowprocess = 0;


	void Start()
	{
		// ����һ��Э��
		StartCoroutine(loadScene());
	}

	// ����һ��Э��
	IEnumerator loadScene()
	{
		// �첽��ȡ����
		// ָ����Ҫ���صĳ�����
		async = SceneManager.LoadSceneAsync("TestScene");

		// ���ü�����ɺ����Զ���ת����
		async.allowSceneActivation = false;

		// ������ɺ󷵻�async
		yield return async;

	}

	void Update()
	{
		// �ж��Ƿ��������Ҫ��ת�ĳ�������
		if (async == null)
		{
			// ���û�����꣬������update������������ִ��return����Ĵ���
			return;
		}

		// ��������Ҫ����Ľ���ֵ
		uint toProcess;
		Debug.Log(async.progress * 100);

		// async.progress �����ڶ�ȡ�ĳ����Ľ���ֵ  0---0.9
		// �����ǰ�Ľ���С��0.9��˵������û�м�����ɣ���˵������������Ҫ�ƶ�
		// ��������������ݼ�����ϣ�async.progress ��ֵ�ͻ����0.9
		if (async.progress < 0.9f)
		{
			//  ����ֵ
			toProcess = (uint)(async.progress * 100);
		}
		// �����ִ�е����else��˵���Ѿ��������
		else
		{
			// �ֶ����ý���ֵΪ100
			toProcess = 100;
		}

		// ����������ĵ�ǰ���ȣ�С�ڣ���ǰ���س����ķ������صĽ���
		if (nowprocess < toProcess)
		{
			// ��ǰ�������Ľ��ȼ�һ
			nowprocess++;
		}

		// ���û�������value
		///processBar.value = nowprocess / 100f;

		// �����������ֵ����100��˵���������
		if (nowprocess == 100)
		{
			// ����Ϊtrue��ʱ������������ݼ�����ϣ��Ϳ����Զ���ת����
			async.allowSceneActivation = true;
		}
	}
}
