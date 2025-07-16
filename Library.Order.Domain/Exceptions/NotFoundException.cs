using Library.Order.Domain.Exceptions;
using System;

namespace Library.Order.Domain.Exceptions
{
    public class NotFoundException : ApplicationException { public NotFoundException(string message) : base(message) { } }
}