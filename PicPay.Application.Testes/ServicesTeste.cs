using Castle.Components.DictionaryAdapter.Xml;
using Moq;
using PicPay.Application.DTOs;
using PicPay.Application.Interfaces;
using PicPay.Application.Services;
using PicPay.Domain.Models;

namespace PicPay.Application.Testes
{
    public class ServicesTeste
    {
        private readonly Mock<ILojistaRepository> mockLojistaRepository = new Mock<ILojistaRepository>();
        private readonly Mock<IUsuarioRepository> mockUsuarioRepository = new Mock<IUsuarioRepository>();
        private readonly Mock<ITransferenciaService> mockTransferenciaService = new Mock<ITransferenciaService>();

       [Fact]
       public async Task Registrar_Lojista_Falhar()
       {
            var lojistaDTO = new LojistaDTO
            {
                id_lojista = Guid.NewGuid(),
                nomeCompleto = "Nome do Lojista",
                cpf = "123.456.789-01",
                email = "lojista@email.com",
                senha = "senha123",
                saldo = 1000.00m,
                cnpj = "12.345.678/0001-90" //cnpj existente no banco
            };

            mockLojistaRepository.Setup(x => x.Register(It.IsAny<LojistaDTO>()))
             .ThrowsAsync(new InvalidOperationException("Email, Cpf ou Cnpj já cadastrado"));

            var service = mockLojistaRepository.Object;

             await Assert.ThrowsAsync<InvalidOperationException>
                (async () => await service.Register(lojistaDTO)); // SE A EXCEPTION FOR LANÇADA O TESTE PASSA          
        }

        [Fact]
        public async Task Registrar_Lojista_Sucesso()
        {
            var lojistaDTO = new LojistaDTO
            {
                id_lojista = Guid.NewGuid(),
                nomeCompleto = "Nome do Lojista",
                cpf = "123.456.789-01",
                email = "lojista@email.com",
                senha = "senha123",
                saldo = 1000.00m,
                cnpj = "16.455.878/0001-97" 
            };
            mockLojistaRepository.Setup(x => x.Register(It.IsAny<LojistaDTO>())).Returns(Task.FromResult(lojistaDTO));

            var service = mockLojistaRepository.Object;

            var resultado = await service.Register(lojistaDTO);

            Assert.NotNull(resultado);
            mockLojistaRepository.Verify(c => c.Register(It.IsAny<LojistaDTO>()));
            Assert.Same(lojistaDTO, resultado);

        }
        [Fact]
        public async Task Registrar_Usuario_Falhar()
        {

            var usuarioDTO = new UsuarioDTO {

                id_usuario = Guid.NewGuid(),
                nomeCompleto = "Nome do Usuário",
                cpf = "123.456.789-01", // cpf existente
                email = "usuario@email.com",
                senha = "senha123",
                saldo = 1000.00m,
                telefone = "123456789"
            };

            mockUsuarioRepository.Setup(x => x.Register(It.IsAny<UsuarioDTO>()))
                .ThrowsAsync(new InvalidOperationException("Email ou Cpf  já cadastrado")); 

            var service = mockUsuarioRepository.Object;

            var resultado =  service.Register(usuarioDTO);
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.Register(usuarioDTO));
            mockUsuarioRepository.Verify(c => c.Register(It.IsAny<UsuarioDTO>()));

        }

        [Fact]
        public async Task Registrar_Usuario_Sucesso()
        {
            var usuarioDTO = new UsuarioDTO
            {
                id_usuario = Guid.NewGuid(),
                nomeCompleto = "Nome do Usuário",
                cpf = "923.756.989-11",
                email = "usuario@email.com",
                senha = "senha123",
                saldo = 1000.00m,
                telefone = "123456789"
            };

            mockUsuarioRepository.Setup(x => x.Register(It.IsAny<UsuarioDTO>()))
                       .Returns(Task.FromResult(usuarioDTO));

            var service = mockUsuarioRepository.Object;

            var resultado = await service.Register(usuarioDTO);

            Assert.NotNull(resultado);
            Assert.Equal(usuarioDTO.id_usuario, resultado.id_usuario);
            Assert.Equal(usuarioDTO.nomeCompleto, resultado.nomeCompleto);
            Assert.Equal(usuarioDTO.cpf, resultado.cpf);
            Assert.Equal(usuarioDTO.email, resultado.email);
            Assert.Equal(usuarioDTO.saldo, resultado.saldo);
            Assert.Equal(usuarioDTO.telefone, resultado.telefone);

            Assert.Same(resultado, usuarioDTO);

        }

        [Fact]
        public async Task Realiazar_PIX_Falhar()
        {
            var usuarioRemetenteDTO = new UsuarioDTO
            {
                id_usuario = Guid.NewGuid(),
                nomeCompleto = "Nome do Usuário",
                cpf = "923.756.989-11",
                email = "usuario@email.com",
                senha = "senha123",
                saldo = 1000.00m,
                telefone = "123456789"
            };
            var usuarioDestinatarioDTO = new UsuarioDTO
            {
                id_usuario = Guid.NewGuid(),
                nomeCompleto = "Nome do Usuário",
                cpf = "483.556.289-71",
                email = "usuario2@email.com",
                senha = "senha123",
                saldo = 1000.00m,
                telefone = "(13)3456789"
            };

            var transferenciaModel = new TransferenciaModel();

            mockTransferenciaService.Setup(x => x.Pix(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(), 
                It.IsAny<TransferenciaModel>()))
                .ReturnsAsync(new TransferenciaModel());


            var service = mockTransferenciaService.Object;

            //REMETENTE E O MESMO DO DESTINATARIO
            var resultado =  await service.Pix(usuarioRemetenteDTO.cpf, usuarioRemetenteDTO.cpf, 2.00m, transferenciaModel);

            mockTransferenciaService.Verify(c => c.Pix(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(),
               It.IsAny<TransferenciaModel>()));
            Assert.Null(transferenciaModel.info_remetente);
           
        }

