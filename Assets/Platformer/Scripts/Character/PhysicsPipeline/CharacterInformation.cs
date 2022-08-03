using UnityEngine;

namespace Platformer
{
	public struct CharacterInformation
	{
		public Vector3 Velocity;
		public Vector3 Position;
		public FootHit FootHit;
		public bool IsOnGround;
	}
}