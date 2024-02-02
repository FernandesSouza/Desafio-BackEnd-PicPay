using Microsoft.EntityFrameworkCore;
using PicPay.Domain.Interfaces;
using PicPay.Infra.Data.Data;
using System.Linq.Expressions;


namespace PicPay.Infra.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly BancoContext _context;

        public BaseRepository(BancoContext context)
        {
            _context = context;
        }
        public async Task<bool> Autorizador()
        {
            var url = "https://run.mocky.io/v3/5794d450-d2e2-4412-8131-73d0293ac1cc";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();

                        bool autorizado = json.Contains("Autorizado");
                        return autorizado;
                    }
                    else
                    {
                        Console.WriteLine($"Erro: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }

                return false;
            }

        }
    
        public async Task Cadastro(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
           await _context.SaveChangesAsync();
        }
        public async Task<bool> Existe(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }
        public async Task<IEnumerable<T>> VerTodos()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}
