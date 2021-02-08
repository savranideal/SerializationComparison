using AutoFixture;
using System;
using System.Collections.Generic;

namespace Serialization.Benchmark.Models.Hotel
{
    public class EnumerableSerializeModel<TModel> : ISerializeableCollection,ICreateableSerializeableCollection<EnumerableSerializeModel<TModel>>
    {

        public IEnumerable<TModel> Items { get; set; }

        public EnumerableSerializeModel()
        {

        }

        public EnumerableSerializeModel<TModel> Create(int size)
        {
            var f = new Fixture() { RepeatCount = 1 };
            f.Customizations.Add(new StringGenerator(() => Guid.NewGuid().ToString().Substring(0, 2)));

            var createdObject = new EnumerableSerializeModel<TModel>()
            {
                Items = f.CreateMany<TModel>(size)
            };
            return createdObject;
        }
    }
}
