using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static UnityEngine.Rendering.DebugUI;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;		// 이동 입력 현황
		public Vector2 look;		// 시야 회전 현황
		public bool jump;			// 점프 버튼 현황
		public bool sprint;			// 달리기 모드 현황

		[Header("Movement Settings")]
		public bool analogMovement;	// 아날로그 스틱이 있는지 여부(true면 있다, false면 없다)

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;		// 커서락 기능을 사용할지 여부(락이 되면 마우스커서가 안보인다)
		public bool cursorInputForLook = true;  // 커서 입력을 카메라 회전용으로 사용

        public void OnMove(InputAction.CallbackContext context)
		{
			MoveInput(context.ReadValue<Vector2>());			
        }

		public void OnLook(InputAction.CallbackContext context)
		{
            if (cursorInputForLook)
            {
                LookInput(context.ReadValue<Vector2>());
            }
        }

		public void OnJump(InputAction.CallbackContext context)
		{
			JumpInput(context.performed);
        }

		public void OnSprint(InputAction.CallbackContext context)
		{
            SprintInput(context.ReadValue<float>() > 0.1f);
            //SprintInput(context.ReadValueAsButton());
        }

		public void OnFire(InputAction.CallbackContext context)
		{
			Debug.Log("Fire");
		}

		public void OnZoom(InputAction.CallbackContext context)
		{
			Debug.Log("Zoom");
		}

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		/// <summary>
		/// 응용프로그램에 포커스가 가면 실행되는 함수
		/// </summary>
		/// <param name="hasFocus">true면 포커스가 갔다. false면 포커스가 나갔다.</param>
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);	// 상태 변경
		}

		/// <summary>
		/// 커서의 락상태를 변경하는 함수
		/// </summary>
		/// <param name="newState">true면 락을 한다, false면 락을 해제한다.</param>
		private void SetCursorState(bool newState)
		{
			// 상태가 lock이되면 커서는 안보이게 되고 항상 가운데에 있다고 가정한다.
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}