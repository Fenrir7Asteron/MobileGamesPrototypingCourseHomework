using UnityEngine;
using CW.Common;
using Photon.Pun;

namespace Destructible2D.Examples
{
	/// <summary>This component turns the current Rigidbody2D into a spaceship that can jump in position while slicing and can be controlled with the <b>Horizontal</b> and <b>Vertical</b> and <b>Jump</b> input axes.</summary>
	[RequireComponent(typeof(Rigidbody2D))]
	[HelpURL(D2dCommon.HelpUrlPrefix + "D2dSpaceshipJumper")]
	[AddComponentMenu(D2dCommon.ComponentMenuPrefix + "Spaceship Jumper")]
	public class D2dSpaceshipJumper : MonoBehaviourPun
	{
		/// <summary>The controls used to turn left and right.</summary>
		public CwInputManager.Axis TurnControls = new CwInputManager.Axis(1, false, CwInputManager.AxisGesture.HorizontalPull, 0.01f, KeyCode.A, KeyCode.D, KeyCode.LeftArrow, KeyCode.RightArrow, 1.0f);

		/// <summary>The controls used to accelerate and reverse.</summary>
		public CwInputManager.Axis MoveControls = new CwInputManager.Axis(1, false, CwInputManager.AxisGesture.VerticalPull, 0.01f, KeyCode.S, KeyCode.W, KeyCode.DownArrow, KeyCode.UpArrow, 1.0f);

		/// <summary>The controls used to make the spaceship shoot.</summary>
		public CwInputManager.Trigger JumpControls = new CwInputManager.Trigger(true, false, KeyCode.Space);

		/// <summary>Minimum time between each jump in seconds.</summary>
		public float JumpDelay = 1.0f;

		/// <summary>The jump distance in world space units.</summary>
		public float JumpDistance = 10.0f;

		/// <summary>The turning force.</summary>
		public float TurnTorque = 10.0f;

		/// <summary>The prefab that will be placed along the slice.</summary>
		public D2dSlicer SlicePrefab;

		/// <summary>The main thrusters.</summary>
		public D2dThruster[] Thrusters;
		public float JumpCacheTime;

		public float JumpVelocity;
		public ControlledCollider ControlledCollider;


		[System.NonSerialized]
		private Rigidbody2D cachedRigidbody2D;

		// Seconds until next shot is available
		private float cooldown;
        private ButtonInput m_JumpInput;
        private float m_LastJumpPressedTime;
        private bool m_JumpInputIsCached;


        public void SetPlayerInput(PlayerInput a_PlayerInput)
		{
			if (a_PlayerInput.GetButton("TeleportJump") != null)
			{ 
				m_JumpInput = a_PlayerInput.GetButton("TeleportJump");
			}
			else
			{
				Debug.LogError("Teleport jump input not set up in character input");
			}
		}

		protected virtual void OnEnable()
		{
			CwInputManager.EnsureThisComponentExists();

			if (cachedRigidbody2D == null) cachedRigidbody2D = GetComponent<Rigidbody2D>();

		}

		protected virtual void FixedUpdate()
		{
			if (!photonView.IsMine)
			{
				return;
			}

			cooldown -= Time.fixedDeltaTime;

			if (m_JumpInput.m_WasJustPressed)
            {
                m_JumpInput.m_WasJustPressed = false;
                m_LastJumpPressedTime = Time.fixedTime;
                m_JumpInputIsCached = true;
            }

			if (m_JumpInputIsCached)
			{
				m_JumpInput.m_WasJustPressed = false;
				DoJump();
			}

            if (m_JumpInputIsCached)
            {
                //Jump has not been started in time; jump cancelled
                if (Time.fixedTime - m_LastJumpPressedTime >= JumpCacheTime)
                {
                    m_JumpInputIsCached = false;
                }
            }
		}

		private void DoJump()
		{
			if (cooldown > 0.0f)
			{
				return;
			}

			m_JumpInputIsCached = false;

			cooldown = JumpDelay;

			var oldPosition = transform.position;

			transform.Translate(0.0f, JumpDistance, 0.0f, Space.Self);

			var newPosition = transform.position;

			if (SlicePrefab != null)
			{
				var indicator = Instantiate(SlicePrefab);

				indicator.SetTransform(oldPosition, newPosition);

				indicator.gameObject.SetActive(true);
			}

			// Set character vertical velocity
			ControlledCollider.SetVelocity(new Vector2(ControlledCollider.GetVelocity().x, JumpVelocity));
		}
	}
}

#if UNITY_EDITOR
namespace Destructible2D.Examples
{
	using UnityEditor;
	using TARGET = D2dSpaceshipJumper;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(TARGET))]
	public class D2dSpaceshipJumper_Editor : CwEditor
	{
		protected override void OnInspector()
		{
			TARGET tgt; TARGET[] tgts; GetTargets(out tgt, out tgts);

			Draw("TurnControls", "The controls used to turn left and right.");
			Draw("MoveControls", "The controls used to accelerate and reverse.");
			Draw("JumpControls", "The controls used to make the spaceship jump.");

			Separator();

			BeginError(Any(tgts, t => t.JumpDelay < 0.0f));
				Draw("JumpDelay", "Minimum time between each jump in seconds.");
			EndError();
			Draw("JumpDistance", "The jump distance in world space units.");
			Draw("TurnTorque", "The turning force.");
			Draw("SlicePrefab", "The prefab that will be placed along the slice.");

			Separator();

			Draw("Thrusters", "The main thrusters.");

			Draw("JumpCacheTime", "Jump cache time");
			Draw("JumpVelocity", "JumpVelocity");
			Draw("ControlledCollider", "ControlledCollider");
		}
	}
}
#endif