        [Fact]
        public async Task Realiazar_PIX_Sucesso()
        {
            var usuarioRemetenteDTO = new UsuarioDTO
            {
                id_usuario = Guid.NewGuid(),
                nomeCompleto = "Nome do Usuário",
                cpf = "923.756.989-11",
                email = "usuario@email.com",
                senha = "senha123",
                saldo = 1000.00m,
                telefone = "(11)3456789"
            };
            var usuarioDestinatarioDTO = new UsuarioDTO
            {
                id_usuario = Guid.NewGuid(),
                nomeCompleto = "Nome do Usuário",
                cpf = "483.556.289-71",
                email = "usuario2@email.com",
                senha = "senha123",
                saldo = 1000.00m,
                telefone = "(13)3456789"
            };

            var transferenciaModel = new TransferenciaModel();

            mockTransferenciaService.Setup(x => x.Pix(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(),
         It.IsAny<TransferenciaModel>()))
         .ReturnsAsync((string remetente, string destinatario, decimal valor, TransferenciaModel transferencia) =>
         {
           
             return new TransferenciaModel
             {
                 info_remetente = remetente,
                 info_destinatario = destinatario
             };
         });


            var service = mockTransferenciaService.Object;

            //REMETENTE E O MESMO DO DESTINATARIO
            var resultado = await service.Pix(usuarioRemetenteDTO.cpf, usuarioDestinatarioDTO.cpf, 2.00m, transferenciaModel);

            mockTransferenciaService.Verify(c => c.Pix(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(),
               It.IsAny<TransferenciaModel>()));
            Assert.Equal(usuarioRemetenteDTO.cpf, resultado.info_remetente);
            Assert.Equal(usuarioDestinatarioDTO.cpf, resultado.info_destinatario);

        }


        [Fact]
        public async Task Solicitar_Estorno_Falhar()
        {      
            var usuarioRemetenteDTO = new UsuarioDTO
            {
                id_usuario = Guid.NewGuid(),
                nomeCompleto = "Nome do Usuário",
                cpf = "923.756.989-11",
                email = "usuario@email.com",
                senha = "senha123",
                saldo = 1000.00m,
                telefone = "(11)3456789"
            };
            var usuarioDestinatarioDTO = new UsuarioDTO
            {
                id_usuario = Guid.NewGuid(),
                nomeCompleto = "Nome do Usuário",
                cpf = "483.556.289-71",
                email = "usuario2@email.com",
                senha = "senha123",
                saldo = 1000.00m,
                telefone = "(13)3456789"
            };

            var transferencia = new TransferenciaModel
            {
                dd = new Guid("fce1e559-b36c-4cb0-9a91-b2d6835c120b"),
                info_remetente = "923.756.989-11",
                info_destinatario = "483.556.289-71",
                valor = 100.00m,
                autorizacaoExterna = true,
                sucesso = true,
                dataTransferencia = DateTime.UtcNow
            };

            var teste = new Guid(); // GUID QUE NÃO EXISTE NO BANCO DE DADOS

            mockTransferenciaService.Setup(c => c.EstornarPix(It.IsAny<Guid>()));
                

            var service = mockTransferenciaService.Object;

          
            var resultado = await service.EstornarPix(teste);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Solicitar_Estorno_Sucesso()
        {
            var usuarioRemetenteDTO = new UsuarioDTO
            {
                id_usuario = Guid.NewGuid(),
                nomeCompleto = "Nome do Usuário",
                cpf = "923.756.989-11",
                email = "usuario@email.com",
                senha = "senha123",
                saldo = 1000.00m,
                telefone = "(11)3456789"
            };
            var usuarioDestinatarioDTO = new UsuarioDTO
            {
                id_usuario = Guid.NewGuid(),
                nomeCompleto = "Nome do Usuário",
                cpf = "483.556.289-71",
                email = "usuario2@email.com",
                senha = "senha123",
                saldo = 1000.00m,
                telefone = "(13)3456789"
            };

            var transferencia = new TransferenciaModel
            {
                dd = new Guid("fce1e559-b36c-4cb0-9a91-b2d6835c120b"),
                info_remetente = "923.756.989-11",
                info_destinatario = "483.556.289-71",
                valor = 100.00m,
                autorizacaoExterna = true,
                sucesso = true,
                dataTransferencia = DateTime.UtcNow
            };

            mockTransferenciaService.Setup(c => c.EstornarPix(It.IsAny<Guid>()))
                .ReturnsAsync(true);
               

            var service = mockTransferenciaService.Object;


            var resultado = await service.EstornarPix(transferencia.dd);

            Assert.True(resultado);
        }



    }  
}