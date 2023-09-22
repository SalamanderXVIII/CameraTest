using UnityEngine;
using Cinemachine;
using UnityEngine.U2D;

namespace Scene
{
	public class CameraController : MonoBehaviour
	{
		[Header("�������� �����������")]
		//[SerializeField] private CinemachineBrain cameraBrain;
		[Tooltip("����������� ������")]
		public GameObject virtualCamera;

		[Header("��������� ������")]
		[Tooltip("��������� ���������� ������. ������ = �������")]
		public float cameraSmoothness;
		[Tooltip("���������� ������")]
		[SerializeField] private int xResolution;
		[SerializeField] private int yResolution;

		[Header("������ ���������")]
		[Tooltip("Transform ��������� ������� ������")]
		public Transform playerTransform;
		[Tooltip("������� ������, �� ������� ��� �� ������ �����")]
		public PolygonCollider2D polygonCollider;

		private void Awake()
		{
			CinemachineComponentsInitialisation();
			CinemachineInitialisation();
		}

		private void CinemachineInitialisation()
		{
			virtualCamera.GetComponent<CinemachineVirtualCamera>().m_Follow = playerTransform;
			virtualCamera.GetComponent<CinemachineVirtualCamera>().GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = polygonCollider;
			PixelPerfectCamera mainPixelResolution = Camera.main.GetComponent<PixelPerfectCamera>();
			mainPixelResolution.refResolutionX = xResolution;
			mainPixelResolution.refResolutionY = yResolution;
			CinemachineTransposer transposer = virtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
			transposer.m_XDamping = cameraSmoothness;
			transposer.m_YDamping = cameraSmoothness;
			transposer.m_ZDamping = cameraSmoothness;
		}

		private void CinemachineComponentsInitialisation()
		{
			Camera.main.gameObject.TryGetComponent<CinemachineBrain>(out var brain);
			if (brain == null)
			{
				brain = Camera.main.gameObject.AddComponent<CinemachineBrain>();

				Debug.Log("��������������� CinemachineBrain.");
			}
			else
				Debug.Log("CinemachineBrain �� ������� �������������.");

			virtualCamera.TryGetComponent<CinemachineVirtualCamera>(out var virtCam);
			if (virtCam == null)
			{
				virtCam = virtualCamera.AddComponent<CinemachineVirtualCamera>();

				Debug.Log("��������������� CinemachineVirtualCamera");
			}
			else
				Debug.Log("CinemachineVirtualCamera �� ������� �������������.");
		}
	}
}


