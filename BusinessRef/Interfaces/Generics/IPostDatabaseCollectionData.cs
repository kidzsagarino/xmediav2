using System.Collections.Generic;

namespace BusinessRef.Interfaces.Generics
{
    public interface IPostDatabaseCollectionData<T>
    {
        ICollection<T> PostDatabaseCollectionData();
    }
}
