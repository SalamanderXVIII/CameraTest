using UnityEngine;
using Cinemachine;
using UnityEngine.U2D;

namespace Scene
{
	public class CameraController : MonoBehaviour
	{
		[Header("Привязка компонентов")]
		//[SerializeField] private CinemachineBrain cameraBrain;
		[Tooltip("Виртуальная камера")]
		public GameObject virtualCamera;

		[Header("Параметры камеры")]
		[Tooltip("Плавность следования камеры. Больше = плавнее")]
		public float cameraSmoothness;
		[Tooltip("Разрешение камеры")]
		[SerializeField] private int xResolution;
		[SerializeField] private int yResolution;

		[Header("Прочие параметры")]
		[Tooltip("Transform компонент объекта игрока")]
		public Transform playerTransform;
		[Tooltip("Границы камеры, за которые она не сможет зайти")]
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

				Debug.Log("Инициализирован CinemachineBrain.");
			}
			else
				Debug.Log("CinemachineBrain не требует инициализации.");

			virtualCamera.TryGetComponent<CinemachineVirtualCamera>(out var virtCam);
			if (virtCam == null)
			{
				virtCam = virtualCamera.AddComponent<CinemachineVirtualCamera>();

				Debug.Log("Инициализирован CinemachineVirtualCamera");
			}
			else
				Debug.Log("CinemachineVirtualCamera не требует инициализации.");
		}
	}
}


