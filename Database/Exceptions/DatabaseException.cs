using System;
using System.Runtime.Serialization;

namespace CRM_PricingBooks.Database
{
    public class DatabaseException : Exception
    {
        public int Code { get { return 404; } }
        public DatabaseException(string message) : base(message)
        {
        }
    }
}