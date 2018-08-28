using System.CodeDom;
using Assets.Editor.Scripts.Controls;
using Assets.Editor.Scripts.Slots;
using UnityEngine;

namespace NodeEditor.Nodes
{
	[Title("References","Reference: Type")]
	public class TypePropertyNode : AbstractNode
	{
		[SerializeField] private string m_Type;

		[TypeControl]
		private string type {
			get { return m_Type; }
			set { m_Type = value; } 
		}

		public TypePropertyNode()
		{
			CreateOutputSlot<GetterSlot<CodeTypeReference>>("type","Type (ref)").SetGetter(Build);
		}

		private CodeTypeReference Build()
		{
			return new CodeTypeReference(m_Type);
		}
	}
}