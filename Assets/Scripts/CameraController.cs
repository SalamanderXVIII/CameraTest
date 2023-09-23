using UnityEngine;
using Cinemachine;
using UnityEngine.U2D;
using Unity.VisualScripting;

namespace Scene
{
	public class CameraController : MonoBehaviour
	{
		[Header("Components")]
		[Tooltip("Virtual Camera. It is an object that contains/will contain CinemachineVirtualCamera. Uses game object that script is attached to or it can be asigned manually")]
		public GameObject virtualCamera;

		[Header("Camera")]
		[Tooltip("Camera smoothness with which it moves. Bigger number = smoother")]
		public float cameraSmoothness = 1;
		[Tooltip("Camera X resolution")]
		public int xResolution = 1280;
		[Tooltip("Camera Y resolution")]
		public int yResolution = 720;
		[Tooltip("Camera's ortho size. Changes overall camera size")]
		public float cameraOrthoSize = 3f;

		[Header("Misc")]
		[Tooltip("Transform component of player object")]
		public Transform playerTransform;
		[Tooltip("Camera borders which camera is not supposed to cross.")]
		public PolygonCollider2D polygonCollider;

		private Vector2 screenBounds;
		private float objectWidth;
		private float objectHeight;

		private void Awake()
		{
			CinemachineComponentsInitialisation();
			CinemachineVirtualValues();
		}

		//Pastes values asigned in script to it's respective parameters in CinemachineVirtualCamera and main Camera.
		public void CinemachineVirtualValues()
		{
			virtualCamera.GetComponent<CinemachineVirtualCamera>().m_Follow = playerTransform;
			virtualCamera.GetComponent<CinemachineVirtualCamera>().GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = polygonCollider;
			virtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = cameraOrthoSize;
			PixelPerfectCamera mainPixelResolution = Camera.main.GetComponent<PixelPerfectCamera>();
			mainPixelResolution.refResolutionX = xResolution;
			mainPixelResolution.refResolutionY = yResolution;
			CinemachineTransposer transposer = virtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
			transposer.m_XDamping = cameraSmoothness;
			transposer.m_YDamping = cameraSmoothness;
			transposer.m_ZDamping = cameraSmoothness;
		}

		//Checks for all of the necessary components and adds them if absent.
		private void CinemachineComponentsInitialisation()
		{
			if (virtualCamera == null)
			{
				virtualCamera = gameObject;
			}

			Camera.main.gameObject.TryGetComponent<CinemachineBrain>(out var brain);
			if (brain == null)
			{
				brain = Camera.main.gameObject.AddComponent<CinemachineBrain>();

				Debug.Log("CinemachineBrain initialized.");
			}

			Camera.main.gameObject.TryGetComponent<PixelPerfectCamera>(out var pixelCamera);
			if (pixelCamera == null)
			{
				pixelCamera = Camera.main.gameObject.AddComponent<PixelPerfectCamera>();

				Debug.Log("PixelPerfectCamera initialized");
			}

			virtualCamera.TryGetComponent<CinemachineVirtualCamera>(out var virtCam);
			if (virtCam == null)
			{
				virtCam = virtualCamera.AddComponent<CinemachineVirtualCamera>();
				virtCam.AddCinemachineComponent<CinemachineTransposer>();
				virtCam.AddComponent<CinemachineConfiner2D>();
				virtCam.AddComponent<Cinemachine.CinemachinePixelPerfect>();

				Debug.Log("CinemachineVirtualCamera initialized");
			}
			else
			{
				virtualCamera.TryGetComponent<CinemachineConfiner2D>(out var conf);
				if (conf == null)
				{
					conf = virtualCamera.AddComponent<CinemachineConfiner2D>();

					Debug.Log("CinemachineConfiner2D initialized");
				}

				virtualCamera.TryGetComponent<CinemachineTransposer>(out var trans);
				if (trans == null)
				{
					trans = virtualCamera.AddComponent<CinemachineTransposer>();

					Debug.Log("CinemachineTransposer initialized");
				}

				virtualCamera.TryGetComponent<Cinemachine.CinemachinePixelPerfect>(out var pixelVirtualCheck);
				if (pixelVirtualCheck == null)
				{
					pixelVirtualCheck = virtualCamera.AddComponent<Cinemachine.CinemachinePixelPerfect>();

					Debug.Log("CinemachineTransposer initialized");
				}
			}
		}

		////Sets camera bounds.
		//private void boundValuesSet()
		//{
		//	objectWidth = playerTransform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
		//	objectHeight = playerTransform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
		//}

		////Keeps player within camera bounds.
		//private void keepingPlayerInBounds()
		//{
		//	screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		//	Vector3 viewPos = playerTransform.position;
		//	viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 - objectWidth, screenBounds.x + objectWidth);
		//	viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 - objectHeight, screenBounds.y + objectHeight);
		//	playerTransform.position = viewPos;
		//}
	}
}


