using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Common.Errors;
public enum ErrorType
{
    Failure = 0,  
    Validation = 1, 
    NotFound = 2,   
    Unauthorized = 3, 
    Conflict = 4
}