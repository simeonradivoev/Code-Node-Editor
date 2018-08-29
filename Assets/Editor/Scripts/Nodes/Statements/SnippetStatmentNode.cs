using System.CodeDom;
using Assets.Editor.Scripts.Controls;
using UnityEngine;

namespace NodeEditor.Nodes.Statements
{
	[Title("Statement: Snippet")]
	public class SnippetStatmentNode : StatementNode<CodeSnippetStatement>
	{
		[SerializeField] private string m_Snippet;

		[TextFieldControl(multiline = true)]
		public string snippet { get { return m_Snippet; } set { m_Snippet = value; } }

		public SnippetStatmentNode() : base("out", "Snippet (stm)")
		{
		}

		protected override void Build(CodeSnippetStatement statement)
		{
			statement.Value = m_Snippet;
		}
	}
}