namespace resunet.Exceptions
{
    public class DuplicateEmailException : Exception { 
    
        public DuplicateEmailException() : base() { }

        public DuplicateEmailException(string message) : base(message) { }
    }
}
