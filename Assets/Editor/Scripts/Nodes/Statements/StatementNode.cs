using System.CodeDom;
using Assets.Editor.Scripts.Slots;

namespace NodeEditor.Nodes.Statements
{
	[Title("Statements")]
	public abstract class StatementNode<T> : CodeNode<T> where T : CodeStatement, new()
	{
		protected StatementNode(string id,string distplayName)
		{
			CreateOutputSlot<GetterSlot<T>>(id,distplayName).SetGetter(BuildInternal);
		}

		private T BuildInternal()
		{
			T statement = new T();
			Build(statement);
			return statement;
		}

		protected abstract void Build(T statement);
	}
}