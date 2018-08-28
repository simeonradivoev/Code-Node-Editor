using System.CodeDom;
using Assets.Editor.Scripts.Slots;

namespace NodeEditor.Nodes.Expressions
{
	[Title("References", "Expression: This Reference")]
	public class ThisReferenceExpressionNode : ExpressionNode<CodeThisReferenceExpression>
	{
		public ThisReferenceExpressionNode() : base("out", "This (ref exp)")
		{
			name = "This Reference";
		}

		protected override void Build(CodeThisReferenceExpression reference)
		{
			
		}
	}
}