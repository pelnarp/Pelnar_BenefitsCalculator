using Api.Models;

namespace Api.DAL
{
    /// <summary>
    /// This is base abstract class for generic entity repository providing Get and GetAll methods.
    /// It implements the shared functionality to populate mocked storage using Dictionary of entities
    /// Other CRUD operations might be added, but are not needed for this task
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MockRepositoryBase<T> : IRepository<T> where T : WithId
    {
        protected Dictionary<int, T> ItemLookup = new();

        protected abstract IList<T> InitialData { get; }

        public MockRepositoryBase()
        {
            this.PopulateMockData();
        }

        public async Task<T?> Get(int id)
        {
            if (ItemLookup.ContainsKey(id))
                return ItemLookup[id];

            return default;
        }

        public async Task<IEnumerable<T?>> GetAll()
        {
            return ItemLookup.Values;
        }

        private void PopulateMockData()
        {
            foreach (var item in InitialData)
            {
                this.ItemLookup.Add(item.Id, item);
            }
        }
    }
}
