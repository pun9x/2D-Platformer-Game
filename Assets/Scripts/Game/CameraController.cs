using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class cameracontroller : monobehaviour
{
	[serializefield] private transform player;
	[serializefield] private float followspeed = 2f;
	[serializefield] private float yoffset = 1f;

	private void update()
	{
		vector3 newpos = new vector3(player.position.x, player.position.y + yoffset, -10f);
		transform.position = vector3.slerp(transform.position, newpos, followspeed * time.deltatime);
	}
}
*/

public class CameraController : MonoBehaviour
	{

		public delegate void ParallaxCameraDelegate ( Vector3 deltaMovement );

		public ParallaxCameraDelegate onCameraTranslate;

		private static CameraController m_Singleton;

		public static CameraController Singleton
		{
			get
			{
				return m_Singleton;
			}
		}

		[SerializeField] private Camera Camera;
		[SerializeField] private Transform Followee;
		[SerializeField] private float MinY = 0f;
		[SerializeField] private float MinX = 0f;
		[SerializeField] private CameraController ShakeControl;
		[SerializeField] private float FastMoveSpeed = 10f;
		[SerializeField] private float Speed = 1f;
		private bool FastMove = false;
		private Vector3 OldPosition;

		public bool fastMove
		{
			get
			{
				return FastMove;
			}
			set
			{
				FastMove = value;
			}
		}

		void Awake ()
		{
			m_Singleton = this;
			ShakeControl = GetComponent<CameraController> ();
		}

		void Start ()
		{
			OldPosition = transform.position;
		}

		void Update ()
		{
//			if (!ShakeControl.IsShaking) {
			Follow ();
//			}
			if ( transform.position != OldPosition )
			{
				if ( onCameraTranslate != null )
				{
					Vector3 delta = OldPosition - transform.position;
					onCameraTranslate ( delta );
				}
				OldPosition = transform.position;
			}
		}

		public void Follow ()
		{
			float speed = Speed;
			if ( FastMove )
			{
				speed = FastMoveSpeed;
			}
			Vector3 cameraPosition = transform.position;
			Vector3 targetPosition = Followee.position;
			if ( targetPosition.x - Camera.orthographicSize * Camera.aspect > MinX )
			{
				cameraPosition.x = targetPosition.x;
			}
			else
			{
				cameraPosition.x = MinX + Camera.orthographicSize * Camera.aspect;
			}
			if ( targetPosition.y - Camera.orthographicSize > MinY )
			{
				cameraPosition.y = targetPosition.y;
			}
			else
			{
				cameraPosition.y = MinY + Camera.orthographicSize;
			}
			transform.position = Vector3.MoveTowards ( transform.position, cameraPosition, speed );
			if ( transform.position == targetPosition && FastMove )
			{
				FastMove = false;
			}
		}

	}