namespace Serialization.Benchmark.Models.Hotel
{
    public interface ISerializeableCollection
    {
         

        
    }

    public interface ICreateableSerializeableCollection<TCollection>  where TCollection:class
    {

        public TCollection Create(int size);

    }
}
