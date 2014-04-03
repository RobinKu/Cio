using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cio
{
    internal class FactoryWrapper : IFactory<object>
    {
        private readonly IFactory factory;

        internal FactoryWrapper(IFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }

            this.factory = factory;
        }

        public object Create()
        {
            return this.factory.Create();
        }
    }
}
