using System;
using System.Runtime.Serialization;
using TgFramework.Core;

namespace TgFramework.Exceptions
{
    public class GridDefinitionParseException : Exception
    {
        public GridDefinitionParseException(string definition, string reason)
            : base(GetMessage(definition, reason))
        {
        }

        public GridDefinitionParseException(string definition, string reason, Exception innerException)
            : base(GetMessage(definition, reason), innerException)
        {
        }

        public GridDefinitionParseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static string GetMessage(string definition, string reason)
        {
            return string.Format("Unable to parse the following definition: '{0}'. Reason: '{1}'",
                definition.ToStringNN(), reason.ToStringNN());
        }
    }
}