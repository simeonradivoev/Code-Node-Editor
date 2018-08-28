using System;
using NodeEditor;

namespace Assets.Editor.Scripts.Slots
{
	public class GetterSlot<T> : NodeSlot, IHasValue<T>
	{
		private Func<T> m_Getter;

		public T value => m_Getter.Invoke();

		public GetterSlot<T> SetGetter(Func<T> getter)
		{
			m_Getter = getter;
			return this;
		}

		public override SerializedType valueType => typeof(T);

		public override void CopyValuesFrom(NodeSlot foundSlot)
		{
		}
	}
}