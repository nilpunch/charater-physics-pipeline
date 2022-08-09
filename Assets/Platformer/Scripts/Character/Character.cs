using UnityEngine;

namespace Platformer
{
	public class Character : MonoBehaviour
	{
		[SerializeField] private CharacterVerticalMovement _characterVerticalMovement;
		[SerializeField] private CharacterHorizontalMovement _characterHorizontalMovement;

		public void Jump()
		{
			_characterVerticalMovement.Jump();
		}
		
		public void Fall()
		{
			_characterVerticalMovement.Fall();
		}

		public void Move(Vector3 direction)
		{
			_characterHorizontalMovement.Move(direction);
		}
	}
}