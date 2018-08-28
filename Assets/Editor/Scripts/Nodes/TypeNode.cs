using System;
using System.CodeDom;
using Assets.Editor.Scripts;
using Assets.Editor.Scripts.Controls;
using Assets.Editor.Scripts.Slots;
using NodeEditor.Util;
using UnityEngine;

namespace NodeEditor.Nodes
{
	[Title("Declarations","Type")]
	public class TypeNode : AbstractNode
	{
		[SerializeField] private string m_TypeName;
		[SerializeField] private TypeType m_Type;
		[SerializeField] private bool m_Partial;

		[TextFieldControl]
		public string typeName
		{
			get { return m_TypeName; }
			set { m_TypeName = value; }
		}

		[EnumControl(label = "Type")]
		public TypeType type { get { return m_Type; } set { m_Type = value; } }

		[ToggleControl(label = "Partial")]
		public bool partial { get { return m_Partial; } set { m_Partial = value; } }

		private GetterSlot<CodeTypeDeclaration> m_TypeBuilder;
		private EmptySlot<CodeTypeMember> m_Members;
		private EmptySlot<CodeTypeReference> m_BaseTypes;
		private EmptySlot<CodeCommentStatement> m_Comments;

		public TypeNode()
		{
			name = "Type Declaration";
			m_TypeBuilder = CreateOutputSlot<GetterSlot<CodeTypeDeclaration>>("type","Type (dec)").SetGetter(Build);
			m_Members = CreateInputSlot<EmptySlot<CodeTypeMember>>("members","Members (mem)");
			m_BaseTypes = CreateInputSlot<EmptySlot<CodeTypeReference>>("baseTypes","Base Types (ref)");
			m_Comments = CreateInputSlot<EmptySlot<CodeCommentStatement>>("comments","Comments (stm)");
			m_Members.allowMultipleConnections = true;
		}

		protected override bool CalculateNodeHasError(ref string error)
		{
			if (NameHelper.IsNameInvalid(m_TypeName, ref error)) return true;
			return false;
		}

		public CodeTypeDeclaration Build()
		{
			var dec = new CodeTypeDeclaration(m_TypeName);

			switch (type)
			{
				case TypeType.Class:
					dec.IsClass = true;
					break;
				case TypeType.Enum:
					dec.IsEnum = true;
					break;
				case TypeType.Interface:
					dec.IsInterface = true;
					break;
				case TypeType.Struct:
					dec.IsStruct = true;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			dec.IsPartial = partial;

			var members = GetSlotValues<CodeTypeMember>(m_Members);
			foreach (var member in members)
			{
				dec.Members.Add(member);
			}

			var baseTypes = GetSlotValues<CodeTypeReference>(m_BaseTypes);
			foreach (var baseType in baseTypes)
			{
				dec.BaseTypes.Add(baseType);
			}

			var comments = GetSlotValues<CodeCommentStatement>(m_Comments);
			foreach (var comment in comments)
			{
				dec.Comments.Add(comment);
			}

			return dec;
		}
	}
}