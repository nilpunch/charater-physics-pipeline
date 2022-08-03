using UnityEngine;

namespace Platformer
{
	public abstract class CharacterPhysicsModule : MonoBehaviour
	{
		public abstract void Affect(IPhysics physics);
	}
}