using Microsoft.EntityFrameworkCore;
using PicPay.Application.Interfaces;
using PicPay.Domain.Interfaces;
using PicPay.Domain.Models;
using PicPay.Infra.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Application.Services
{
    public class TransferenciaService : ITransferenciaService
    {
        private readonly BancoContext _context;
        private readonly IBaseRepository<TransferenciaModel> _baseRepository;

        public TransferenciaService(BancoContext context, IBaseRepository<TransferenciaModel> baseRepository)
        {
            _context = context;
            _baseRepository = baseRepository;
        }
        public async Task<IEnumerable<TransferenciaModel>> GetAllTransferencias(string usuario)
        {
            var user = await _context.usuarioModels.SingleOrDefaultAsync(item => item.email == usuario); 
           
            return await _context.transferenciaModels.Where(item => item.info_remetente == user.cpf).ToListAsync();
        }
        public async Task<UsuarioModel> GetByIdentificador(string identificador)
        {
            return await _context.usuarioModels.SingleOrDefaultAsync(item => item.cpf == identificador 
            || item.email == identificador 
            || item.telefone == identificador);
        }

        public async Task<bool> EstornarPix(Guid idTransacao)
        {
            try
            {
                var pixTransacao = await _context.transferenciaModels.SingleOrDefaultAsync(item => item.dd == idTransacao);

                if (pixTransacao == null)
                {
                    return false;
                }
                var remetente = await _context.usuarioModels.SingleOrDefaultAsync(user => user.cpf == pixTransacao.info_remetente);
                var destinatario = await _context.usuarioModels.SingleOrDefaultAsync(user => user.cpf == pixTransacao.info_destinatario);
                var destinatarioQuery = await _context.lojistaModels.SingleOrDefaultAsync(user => user.cpf == pixTransacao.info_destinatario);

                bool autorizador = await _baseRepository.Autorizador();

                if (autorizador == true)
                {
                    remetente.saldo += pixTransacao.valor;
                     _context.usuarioModels.Update(remetente);

                    if (destinatario is UsuarioModel)
                    {
                        destinatario.saldo -= pixTransacao.valor;
                         _context.usuarioModels.Update(destinatario);
                    }
                    else if (destinatarioQuery is LojistaModel)
                    {
                        destinatarioQuery.saldo -= pixTransacao.valor;
                         _context.lojistaModels.Update(destinatarioQuery);
                    }
                    else
                    {
                        Console.WriteLine("PROBLEMA COM DESTINATARIO");
                        return false;
                    }

                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro em EstornarPix: {ex.Message}");
                return false;
            }
        }
        public async Task<TransferenciaModel> Pix(string remetente, string destinatario, decimal valor, TransferenciaModel transferenciaModel)
        {
                bool autorizado = await _baseRepository.Autorizador();

                if (autorizado == true)
                {
                    var remetenteQuery = await _context.usuarioModels
                  .SingleOrDefaultAsync(c => c.cpf == remetente || c.telefone == remetente || c.email == remetente);

                    var destinatarioQuery = await _context.usuarioModels
                        .Where(c => c.cpf == destinatario || c.telefone == destinatario || c.email == destinatario)
                        .SingleOrDefaultAsync();

                    var destinatarioQueryLojista = await _context.lojistaModels
                        .Where(c => c.cpf == destinatario || c.cnpj == destinatario || c.email == destinatario)
                        .SingleOrDefaultAsync();

                    if (destinatarioQuery == null && destinatarioQueryLojista == null || remetenteQuery == null)
                    {
                        Console.WriteLine("DESTINATARIO NÃO ENCONTRADO OU SUA CHAVE PIX ESTA COM PROBLEMAS");
                    }
                    else
                    {
                        if (remetenteQuery.saldo >= valor)
                        {
                            remetenteQuery.saldo -= valor;

                            transferenciaModel.dd = Guid.NewGuid();  // Gera um novo Guid
                            transferenciaModel.info_remetente = remetenteQuery.cpf ?? remetenteQuery.email ?? remetenteQuery.telefone;

                            transferenciaModel.valor = valor;
                            transferenciaModel.dataTransferencia = DateTime.UtcNow;

                            if (destinatarioQuery is UsuarioModel)
                            {
                                // Destinatário é um usuário
                                var destinatarioUsuario = destinatarioQuery;
                                destinatarioUsuario.saldo += valor;
                                transferenciaModel.info_destinatario = destinatarioQuery.cpf ?? destinatarioQuery.email ?? destinatarioQuery.telefone;
                            }
                            else if (destinatarioQueryLojista is LojistaModel)
                            {
                                // Destinatário é um lojista
                                var destinatarioLojista = destinatarioQueryLojista;
                                destinatarioLojista.saldo += valor;
                                transferenciaModel.info_destinatario = destinatarioQueryLojista.cpf ?? destinatarioQueryLojista.cnpj;
                            }
                            else
                            {
                                Console.WriteLine("PROBLEMA COM DESTINATARIO");
                            }
                            await _context.transferenciaModels.AddAsync(transferenciaModel);
                            await _context.SaveChangesAsync();
                            Console.WriteLine("TRANSAÇÃO CONCLUÍDA COM SUCESSO");
                        }
                        else
                        {
                            Console.WriteLine("SALDO INSUFICIENTE");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("TRANSAÇÃO NÃO AUTORIZADA");
                }
          
            return transferenciaModel;
        }
    }
}
