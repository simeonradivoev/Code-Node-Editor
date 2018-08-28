using System.CodeDom;
using Assets.Editor.Scripts.Controls;
using Assets.Editor.Scripts.Slots;
using NodeEditor.Util;
using UnityEngine;

namespace NodeEditor.Nodes
{
	[Title("Declarations","Declare: Parameter")]
	public class ParameterNode : AbstractNode
	{
		[SerializeField] private string m_ParamName;
		[SerializeField] private FieldDirection m_Direction;

		[TextFieldControl]
		public string paramName
		{
			get { return m_ParamName; }
			set { m_ParamName = value; }
		}

		[EnumControl]
		public FieldDirection direction
		{
			get { return m_Direction; }
			set { m_Direction = value; }
		}

		private CodeTypeReferenceSlot m_Type;

		public ParameterNode()
		{
			name = "Parameter Declaration";
			CreateOutputSlot<GetterSlot<CodeParameterDeclarationExpression>>("out","Out (dec exp)").SetGetter(Build);
			m_Type = CreateInputSlot<CodeTypeReferenceSlot>("type","Type (ref)");
		}

		protected override bool CalculateNodeHasError(ref string error)
		{
			if (NameHelper.IsNameInvalid(m_ParamName, ref error)) return true;
			return false;
		}

		private CodeParameterDeclarationExpression Build()
		{
			var param = new CodeParameterDeclarationExpression();

			param.Type = GetSlotValue<CodeTypeReference>(m_Type);
			param.Name = m_ParamName;
			return param;
		}
	}
}