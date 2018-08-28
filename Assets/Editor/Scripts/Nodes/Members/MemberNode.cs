using System.CodeDom;
using Assets.Editor.Scripts;
using Assets.Editor.Scripts.Controls;
using Assets.Editor.Scripts.Slots;
using NodeEditor.Util;
using UnityEngine;

namespace NodeEditor.Nodes
{
	[Title("Members")]
	public abstract class MemberNode<T> : CodeNode<T> where T : CodeTypeMember, new()
	{
		[SerializeField] private string m_MemberName;
		[SerializeField] private MemberAccesibility m_Accecability = MemberAccesibility.Private;
		[SerializeField] private bool m_Static;

		[TextFieldControl]
		public string memberName { get { return m_MemberName; } set { m_MemberName = value; } }

		[EnumControl(label = "Accesability")]
		public MemberAccesibility accesibility { get { return m_Accecability; } set { m_Accecability = value; } }

		[ToggleControl(label = "Static")]
		public bool staticMember { get { return m_Static; } set { m_Static = value; } }

		public MemberNode()
		{
			CreateOutputSlot<GetterSlot<T>>("out","Out (mem)").SetGetter(BuildInternal);
		}

		protected override bool CalculateNodeHasError(ref string error)
		{
			if (NameHelper.IsNameInvalid(m_MemberName, ref error)) return true;
			return false;
		}

		private T BuildInternal()
		{
			T member = new T();
			member.Name = m_MemberName;
			member.Attributes |= (MemberAttributes)m_Accecability;
			if (m_Static)
				member.Attributes |= MemberAttributes.Static;
			Build(member);
			return member;
		}

		protected abstract void Build(T member);
	}
}