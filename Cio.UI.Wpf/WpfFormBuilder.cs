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
        }

        public override IResult Add(BindingInformation info)
        {
            ItemsControl panel = this.ValidateAsBaseClass<ItemsControl>(info.AddTo);

            FormResult result = this.AddInternal(info);

            panel.Items.Add(result.LabelElement);
            panel.Items.Add(result.EditorElement);

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
}
