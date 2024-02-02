using AutoMapper.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PicPay.Application.DTOs;
using PicPay.Application.Interfaces;
using PicPay.Domain.Models;
using PicPay.Infra.Data.Data;
using System.Linq;


namespace PicPay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly BancoContext _context;

        public LoginController(ITokenService tokenService, BancoContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> Login(LoginDTO loginDTO)
        {
            try
            {
                var lojistaQuery = _context.Set<LojistaModel>()
                    .Where(c => c.email == loginDTO.Email && c.senha == loginDTO.Password);

                var usuarioQuery = _context.Set<UsuarioModel>()
                    .Where(c => c.email == loginDTO.Email && c.senha == loginDTO.Password);

                if (usuarioQuery is null && lojistaQuery is null)
                {
                    return BadRequest();
                }
                else
                {
                   
                    var token = _tokenService.GerarToken(loginDTO.Email, loginDTO.Password);
                    return Ok(new { Token = token});
                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocorreu um erro interno ao processar a solicitação." });
            }
        }

    }
}
