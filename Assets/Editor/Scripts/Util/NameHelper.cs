using System.CodeDom.Compiler;

namespace NodeEditor.Util
{
	public static class NameHelper
	{
		public static bool IsNameInvalid(string name,ref string error)
		{
			if (string.IsNullOrEmpty(name))
			{
				error = "Empty Name";
				return true;
			}
			if (!CodeGenerator.IsValidLanguageIndependentIdentifier(name))
			{
				error = "Invalid Name";
				return true;
			}
			return false;
		}
	}
}