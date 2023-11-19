using BookStore.Model;

namespace BookStore.Repositories.QuantityRepository
{
    public interface IQuantityRepository
    {
        void CreateQuantity(int Count);
        Quantity GetQuantity(int Count);
        bool IsSaveChanges();
    }
}
