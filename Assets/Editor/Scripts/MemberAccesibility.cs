using System;

namespace Assets.Editor.Scripts
{
	[Serializable]
	public enum MemberAccesibility
	{
		Private = 20480,
		Public = 24576,
		Protected = 12288,
		Internal = 16384,
		InternalProtected = 8192
	}
}