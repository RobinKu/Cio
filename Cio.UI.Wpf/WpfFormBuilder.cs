/*
 * Cio
 * Copyright (C) 2013 Robin Kuijstermans
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see [http://www.gnu.org/licenses/].
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Cio.UI.Wpf
{
    public class WpfFormBuilder : FormBuilder
    {
        public WpfFormBuilder(IElementResolver resolver)
            : base(resolver)
        {
        }

        public override object CreateBlock()
        {
            ItemsControl panel = new ItemsControl();

            FrameworkElementFactory frameworkElementFactory = new FrameworkElementFactory(typeof(ColumnWrapPanel));
            panel.ItemsPanel = new ItemsPanelTemplate(frameworkElementFactory);

            return panel;
<<<<<<< HEAD
		}
		
		public object Add(object form, object source, string bindingPath, string rendermode, params object[] services)
		{
			if (form == null)
			{
				throw new ArgumentNullException("form");
			}
			else if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			
			ItemsControl panel = form as ItemsControl;
			
			if (panel == null)
			{
				throw new ArgumentException("form must derive from ItemsControl. Use the CreateForm() method of the formbuilder to create the right type.");
			}
			
			PropertyInfo property = BindingPathUtility.GetProperty(source, bindingPath);
			
			IElementFactory labelFactory = this.resolver.Resolve<string>(RenderModes.EditorLabel);
			object labelElement = labelFactory.CreateElement(RenderModes.EditorLabel);
			
			IElementFactory editorFactory = this.resolver.Resolve(property.PropertyType, rendermode);
			object editorElement = editorFactory.CreateElement(source, bindingPath, rendermode);
			
			foreach (IServiceVisitor visitor in ServiceVisitors)
			{
				visitor.Visit(labelElement, editorElement, source, bindingPath, rendermode, services.Concat(this.Services));
			}
			
			panel.Items.Add(labelElement);
			panel.Items.Add(editorElement);
			
			return editorElement;
		}
	}
=======
        }

        public override IResult Add(BindingInformation info)
        {
            ItemsControl panel = this.ValidateAsBaseClass<ItemsControl>(info.AddTo);

            FormResult result = this.CreateBindingResult(info);

            panel.Items.Add(result.LabelElement);
            panel.Items.Add(result.EditorElement);

            OnAdded(info, result);

            return result;
        }

        public override void Bind(object block, object bindableOBject)
        {
            if (block == null)
            {
                throw new ArgumentNullException("block");
            }

            ItemsControl panel = ValidateAsBaseClass<ItemsControl>(block);

            panel.DataContext = bindableOBject;
        }
    }
>>>>>>> origin/grid
}
