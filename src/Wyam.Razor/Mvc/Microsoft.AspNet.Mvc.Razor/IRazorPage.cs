﻿﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Wyam.Razor.Microsoft.AspNet.Mvc.Razor
{
    /// <summary>
    /// Represents properties and methods that are used by <see cref="RazorView"/> for execution.
    /// </summary>
    public interface IRazorPage
    {
        /// <summary>
        /// Gets or sets the view context of the renderign view.
        /// </summary>
        ViewContext ViewContext { get; set; }

        /// <summary>
        /// Gets or sets the action invoked to render the body.
        /// </summary>
        Action<TextWriter> RenderBodyDelegate { get; set; }

        /// <summary>
        /// Gets or sets a flag that determines if the layout of this page is being rendered.
        /// </summary>
        /// <remarks>
        /// Sections defined in a page are deferred and executed as part of the layout page.
        /// When this flag is set, all write operations performed by the page are part of a
        /// section being rendered.
        /// </remarks>
        bool IsLayoutBeingRendered { get; set; }

        /// <summary>
        /// Gets the application base relative path to the page.
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// Gets or sets the path of a layout page.
        /// </summary>
        string Layout { get; set; }

        /// <summary>
        /// Gets or sets a value that determines if the current instance of <see cref="IRazorPage"/> is being executed
        /// from a partial view.
        /// </summary>
        bool IsPartial { get; set; }

        /// <summary>
        /// Gets or sets the sections that can be rendered by this page.
        /// </summary>
        IDictionary<string, RenderAsyncDelegate> PreviousSectionWriters { get; set; }

        /// <summary>
        /// Gets the sections that are defined by this page.
        /// </summary>
        IDictionary<string, RenderAsyncDelegate> SectionWriters { get; }

        /// <summary>
        /// Renders the page and writes the output to the <see cref="ViewContext.Writer"/>.
        /// </summary>
        /// <returns>A task representing the result of executing the page.</returns>
        Task ExecuteAsync();

        /// <summary>
        /// Verifies that all sections defined in <see cref="PreviousSectionWriters"/> were rendered, or
        /// the body was rendered if no sections were defined.
        /// </summary>
        /// <exception cref="InvalidOperationException">if one or more sections were not rendered or if no sections were
        /// defined and the body was not rendered.</exception>
        void EnsureRenderedBodyOrSections();

        // These match the default member names in Microsoft.AspNet.Razor.Generator.GeneratedClassContext
        // They must also match the string literals passed into GeneratedClassContext in the Razor module
        void Write(object value);
        void WriteTo(TextWriter writer, object value);
        void WriteLiteral(object value);
        void WriteLiteralTo(TextWriter writer, object value);
        void WriteAttribute(string name, PositionTagged<string> prefix, PositionTagged<string> suffix, params AttributeValue[] values);
        void WriteAttributeTo(TextWriter writer, string name, PositionTagged<string> prefix, PositionTagged<string> suffix, params AttributeValue[] values);
        void BeginContext(int position, int length, bool isLiteral);
        void EndContext();
        void DefineSection(string name, RenderAsyncDelegate section);
        string Href(string contentPath);
    }
}