using System;

namespace NodeEditor
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue,AllowMultiple = true)]
	public class TitleAttribute : Attribute
	{
		public string[] title;
		public TitleAttribute(params string[] title) { this.title = title; }
	}
}