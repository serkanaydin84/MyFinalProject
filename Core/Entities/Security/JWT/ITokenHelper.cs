using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;

namespace Core.Entities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
