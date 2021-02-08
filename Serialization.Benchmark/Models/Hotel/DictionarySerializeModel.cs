using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.Benchmark.Models.Hotel
{

    public class DictionarySerializeModel<TKey, TValue> : ISerializeableCollection, ICreateableSerializeableCollection<DictionarySerializeModel<TKey, TValue>>
    {

        public Dictionary<TKey, TValue> Items { get; set; }

        public DictionarySerializeModel()
        {

        }

        public DictionarySerializeModel<TKey, TValue> Create(int size)
        {
            var f = new Fixture() { RepeatCount = 1 };
            f.Customizations.Add(new StringGenerator(() => Guid.NewGuid().ToString().Substring(0, 6)));

            var createdObject = new DictionarySerializeModel<TKey, TValue>()
            {
                Items = f.CreateMany<KeyValuePair<TKey, TValue>>(size).GroupBy(c=>c.Key).ToDictionary(x => x.Key, x => x.First().Value)
            };

            return createdObject;
        }




    }
}
