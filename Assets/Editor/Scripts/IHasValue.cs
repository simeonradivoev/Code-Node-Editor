namespace Assets.Editor.Scripts
{
	public interface IHasValue<out T>
	{
		T value { get; }
	}
}