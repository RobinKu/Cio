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
using System.Reflection;

namespace Cio.UI
{
    public abstract class FormBuilder : BindableBlockBuilder, IFormBuilder
    {
        private readonly IElementResolver resolver;

        public FormBuilder(IElementResolver resolver)
        {
            if (resolver == null)
            {
                throw new ArgumentNullException("resolver");
            }

            this.resolver = resolver;
        }

        protected IElementResolver Resolver
        {
            get
            {
                return this.resolver;
            }
        }

        protected FormResult CreateBindingResult(BindingInformation info)
        {
            Type sourceType = info.SourceType;
            string bindingPath = info.BindingPath;
            string rendermode = info.RenderMode;

            object labelElement = this.CreateLabel();
            object editorElement = this.CreateEditor(sourceType, bindingPath, rendermode);

            return new FormResult(labelElement, editorElement);
        }

        protected object CreateLabel()
        {
            IElementFactory labelFactory = this.Resolver.ResolveEditor<string>(RenderModes.EditorLabel);
            object labelElement = labelFactory.CreateElement(RenderModes.EditorLabel);

            return labelElement;
        }

        protected object CreateEditor(Type sourceType, string bindingPath, string rendermode)
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

            IElementFactory editorFactory = this.Resolver.ResolveEditor(property.PropertyType, rendermode);
            object editorElement = editorFactory.CreateElement(sourceType, bindingPath, rendermode);

            return editorElement;
        }
    }
}
