using Assets.Editor.Scripts;

namespace NodeEditor.Nodes
{
	public class DefaultValueSlot<T> : NodeSlot,IHasValue<T>
	{
		public T value { get; set; }

		public DefaultValueSlot<T> SetValue(T val)
		{
			value = val;
			return this;
		}

		public override SerializedType valueType => typeof(T);

		public override void CopyValuesFrom(NodeSlot foundSlot)
		{
		}
	}
}