// ReSharper disable all

#pragma warning disable

namespace Utf8Json.CodeGenerator.Generator
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;

    /// <summary>
    /// Class to produce the template output
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    internal partial class ResolverTemplate : ResolverTemplateBase
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("#pragma warning disable 618\r\n#pragma warning disable 612\r\n#pragma warning disable" +
                       " 414\r\n#pragma warning disable 168\r\n\r\nnamespace ");

            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));

            this.Write("\r\n{\r\n    using System;\r\n    using Utf8Json;\r\n\r\n    public class ");

            this.Write(this.ToStringHelper.ToStringWithCulture(ResolverName));

            this.Write(" : global::Utf8Json.IJsonFormatterResolver\r\n    {\r\n        public static readonly" +
                       " global::Utf8Json.IJsonFormatterResolver Instance = new ");

            this.Write(this.ToStringHelper.ToStringWithCulture(ResolverName));

            this.Write("();\r\n\r\n        ");

            this.Write("private readonly System.Collections.Generic.Dictionary<global::System.Type, global::Utf8Json.IJsonFormatter> m_formatters = new System.Collections.Generic.Dictionary<global::System.Type, global::Utf8Json.IJsonFormatter>();\r\n\r\n        ");

            this.Write(this.ToStringHelper.ToStringWithCulture(ResolverName));

            this.Write("()\r\n        {\r\n\r\n        }");

            this.Write($@"
        public global::Utf8Json.IJsonFormatter GetFormatter(global::System.Type type)
        {{
            if (!m_formatters.TryGetValue(type, out var formatter))
            {{
                formatter = (global::Utf8Json.IJsonFormatter){this.ToStringHelper.ToStringWithCulture(ResolverName)}GetFormatterHelper.GetFormatter(type);

                m_formatters.Add(type, formatter);
            }}

            return formatter;
        }}");

            this.Write("\r\n\r\n        public global::Utf8Json.IJsonFormatter<T> GetFormatter<T>()\r\n" +
                       "        {\r\n            return FormatterCache<T>.formatter;\r\n        }\r\n\r\n        static class FormatterCache<T>\r\n" +
                       "        {\r\n            public static readonly global::Utf8Json.IJsonFormatter<T> formatter;\r\n\r\n            static FormatterCache()\r\n" +
                       "            {\r\n                var f = ");

            this.Write(this.ToStringHelper.ToStringWithCulture(ResolverName));

            this.Write("GetFormatterHelper.GetFormatter(typeof(T));\r\n                if (f != null)\r\n    " +
                       "            {\r\n                    formatter = (global::Utf8Json.IJsonFormatter<" +
                       "T>)f;\r\n                }\r\n            }\r\n        }\r\n    }\r\n\r\n    internal static" +
                       " class ");

            this.Write(this.ToStringHelper.ToStringWithCulture(ResolverName));

            this.Write("GetFormatterHelper\r\n    {\r\n        static readonly global::System.Collections.Gen" +
                       "eric.Dictionary<Type, int> lookup;\r\n\r\n        static ");

            this.Write(this.ToStringHelper.ToStringWithCulture(ResolverName));

            this.Write("GetFormatterHelper()\r\n        {\r\n            lookup = new global::System.Collecti" +
                       "ons.Generic.Dictionary<Type, int>(");

            this.Write(this.ToStringHelper.ToStringWithCulture(registerInfos.Length));

            this.Write(")\r\n            {\r\n");

            for (var i = 0; i < registerInfos.Length; i++)
            {
                var x = registerInfos[i];

                this.Write("                {typeof(");

                this.Write(this.ToStringHelper.ToStringWithCulture(x.FullName));

                this.Write("), ");

                this.Write(this.ToStringHelper.ToStringWithCulture(i));

                this.Write(" },\r\n");
            }

            this.Write("            };\r\n        }\r\n\r\n        internal static object GetFormatter(Type t)\r" +
                       "\n        {\r\n            int key;\r\n            if (!lookup.TryGetValue(t, out key" +
                       ")) return null;\r\n\r\n            switch (key)\r\n            {\r\n");

            for (var i = 0; i < registerInfos.Length; i++)
            {
                var x = registerInfos[i];

                this.Write("                case ");

                this.Write(this.ToStringHelper.ToStringWithCulture(i));

                this.Write(": return new ");

                this.Write(this.ToStringHelper.ToStringWithCulture(x.FormatterName.StartsWith("global::") ? x.FormatterName : (!string.IsNullOrEmpty(FormatterNamespace) ? FormatterNamespace + "." : FormatterNamespace) + x.FormatterName));

                this.Write("();\r\n");
            }

            this.Write("                default: return null;\r\n            }\r\n        }\r\n    }\r\n}\r\n\r\n#pra" +
                       "gma warning disable 168\r\n#pragma warning restore 414\r\n#pragma warning restore 61" +
                       "8\r\n#pragma warning restore 612");
            return this.GenerationEnvironment.ToString();
        }
    }

    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    internal class ResolverTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion

        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set { this.generationEnvironmentField = value; }
        }

        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }

        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }

        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent { get { return this.currentIndentField; } }

        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session { get { return this.sessionField; } set { this.sessionField = value; } }
        #endregion

        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0)
                 || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }

        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }

        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }

        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }

        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }

        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }

        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }

        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion

        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField = global::System.Globalization.CultureInfo.InvariantCulture;

            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get { return this.formatProviderField; }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField = value;
                    }
                }
            }

            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[]
                {
                    typeof(System.IFormatProvider)
                });
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[]
                    {
                        this.formatProviderField
                    })));
                }
            }
        }

        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();

        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper { get { return this.toStringHelperField; } }
        #endregion
    }
    #endregion
}
