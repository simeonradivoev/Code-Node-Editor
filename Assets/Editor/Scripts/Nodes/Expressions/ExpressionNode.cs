using System.CodeDom;
using Assets.Editor.Scripts.Slots;

namespace NodeEditor.Nodes.Expressions
{
	[Title("Expressions")]
	public abstract class ExpressionNode<T> : CodeNode<T> where T : CodeExpression, new()
	{
		protected readonly GetterSlot<T> m_Output;

		protected ExpressionNode(string id,string displayName)
		{
			m_Output = CreateOutputSlot<GetterSlot<T>>(id, displayName).SetGetter(BuildInternal);
		}

		private T BuildInternal()
		{
			T exp = new T();
			Build(exp);
			return exp;
		}

		protected abstract void Build(T expression);
	}
}