using System.CodeDom;
using Assets.Editor.Scripts.Controls;
using UnityEngine;

namespace NodeEditor.Nodes.Expressions
{
	[Title("Expression: Snippet")]
	public class SnippetExpressionNode : ExpressionNode<CodeSnippetExpression>
	{
		[SerializeField] private string m_Snippet;

		[TextFieldControl(multiline = true)]
		public string snippet { get { return m_Snippet; } set { m_Snippet = value; } }

		public SnippetExpressionNode() : base("out", "Snippet (exp)")
		{
		}

		protected override void Build(CodeSnippetExpression expression)
		{
			expression.Value = m_Snippet;
		}
	}
}