using System.CodeDom;
using Assets.Editor.Scripts.Controls;
using UnityEngine;

namespace NodeEditor.Nodes.Statements
{
	[Title("Statement: Comment")]
	public class CommentStatementNode : StatementNode<CodeCommentStatement>
	{
		[SerializeField] private string m_Comment;
		[SerializeField] private bool m_DocComment;

		[TextFieldControl(multiline = true)]
		public string comment { get { return m_Comment; } set { m_Comment = value; } }

		[ToggleControl(label = "Document Comment")]
		public bool docComment { get { return m_DocComment; } set { m_DocComment = value; } }

		public CommentStatementNode() : base("comment", "Comment (stm)")
		{
			name = "Comment";
		}

		protected override void Build(CodeCommentStatement com)
		{
			com.Comment = new CodeComment(m_Comment, m_DocComment);
		}
	}
}