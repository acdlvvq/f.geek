namespace fgeek.Exceptions
{
    public class FieldAlreadyTakenException : Exception
    {
        public FieldAlreadyTakenException() { }

        public FieldAlreadyTakenException(string message) 
            : base(message) { }

        public FieldAlreadyTakenException(string message, Exception inner)
            : base(message, inner) { }
    }
}
