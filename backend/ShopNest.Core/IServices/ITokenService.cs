using ShopNest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNest.Core.IServices
{
    public interface ITokenService
    {
        string CreateAccessToken(User user);
        string CreateRefreshToken();
    }
}
