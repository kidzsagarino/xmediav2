using System.Collections.Generic;

namespace BusinessRef.Interfaces.Generics
{
    public interface IGetDatabaseCollectionData<T>
    {
        ICollection<T> GetDatabaseCollectionData();
    }
}
