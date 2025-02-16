﻿using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DapperClassGenerator
{
    public static class Helpers
    {
        private static readonly Regex RxCleanUp = new(@"[^\w\d_]", RegexOptions.Compiled);

        private static readonly string[] ReservedKeywords = { 
            "abstract", "event", "new", "struct", "as", "explicit", "null",
            "switch", "base", "extern", "object", "this", "bool", "false", "operator", "throw",
            "break", "finally", "out", "true", "byte", "fixed", "override", "try", "case", "float",
            "params", "typeof", "catch", "for", "private", "uint", "char", "foreach", "protected",
            "ulong", "checked", "goto", "public", "unchecked", "class", "if", "readonly", "unsafe",
            "const", "implicit", "ref", "ushort", "continue", "in", "return", "using", "decimal",
            "int", "sbyte", "virtual", "default", "interface", "sealed", "volatile", "delegate",
            "internal", "short", "void", "do", "is", "sizeof", "while", "double", "lock",
            "stackalloc", "else", "long", "static", "enum", "namespace", "string" 
        };

        public static readonly Func<string, string> CleanUp = str =>
        {
            if (string.IsNullOrEmpty(str))
                return str;
            
            str = RxCleanUp.Replace(str, "_");

            if (char.IsDigit(str[0]) || ReservedKeywords.Contains(str))
            {
                str = "@" + str;
            }

            return str;
        };

        public static string ConvertPropertyType(string type) => type switch
        {
            "bigint" => "long",
            "smallint" => "short",
            "int" => "int",
            "uniqueidentifier" => "Guid",
            "smalldatetime" => "DateTime",
            "datetime" => "DateTime",
            "datetime2" => "DateTime",
            "date" => "DateTime",
            "time" => "DateTime",
            "datetimeoffset" => "DateTimeOffset",
            "float" => "double",
            "real" => "float",
            "numeric" => "decimal",
            "smallmoney" => "decimal",
            "decimal" => "decimal",
            "money" => "decimal",
            "tinyint" => "byte",
            "bit" => "bool",
            "image" => "byte[]",
            "binary" => "byte[]",
            "varbinary" => "byte[]",
            "timestamp" => "byte[]",
            "geography" => "Microsoft.SqlServer.Types.SqlGeography",
            "geometry" => "Microsoft.SqlServer.Types.SqlGeometry",
            _ => "string"
        };
    }
}
