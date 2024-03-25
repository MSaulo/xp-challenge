namespace XPChallenge.Contracts {
    public interface IBaseRepository<T> {
        Task<T?> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> CreateAsync(T newDocument);
        Task<T> UpdateAsync(string id, T document);
        Task<string?> DeleteAsync(string id);
    }
}
