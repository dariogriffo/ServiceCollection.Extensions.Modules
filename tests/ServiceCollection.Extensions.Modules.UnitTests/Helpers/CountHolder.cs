namespace ServiceCollection.Extensions.Modules.UnitTests.Helpers
{
    internal class CountHolder
    {
        internal CountHolder()
        {
            Count = 0;

        }

        internal int Count { get; set; }

        internal void Increment()
        {
            Count += 1;
        }
    }
}
