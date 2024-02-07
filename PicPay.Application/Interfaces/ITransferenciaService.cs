using PicPay.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Application.Interfaces
{
    public interface ITransferenciaService
    {
        Task<TransferenciaModel> Pix(string remetente, string destinatario, decimal valor, TransferenciaModel transferenciaModel);
        Task<UsuarioModel> GetByIdentificador(string identificador);
        Task<IEnumerable<TransferenciaModel>> GetAllTransferencias(string usuario);
        Task<bool> EstornarPix(Guid idTransacao);

    }
}
