﻿// ReSharper disable all

#pragma warning disable

// ------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン: 15.0.0.0
//
//     このファイルへの変更は、正しくない動作の原因になる可能性があり、
//     コードが再生成されると失われます。
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Utf8Json.CodeGenerator.Generator
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;

    /// <summary>
    /// Class to produce the template output
    /// </summary>
#line 1 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class FormatterTemplate : FormatterTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("#pragma warning disable 618\r\n#pragma warning disable 612\r\n#pragma warning disable" +
                       " 414\r\n#pragma warning disable 219\r\n#pragma warning disable 168\r\n\r\nnamespace ");

#line 12 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));

#line default
#line hidden
            this.Write("\r\n{\r\n    using System;\r\n    using Utf8Json;\r\n\r\n");

#line 17 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
            foreach (var objInfo in objectSerializationInfos)
            {
#line default
#line hidden
                this.Write("\r\n    public sealed class ");

#line 19 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.Name));

#line default
#line hidden
                this.Write("Formatter : global::Utf8Json.IJsonFormatter<");

#line 19 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.FullName));

#line default
#line hidden
                this.Write(">\r\n    {\r\n        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMa" +
                           "pping;\r\n        readonly byte[][] ____stringByteKeys;\r\n\r\n        public ");

#line 24 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.Name));

#line default
#line hidden
                this.Write("Formatter()\r\n        {\r\n            this.____keyMapping = new global::Utf8Json.In" +
                           "ternal.AutomataDictionary()\r\n            {\r\n");

#line 28 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                var index = 0;
                foreach (var x in objInfo.Members)
                {
#line default
#line hidden
                    this.Write("                { JsonWriter.GetEncodedPropertyNameWithoutQuotation(\"");

#line 29 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                    this.Write(this.ToStringHelper.ToStringWithCulture(x.Name));

#line default
#line hidden
                    this.Write("\"), ");

#line 29 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                    this.Write(this.ToStringHelper.ToStringWithCulture(index++));

#line default
#line hidden
                    this.Write("},\r\n");

#line 30 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                }

#line default
#line hidden
                this.Write("            };\r\n\r\n            this.____stringByteKeys = new byte[][]\r\n           " +
                           " {\r\n");

#line 35 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                index = 0;
                foreach (var x in objInfo.Members.Where(x => x.IsReadable))
                {
#line default
#line hidden

#line 36 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                    if (index++ == 0)
                    {
#line default
#line hidden
                        this.Write("                JsonWriter.GetEncodedPropertyNameWithBeginObject(\"");

#line 37 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                        this.Write(this.ToStringHelper.ToStringWithCulture(x.Name));

#line default
#line hidden
                        this.Write("\"),\r\n");

#line 38 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                    }
                    else
                    {
#line default
#line hidden
                        this.Write("                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator(\"");

#line 39 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                        this.Write(this.ToStringHelper.ToStringWithCulture(x.Name));

#line default
#line hidden
                        this.Write("\"),\r\n");

#line 40 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                    }
                }

#line default
#line hidden
                this.Write("                \r\n            };\r\n        }\r\n\r\n        public void Serialize(ref " +
                           "JsonWriter writer, ");

#line 44 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.FullName));

#line default
#line hidden
                this.Write(" value, global::Utf8Json.IJsonFormatterResolver formatterResolver)\r\n        {\r\n");

#line 46 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                if (objInfo.IsClass)
                {
#line default
#line hidden
                    this.Write("            if (value == null)\r\n            {\r\n                writer.WriteNull()" +
                               ";\r\n                return;\r\n            }\r\n");

#line 52 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                }

#line default
#line hidden
                this.Write("            \r\n\r\n");

#line 54 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                index = 0;
                foreach (var x in objInfo.Members.Where(x => x.IsReadable))
                {
#line default
#line hidden
                    this.Write("            writer.WriteRaw(this.____stringByteKeys[");

#line 55 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                    this.Write(this.ToStringHelper.ToStringWithCulture(index++));

#line default
#line hidden
                    this.Write("]);\r\n            ");

#line 56 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                    this.Write(this.ToStringHelper.ToStringWithCulture(x.GetSerializeMethodString()));

#line default
#line hidden
                    this.Write(";\r\n");

#line 57 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                }

#line default
#line hidden
                this.Write("            \r\n            writer.WriteEndObject();\r\n        }\r\n\r\n        public ");

#line 62 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.FullName));

#line default
#line hidden
                this.Write(" Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver forma" +
                           "tterResolver)\r\n        {\r\n            if (reader.ReadIsNull())\r\n            {\r\n");

#line 66 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                if (objInfo.IsClass)
                {
#line default
#line hidden
                    this.Write("                return null;\r\n");

#line 68 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                }
                else
                {
#line default
#line hidden
                    this.Write("                throw new InvalidOperationException(\"typecode is null, struct not" +
                               " supported\");\r\n");

#line 70 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                }

#line default
#line hidden
                this.Write("            }\r\n            \r\n");

#line 73 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                if (!objInfo.HasConstructor)
                {
#line default
#line hidden
                    this.Write("            \r\n\t        throw new InvalidOperationException(\"generated serializer " +
                               "for IInterface does not support deserialize.\");\r\n");

#line 75 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                }
                else
                {
#line default
#line hidden
                    this.Write("\r\n");

#line 77 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                    foreach (var x in objInfo.Members)
                    {
#line default
#line hidden
                        this.Write("            var __");

#line 78 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                        this.Write(this.ToStringHelper.ToStringWithCulture(x.MemberName));

#line default
#line hidden
                        this.Write("__ = default(");

#line 78 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                        this.Write(this.ToStringHelper.ToStringWithCulture(x.Type));

#line default
#line hidden
                        this.Write(");\r\n            var __");

#line 79 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                        this.Write(this.ToStringHelper.ToStringWithCulture(x.MemberName));

#line default
#line hidden
                        this.Write("__b__ = false;\r\n");

#line 80 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                    }

#line default
#line hidden
                    this.Write(
                        "\n            var ____count = 0;\n            reader.ReadIsBeginObjectWithVerify();\n            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))\n            {\n                var stringKey = reader.ReadPropertyNameSegmentRaw();\n                int key;\n                if (!____keyMapping.TryGetValueSafe(stringKey, out key))\n                {\n                    reader.ReadNextBlock();\n                    goto NEXT_LOOP;\n                }\n\n                switch (key)\n                {\n");

#line 96 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                    index = 0;
                    foreach (var x in objInfo.Members)
                    {
#line default
#line hidden
                        this.Write("                    case ");

#line 97 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                        this.Write(this.ToStringHelper.ToStringWithCulture(index++));

#line default
#line hidden
                        this.Write(":\r\n                        __");

#line 98 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                        this.Write(this.ToStringHelper.ToStringWithCulture(x.MemberName));

#line default
#line hidden
                        this.Write("__ = ");

#line 98 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                        this.Write(this.ToStringHelper.ToStringWithCulture(x.GetDeserializeMethodString()));

#line default
#line hidden
                        this.Write(";\r\n                        __");

#line 99 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                        this.Write(this.ToStringHelper.ToStringWithCulture(x.MemberName));

#line default
#line hidden
                        this.Write("__b__ = true;\r\n                        break;\r\n");

#line 101 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                    }

#line default
#line hidden
                    this.Write("                    default:\r\n                        reader.ReadNextBlock();\r\n  " +
                               "                      break;\r\n                }\r\n\r\n                NEXT_LOOP:\r\n " +
                               "               continue;\r\n            }\r\n\r\n            var ____result = new ");

#line 111 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                    this.Write(this.ToStringHelper.ToStringWithCulture(objInfo.GetConstructorString()));

#line default
#line hidden
                    this.Write(";\r\n");

#line 112 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                    foreach (var x in objInfo.Members.Where(x => x.IsWritable))
                    {
#line default
#line hidden
                        this.Write("            if(__");

#line 113 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                        this.Write(this.ToStringHelper.ToStringWithCulture(x.MemberName));

#line default
#line hidden
                        this.Write("__b__) ____result.");

#line 113 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                        this.Write(this.ToStringHelper.ToStringWithCulture(x.MemberName));

#line default
#line hidden
                        this.Write(" = __");

#line 113 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                        this.Write(this.ToStringHelper.ToStringWithCulture(x.MemberName));

#line default
#line hidden
                        this.Write("__;\r\n");

#line 114 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                    }

#line default
#line hidden
                    this.Write("\r\n            return ____result;\r\n");

#line 117 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
                }

#line default
#line hidden
                this.Write("        }\r\n    }\r\n\r\n");

#line 121 "C:\Users\y.kawai\Documents\neuecc\Utf8Json\src\Utf8Json.CodeGenerator\Generator\FormatterTemplate.tt"
            }

#line default
#line hidden
            this.Write("}\r\n\r\n#pragma warning disable 168\r\n#pragma warning restore 219\r\n#pragma warning re" +
                       "store 414\r\n#pragma warning restore 618\r\n#pragma warning restore 612");
            return this.GenerationEnvironment.ToString();
        }
    }

#line default
#line hidden

    #region Base class

    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public class FormatterTemplateBase
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
