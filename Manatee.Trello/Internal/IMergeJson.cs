namespace Manatee.Trello.Internal
{
	internal interface IMergeJson<in T>
	{
		void Merge(T json, bool overwrite);
	}
}