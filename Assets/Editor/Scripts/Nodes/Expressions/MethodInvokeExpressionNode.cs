using System.CodeDom;
using Assets.Editor.Scripts.Slots;

namespace NodeEditor.Nodes.Expressions
{
	[Title("Method Invoke")]
	public class MethodInvokeExpressionNode : ExpressionNode<CodeMethodInvokeExpression>
	{
		private EmptySlot<CodeMethodReferenceExpression> m_Method;
		private EmptySlot<CodeExpression> m_Parameters;

		public MethodInvokeExpressionNode() : base("out", "Out (ref exp)")
		{
			name = "Method Invoke";
			m_Method = CreateInputSlot<EmptySlot<CodeMethodReferenceExpression>>("method","Method (ref exp)");
			m_Parameters = CreateInputSlot<EmptySlot<CodeExpression>>("params","Params (exp)");
			m_Parameters.allowMultipleConnections = true;
		}

		protected override void Build(CodeMethodInvokeExpression invoke)
		{
			invoke.Method = GetSlotValue<CodeMethodReferenceExpression>(m_Method);

			var parameters = GetSlotValues<CodeExpression>(m_Parameters);
			foreach (var parameter in parameters)
			{
				invoke.Parameters.Add(parameter);
			}
		}
	}
}