using CentroEventos.Aplicacion.Entidades;
using CentroEventos.Aplicacion.Interfaces;

namespace CentroEventos.Aplicacion.CasosDeUso.PersonaCDU
{
    // NO NECESITA VALIDACION
    // NO NECESITA AUTORIZACION
    public class ListarUsuarioUseCase(IRepositorioPersona repositorioPersona)
    {
        private readonly IRepositorioPersona _repositorioPersona = repositorioPersona;

        public List<Persona> Ejecutar()
        {
            return _repositorioPersona.Listar();
        }
    }
}