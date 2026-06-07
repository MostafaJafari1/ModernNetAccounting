using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Domain.Exceptions;
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message)
    {
    }
}