using NodeEditor;

namespace Assets.Editor.Scripts.Slots
{
	public class EmptySlot<T> : NodeSlot
	{
		public override SerializedType valueType => typeof(T);

		public override void CopyValuesFrom(NodeSlot foundSlot)
		{
		}
	}
}