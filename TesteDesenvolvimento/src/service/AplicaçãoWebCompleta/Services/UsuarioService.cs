using AplicaçãoWebCompleta.Data.Repositoy.Interface;
using AplicaçãoWebCompleta.Data.Table;
using AplicaçãoWebCompleta.Models.Request;
using AplicaçãoWebCompleta.Models.Response;
using AplicaçãoWebCompleta.Services.Interface;
using AplicaçãoWebCompleta.Validators;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AplicaçãoWebCompleta.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IConsultaCep _consultaCep;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, IConsultaCep consultaCep)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _consultaCep = consultaCep;
        }

        public async Task CriarUsuarioAsync(UsuarioRequest usuarioRequest)
        {
            var validator = new UsuarioRequestValidator()
                .Validate(usuarioRequest);

            if (!validator.IsValid)
                throw new BadHttpRequestException(validator.Errors.FirstOrDefault().ErrorMessage);

            var usuario = _mapper.Map<CadastroUsuario>(usuarioRequest);
            await _usuarioRepository.CriarUsuarioAsync(usuario);
        }

        public async Task AtualizarUsuarioAsync(AtualizarUsuarioRequest atualizarUsuarioRequest)
        {
            var validator = new AtualizarUsuarioRequestValidator()
                .Validate(atualizarUsuarioRequest);

            if (!validator.IsValid)
                throw new BadHttpRequestException(validator.Errors.FirstOrDefault().ErrorMessage);

            var usuario = _mapper.Map<CadastroUsuario>(atualizarUsuarioRequest);
            await _usuarioRepository.AtualizarUsuarioAsync(usuario);
        }

        public async Task<IEnumerable<UsuarioResponse>> ListarUsuarioAsync()
        {
            var usuario = await _usuarioRepository.ListarUsuarioAsync();
            return _mapper.Map<IEnumerable<UsuarioResponse>>(usuario);
        }

        public async Task<UsuarioResponse> BuscarUsuarioPorIdAsync(int id)
        {
            var usuario = await _usuarioRepository.BuscarUsuarioPorIdAsync(id);
            return _mapper.Map<UsuarioResponse>(usuario);
        }

        public async Task RemoverUsuarioAsync(int id)
        {
            var usuario = await _usuarioRepository.BuscarUsuarioPorIdAsync(id);

            await _usuarioRepository.RemoverUsuarioAsync(usuario);
        }

        public async Task<ConsultaCep> ConsultarCepAsync(string cep)
        {
            var cepRequest = new ConsultaCepRequest { CEP = cep };

            var validator = new ConsultaCepRequestValidator().Validate(cepRequest);

            if (!validator.IsValid)
                throw new BadHttpRequestException(validator.Errors.FirstOrDefault()?.ErrorMessage ?? "CEP inválido.");

            try
            {
                var endereco = await _consultaCep.GetCepAsync(cep);
                if (endereco == null || string.IsNullOrWhiteSpace(endereco?.CEP))
                    throw new ArgumentException("O CEP fornecido não foi encontrado.");

                return endereco;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
