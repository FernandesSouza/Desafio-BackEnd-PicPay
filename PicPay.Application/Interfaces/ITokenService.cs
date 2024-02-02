using PicPay.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Application.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(string email, string password);
    }
}
