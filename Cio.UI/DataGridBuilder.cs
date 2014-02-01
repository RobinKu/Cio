using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cio.UI
{
    public abstract class DataGridBuilder : BindableBlockBuilder, IDataGridBuilder
    {
        private readonly IColumnElementResolver resolver;

        public DataGridBuilder(IColumnElementResolver resolver)
        {
            if(resolver == null)
            {
                throw new ArgumentNullException("resolver");
            }

            this.resolver = resolver;
        }

        protected IColumnElementResolver Resolver
        {
            get
            {
                return this.resolver;
            }
        }

        protected ColumnResult CreateColumnResult(BindingInformation info)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            object columnElement = this.CreateColumn(info.SourceType, info.BindingPath, info.RenderMode);

            return new ColumnResult(columnElement);
        }

        protected object CreateColumn(Type sourceType, string bindingPath, string rendermode)
        {
            if (sourceType == null)
            {
                throw new ArgumentNullException("sourceType");
            }
            else if (string.IsNullOrWhiteSpace(bindingPath))
            {
                throw new ArgumentNullException("bindingPath");
            }

            PropertyInfo property = BindingPathUtility.GetProperty(sourceType, bindingPath);

            IColumnElementFactory columnFactory = this.Resolver.ResolveColumn(property.PropertyType, rendermode);
            object columnElement = columnFactory.CreateColumn(sourceType, bindingPath, rendermode);

            return columnElement;
        }
    }
}
