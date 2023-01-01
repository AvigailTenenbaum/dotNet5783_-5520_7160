namespace DO
{
    /// <summary>
    /// Exception for entity not found or missing identifier
    /// </summary>
    public class NotExist : Exception
    {
        public override string Message => "ERROR: No matching entity was found in the list";
        public override string ToString()
        {
            return Message;
        }

    }
    /// <summary>
    /// Exception of duplicate ID
    /// </summary>
    public class AllReadyExist : Exception
    {
        public override string Message => "ERROR: the id is allready exist in the list";
        public override string ToString()
        {
            return Message;
        }
    }
    [Serializable]
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }

}