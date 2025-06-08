using CentroEventos.Aplicacion.Interfaces;
using CentroEventos.Aplicacion.Enums;
using CentroEventos.Aplicacion.Excepciones;
namespace CentroEventos.Aplicacion.CasosDeUso.EventoCDU
{
    public class EventoBajaUseCase(IRepositorioEventoDeportivo repositorioEvento, IServicioAutorizacion servicioAutorizacion)
    {
        private readonly IRepositorioEventoDeportivo _repositorioEvento = repositorioEvento;
        private readonly IServicioAutorizacion _servicioAutorizacion = servicioAutorizacion;

        public void Ejecutar(int idUsuario, int idEvento)
        {
            if (!_servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.EventoBaja))
            {
                throw new FalloAutorizacionException("No tiene permiso para realizar esto");
            }

            var evento = _repositorioEvento.ObtenerEventoPorId(idEvento);
            if (evento == null)
            {
                throw new Exception("El evento no existe.");
            }

            _repositorioEvento.Eliminar(idEvento);
        }
    }
}