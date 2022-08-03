using UnityEngine;

namespace Platformer
{
	public class Character : MonoBehaviour
	{
		[SerializeField] private CharacterJump _characterJump;
		[SerializeField] private CharacterMovement _characterMovement;

		public void Jump()
		{
			_characterJump.Jump();
		}
		
		public void Fall()
		{
			_characterJump.Fall();
		}

		public void Move(Vector3 direction)
		{
			_characterMovement.Move(direction);
		}
	}
}