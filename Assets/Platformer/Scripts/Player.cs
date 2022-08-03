using UnityEngine;

namespace Platformer
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private Character _character;
		
		private PlayerInput _playerInput;

		private void Awake()
		{
			_playerInput = new PlayerInput();
			_playerInput.Enable();
		}

		private void Update()
		{
			Vector2 moveInput = _playerInput.Character.Move.ReadValue<Vector2>();
			_character.Move(new Vector3(moveInput.x, 0f, moveInput.y));

			if (_playerInput.Character.Jump.WasPerformedThisFrame())
			{
				if (_playerInput.Character.Jump.IsPressed())
				{
					_character.Jump();
				}
				else
				{
					_character.Fall();
				}
			}
		}
	}
}