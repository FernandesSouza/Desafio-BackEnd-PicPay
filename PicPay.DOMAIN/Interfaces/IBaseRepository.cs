using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Domain.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task Cadastro(T entity);
        Task<bool> Existe(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> VerTodos();
        Task<bool> Autorizador();
    }
}
