using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Exception for entity not found or missing identifier
    /// </summary>
    public class NotExist:Exception
    {
        public override string Message => "ERROR: the entity is not exist in the list";
        public override string ToString()
        {
            return Message;
        }

    }
    /// <summary>
    /// Exception of duplicate ID
    /// </summary>
    public class AllReadyExist:Exception
    {
        public override string Message => "ERROR: the id is allready exist in the list";
        public override string ToString()
        {
            return Message;
        }
    }
}