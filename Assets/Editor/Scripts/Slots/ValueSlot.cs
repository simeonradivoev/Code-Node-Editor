using NodeEditor;
using UnityEngine;

namespace Assets.Editor.Scripts.Slots
{
	public class ValueSlot<T> : NodeSlot, IHasValue<T>
	{
		[SerializeField] private T m_Value;

		public T value => m_Value;

		public ValueSlot<T> SetValue(T val)
		{
			m_Value = val;
			return this;
		}

		public override SerializedType valueType => typeof(T);

		public override void CopyValuesFrom(NodeSlot foundSlot)
		{
		}
	}
}