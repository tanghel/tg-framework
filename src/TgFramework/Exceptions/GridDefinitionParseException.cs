using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgFramework.Core;

namespace TgFramework.Exceptions
{
    public class GridDefinitionParseException : Exception
    {
        private static string GetMessage(string definition, string reason)
        {
            return string.Format("Unable to parse the following definition: '{0}'. Reason: '{1}'", definition.ToStringNN(), reason.ToStringNN());
        }

        public GridDefinitionParseException(string definition, string reason)
            : base(GetMessage(definition, reason))
        {

        }

        public GridDefinitionParseException(string definition, string reason, Exception innerException)
            : base(GetMessage(definition, reason), innerException)
        {

        }

        public GridDefinitionParseException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }
    }
}
